using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;

public class EditingTool : Singleton<EditingTool>
{
    [SerializeField] LineRenderer dotLines;
    [Header("editing parameters")]
    [SerializeField] float scalingSpeed;
    [SerializeField] float rotatingSpeed;
    [SerializeField] float minScale, maxScale;

    public event System.Action onEditted;
    //editing variables
    Vector3 scalingOffset;
    Quaternion rotationOffset, rotationOffsetNeg;
    public Vector3 ScalingOffset
    {
        get => scalingOffset;
    }
    public Quaternion RotationOffset
    {
        get => rotationOffset;
    }
    public Quaternion RotationOffsetNeg
    {
        get => rotationOffsetNeg;
    }

    Transform targetObj;
    public Transform TargetObj
    {
        get => targetObj;
        set
        {
            targetObj = value;
            UpdateTransform();
        }
    }
    private void Start()
    {
        scalingOffset = new Vector3(scalingSpeed, scalingSpeed, 0);
        rotationOffset = Quaternion.AngleAxis(rotatingSpeed, Vector3.back);
        rotationOffsetNeg = Quaternion.AngleAxis(rotatingSpeed, Vector3.forward);
        gameObject.SetActive(false);
    }
    Vector3[] GetBoundingPoints()
    {
        Vector3[] points = new Vector3[5];
        Vector2 halfSize = transform.localScale;
        //halfSize.x*=targetObj
        Vector3 center = transform.position;
        //points[0] = new Vector2(center.x - halfSize.x, center.y + halfSize.y);
        //points[1] = new Vector2(center.x + halfSize.x, center.y + halfSize.y);
        //points[2] = new Vector2(center.x + halfSize.x, center.y - halfSize.y);
        //points[3] = new Vector2(center.x - halfSize.x, center.y - halfSize.y);
        points[0] = new Vector2(-halfSize.x, halfSize.y);
        points[1] = new Vector2(halfSize.x, halfSize.y);
        points[2] = new Vector2(halfSize.x, -halfSize.y);
        points[3] = new Vector2(-halfSize.x, -halfSize.y);
        for(int i = 0; i < 4; ++i)
        {
            points[i] = transform.rotation * points[i] + center;
        }
        points[4] = points[0];
        return points;
    }
    public void UpdateTransform()
    {
        transform.position = targetObj.position;
        transform.localScale = targetObj.localScale;
        transform.rotation = targetObj.rotation;
        dotLines.SetPositions(GetBoundingPoints());
        onEditted?.Invoke();
    }
    public void SetObjRotation(Quaternion rotation)
    {
        targetObj.rotation = rotation;
        UpdateTransform();
    }
    public void SetObjScale(float scale)
    {
        scale = Mathf.Clamp(scale, minScale, maxScale);
        targetObj.localScale = new Vector3(scale, scale, 1);
        UpdateTransform();
    }
    private void FixedUpdate()
    {
        if(targetObj != null)
        {
            //scaling
            if (Input.GetKey(KeyCode.W)) //up
            {
                targetObj.localScale += scalingOffset;
                UpdateTransform();
            }
            else if (Input.GetKey(KeyCode.S))
            {
                targetObj.localScale -= scalingOffset;
                UpdateTransform();
            }
            if (Input.GetKey(KeyCode.D))
            {
                targetObj.rotation *= rotationOffset;
                UpdateTransform();
            }
            else if (Input.GetKey(KeyCode.A))
            {
                targetObj.rotation *= rotationOffsetNeg;
                UpdateTransform();
            }
            if (Input.GetKey(KeyCode.Delete))
            {
                DraggableManager.instance.SellObject();
            }
        }
    }
}
