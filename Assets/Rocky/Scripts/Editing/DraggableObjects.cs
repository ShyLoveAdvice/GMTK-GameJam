using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DraggableObjects : MonoBehaviour
{
    [SerializeField] float price;
    [Header("Bounding Box")]
    public Vector2 leftTop;
    public Vector2 rightBottom;

    Collider2D bc;
    Rigidbody2D rb;
    Camera mainCam;
    //---mouse events---
    float mouseLeftButDownTimer, mouseClickThreshold = .1f;
    //dragging
    bool selected = false;
    Vector3 draggingOffset;
    //bounding points
    [HideInInspector] public Vector2 baseScale;

    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube((leftTop + rightBottom)/2 + (Vector2)transform.position, leftTop - rightBottom);
    }
    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        mainCam = Camera.main;
        mouseLeftButDownTimer = 0.0f;
        baseScale = transform.localScale;
    }
    public float GetRawPrice()
    {
        return price;
    }
    public float GetScaledPrice()
    {
        float scale = transform.localScale.x / baseScale.x;
        float ratio = Mathf.Clamp(scale, 0.25f, 4.0f);
        if (ratio > 1) //larger
            return price * (1 + (ratio - 1) / 6);
        //smaller
        return price * ratio;
    }
    public float PriceToScale(float tprice)
    {
        if (tprice < price) // smaller
        {
            //Debug.Log("smaller. base scale.x=" + baseScale.x);
            return tprice / price * baseScale.x;
        }
        //Debug.Log("larger. base scale.x=" + baseScale.x);
        return (6 * tprice / price - 5) * baseScale.x;
    }
    public void EnableCollider(bool val)
    {
        bc.isTrigger = !val;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            if ((DraggableManager.instance.SelectedObject==null || DraggableManager.instance.SelectedObject==this) && Physics2D.OverlapPoint(mouseWorldPos, LayerMask.GetMask("Brisk")) == bc)
            {
                selected = true;
                draggingOffset = transform.position - mouseWorldPos;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (selected && mouseLeftButDownTimer < mouseClickThreshold)
                OnClicked();
            mouseLeftButDownTimer = 0.0f;
            selected = false;
        }
    }
    private void FixedUpdate()
    {
        if (selected && Input.GetMouseButton(0))
        {
            mouseLeftButDownTimer += Time.fixedDeltaTime;
            if (mouseLeftButDownTimer > mouseClickThreshold)
                OnDragging();
        }
    }
    void OnClicked()
    {
        DraggableManager.instance.SelectedObject = this;
    }
    void OnDragging()
    {
        rb.position = draggingOffset + mainCam.ScreenToWorldPoint(Input.mousePosition);
        DraggableManager.instance.editingTool.TargetObj=this;
    }
    public Vector3[] GetBoundingPoints()
    {
        Vector3[] ret = new Vector3[5];
        Vector2 halfSize = leftTop - rightBottom;
        Vector2 center = (leftTop + rightBottom) / 2;
        Quaternion rotation = transform.rotation;
        halfSize.x = Mathf.Abs(halfSize.x) / 2 / baseScale.x * transform.localScale.x;
        halfSize.y = Mathf.Abs(halfSize.y) / 2 / baseScale.y * transform.localScale.y;
        ret[0] = new Vector3(center.x - halfSize.x, center.y + halfSize.y);
        ret[1] = new Vector3(center.x + halfSize.x, center.y + halfSize.y);
        ret[2] = new Vector3(center.x + halfSize.x, center.y - halfSize.y);
        ret[3] = new Vector3(center.x - halfSize.x, center.y - halfSize.y);
        for(int i = 0; i < 4; ++i)
        {
            ret[i] = rotation * ret[i] + transform.position;
        }
        ret[4] = ret[0];
        return ret;
    }
}
