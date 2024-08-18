using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DraggableObjects : MonoBehaviour
{
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
        DraggableManager.instance.editingTool.SetTargetObject(transform);
    }
}
