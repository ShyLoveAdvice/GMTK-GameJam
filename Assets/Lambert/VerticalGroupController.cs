using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VerticalGroupController : MonoBehaviour
{
    public UnityEvent hoverEvent;
    private void OnMouseOver() {
        hoverEvent.Invoke();
    }
}
