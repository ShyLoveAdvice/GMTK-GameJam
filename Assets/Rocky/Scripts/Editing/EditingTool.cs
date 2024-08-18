using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;

public class EditingTool : MonoBehaviour
{
    [SerializeField] LineRenderer dotLines;
    [Header("editing parameters")]
    [SerializeField] float scalingSpeed;
    [SerializeField] float rotatingSpeed;

    //editing variables
    Vector3 scalingOffset;
    Quaternion rotationOffset, rotationOffsetNeg;

    Transform targetObj;
    private void Start()
    {
        scalingOffset = new Vector3(scalingSpeed, scalingSpeed, 0);
        rotationOffset = Quaternion.AngleAxis(rotatingSpeed, Vector3.back);
        rotationOffsetNeg = Quaternion.AngleAxis(rotatingSpeed, Vector3.forward);
    }
    Vector3[] GetBoundingPoints()
    {
        Vector3[] points = new Vector3[5];
        Vector2 halfSize = transform.localScale / 2;
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
    }
    public void SetTargetObject(Transform obj)
    {
        targetObj = obj;
        UpdateTransform();
    }
    void SellObject()
    {
        Destroy(targetObj.gameObject);
        targetObj = null;
        gameObject.SetActive(false);
    }
    public void CreateObject(GameObject prefab)
    {
        GameObject ins = Instantiate(prefab);
        SetTargetObject(ins.transform);
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
                SellObject();
            }
        }
    }
}
