using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DraggableObjects : MonoBehaviour
{
    [SerializeField] float price;
    Collider2D bc;
    Rigidbody2D rb;
    Camera mainCam;
    //---mouse events---
    float mouseLeftButDownTimer, mouseClickThreshold = .1f;
    //dragging
    bool selected = false;
    Vector3 draggingOffset;

    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        mainCam = Camera.main;
        mouseLeftButDownTimer = 0.0f;
    }
    public float GetScaledPrice()
    {
        float ratio = Mathf.Clamp(transform.localScale.x, 0.25f, 4.0f);
        if (ratio > 1) //larger
            return price * (1 + (ratio - 1) / 6);
        //smaller
        return price * ratio;
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
        DraggableManager.instance.editingTool.TargetObj=transform;
    }
}
