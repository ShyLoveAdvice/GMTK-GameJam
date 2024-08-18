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
        ScaleMT,
        ScaleRT,
        ScaleLM,
        ScaleMM,
        ScaleRM,
        ScaleLB,
        ScaleMB,
        ScaleRB
    }
    public EditType editType;
    public bool globalScaleRemains;
    /// <summary>
    /// editing tool elements should keep their global scales constant.
    /// </summary>
    Vector3 globalScale;
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
        globalScale = transform.lossyScale;
    }
    void AdjustGlobalScale()
    {
        if (globalScaleRemains)
        {
            Vector3 editingToolScale = EditingTool.instance.transform.localScale;
            transform.localScale = new Vector3(globalScale.x / editingToolScale.x, globalScale.y / editingToolScale.y, globalScale.z / editingToolScale.z);
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
                    AdjustGlobalScale();
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
                    scaleBaseLength = ((Vector2)mouseOffset).magnitude/EditingTool.instance.TargetObj.localScale.x;
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
