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
    public float buttonSize;

    public event System.Action<Vector3[]> onEditted;
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

    DraggableObjects targetObj;
    public DraggableObjects TargetObj
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
    public void UpdateTransform()
    {
        transform.position = targetObj.transform.position;
        transform.rotation = targetObj.transform.rotation;
        Vector3[] boundingPoints = targetObj.GetBoundingPoints();
        dotLines.SetPositions(boundingPoints);
        onEditted?.Invoke(boundingPoints);
    }
    public void SetObjRotation(Quaternion rotation)
    {
        targetObj.transform.rotation = rotation;
        UpdateTransform();
    }
    public void SetObjScale(float scale)
    {
        scale = Mathf.Clamp(scale, minScale*targetObj.baseScale.x, maxScale*targetObj.baseScale.x);
        float originalPrice = targetObj.GetScaledPrice();
        targetObj.transform.localScale = new Vector3(scale, scale, 1);
        float newPrice = targetObj.GetScaledPrice();
        if (newPrice + DraggableManager.instance.PriceSum - originalPrice > GameManager.instance.Money)
        {
            float maxPrice = GameManager.instance.Money - DraggableManager.instance.PriceSum + originalPrice;
            scale = targetObj.PriceToScale(maxPrice);
            targetObj.transform.localScale = new Vector3(scale, scale, 1);
        }
        UpdateTransform();
    }
    private void FixedUpdate()
    {
        if(targetObj != null)
        {
            //scaling
            if (Input.GetKey(KeyCode.W)) //up
            {
                targetObj.transform.localScale += scalingOffset;
                UpdateTransform();
            }
            else if (Input.GetKey(KeyCode.S))
            {
                targetObj.transform.localScale -= scalingOffset;
                UpdateTransform();
            }
            if (Input.GetKey(KeyCode.D))
            {
                targetObj.transform.rotation *= rotationOffset;
                UpdateTransform();
            }
            else if (Input.GetKey(KeyCode.A))
            {
                targetObj.transform.rotation *= rotationOffsetNeg;
                UpdateTransform();
            }
            if (Input.GetKey(KeyCode.Delete))
            {
                DraggableManager.instance.SellObject();
            }
        }
    }
}
