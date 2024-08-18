using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public Ray[] rays;

    [HideInInspector] public float score;
    [HideInInspector] public bool completed = false;
    private void OnDrawGizmosSelected()
    {
        if (rays != null)
        {
            Gizmos.color = Color.red;
            for(int i = 0; i < rays.Length; ++i)
            {
                //Handles.color = Color.red;
                //rays[i].from = Handles.FreeMoveHandle(rays[i].from + (Vector2)transform.position, .2f, Vector2.zero, Handles.CylinderHandleCap) - transform.position;
                //Handles.color = Color.blue;
                //rays[i].to = Handles.FreeMoveHandle(rays[i].to + (Vector2)transform.position, .2f, Vector2.zero, Handles.CylinderHandleCap) - transform.position;
                Gizmos.DrawLine((Vector3)rays[i].from+transform.position, (Vector3)rays[i].to+transform.position);
            }
        }
    }
    public bool HomeIsComplete()
    {
        int layerMask = LayerMask.GetMask("Brisk");
        for(int i=0;i< rays.Length; ++i)
        {
            Vector2 direction = rays[i].to-rays[i].from;
            float magnitude = direction.magnitude;
            direction /= magnitude;
            if (!Physics2D.Raycast((Vector3)rays[i].from + transform.position,
                direction, magnitude, layerMask
                ))
                return false;
        }
        return true;
    }
    [System.Serializable]
    public struct Ray
    {
        public Vector2 from, to;
    }
}
