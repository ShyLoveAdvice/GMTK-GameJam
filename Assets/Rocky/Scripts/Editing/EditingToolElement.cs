using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EditingToolElement : MonoBehaviour
{
    public enum EditType
    {
        Rotate,
        ScaleLT,
        ScaleRT,
        ScaleLB,
        ScaleRB
    }
    public EditType editType;
    BoxCollider2D bc;
    Camera mainCam;
    bool isDragging;
    Vector3 mouseOffset;
    float originalAngle;
    float scaleBaseLength;
    // Start is called before the first frame update
    void Awake()
    {
        bc = GetComponent<BoxCollider2D>();
        mainCam = Camera.main;
    }
    void AdjustGlobalScale(Vector3[] boundingPoints)
    {
        Vector3 editingToolScale = EditingTool.instance.transform.localScale;
        float newScale = ((Vector2)(boundingPoints[1] - boundingPoints[0])).magnitude * EditingTool.instance.buttonSize;
        transform.localScale = new Vector3(newScale / editingToolScale.x, newScale / editingToolScale.y, newScale / editingToolScale.z);
        switch (editType)
        {
            case EditType.ScaleLT:
                transform.position = boundingPoints[0];
                break;
            case EditType.ScaleRT:
                transform.position = boundingPoints[1];
                break;
            case EditType.ScaleLB:
                transform.position = boundingPoints[3];
                break;
            case EditType.ScaleRB:
                transform.position = boundingPoints[2];
                break;
            case EditType.Rotate:
                Vector2 temp = boundingPoints[1] - boundingPoints[0];
                temp = new Vector2(-temp.y, temp.x).normalized * newScale * 2.0f;
                transform.position = (Vector2)(boundingPoints[1] + boundingPoints[0]) / 2 + temp;
                break;
        }
    }
    private void OnEnable()
    {
        EditingTool.instance.onEditted += AdjustGlobalScale;
    }
    private void OnDisable()
    {
        EditingTool.instance.onEditted -= AdjustGlobalScale;
    }
    
    bool MouseOnElement()
    {
        return Physics2D.OverlapPoint(mainCam.ScreenToWorldPoint(Input.mousePosition)) == bc;
    }
    void Edit()
    {
        switch (editType)
        {
            case EditType.Rotate:
                {
                    float angle = Vector2.SignedAngle(mouseOffset, mainCam.ScreenToWorldPoint(Input.mousePosition) - EditingTool.instance.transform.position);
                    angle += originalAngle;
                    EditingTool.instance.SetObjRotation(Quaternion.AngleAxis(angle, Vector3.forward));
                    break;
                }
            default:
                {
                    float length=((Vector2)(mainCam.ScreenToWorldPoint(Input.mousePosition)-EditingTool.instance.transform.position)).magnitude;
                    EditingTool.instance.SetObjScale(length / scaleBaseLength);
                    break;
                }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && MouseOnElement())
        {
            isDragging = true;
            Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            mouseOffset = mousePos - EditingTool.instance.transform.position;
            switch (editType)
            {
                case EditType.Rotate:
                    originalAngle = EditingTool.instance.transform.eulerAngles.z;
                    break;
                default:
                {
                    scaleBaseLength = ((Vector2)mouseOffset).magnitude/EditingTool.instance.TargetObj.transform.localScale.x;
                    break;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }
    private void FixedUpdate()
    {
        if(isDragging && Input.GetMouseButton(0))
        {
            Edit();
        }
    }
}
