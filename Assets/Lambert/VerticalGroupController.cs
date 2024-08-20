using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VerticalGroupController : MonoBehaviour
{
    public Canvas canvas;
    public float panelWidth = 170f;
    RectTransform m_rectTransform;
    bool hovering = false;
    public UnityEvent upEvent;
    private void Start() {
        m_rectTransform = GetComponent<RectTransform>();
    }
    private void Update() {
        if(Input.GetMouseButtonUp(0))
        {
            var startPos = Input.mousePosition / canvas.scaleFactor;
            if(startPos.x < 170f)
            {
                upEvent.Invoke();
            }
        }
    }

}
