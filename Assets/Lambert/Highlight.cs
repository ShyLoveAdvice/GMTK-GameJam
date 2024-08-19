using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highlight : MonoBehaviour
{
    public Material highlightMat;
    public float mouseEnterHighlightWidth = 0.005f;
    public float defaultHighlightWidth = 0f;
    public float transitionDuration = 0.1f;
    float targetHighlightWidth;
    float targetHightlightSpeed;
    float transitionTimer;
    Material m_material;
    bool transitioning;
    // Start is called before the first frame update
    void Start()
    {
        Image image = GetComponent<Image>();
        image.material = new Material(highlightMat);
        m_material = image.material;
        //GetComponent<Image>().material.SetFloat("_OutlineWidth", Random.Range(0, 0.1f));
    }

    void Update()
    {
        if(transitioning)
        {
            if(transitionTimer < transitionDuration)
            {
                transitionTimer += Time.deltaTime;
                m_material.SetFloat("_OutlineWidth", m_material.GetFloat("_OutlineWidth") + targetHightlightSpeed * Time.deltaTime);
            }
            else
            {
                m_material.SetFloat("_OutlineWidth", targetHighlightWidth);
                transitioning = false;
            }
        }
    }
    public void MouseEnterHighlight()
    {
        var distance = mouseEnterHighlightWidth - m_material.GetFloat("_OutlineWidth");
        targetHighlightWidth = mouseEnterHighlightWidth;
        targetHightlightSpeed = distance / transitionDuration;
        transitionTimer = 0f;

        transitioning = true;
    }
    public void MouseExitHighlight()
    {
        var distance = defaultHighlightWidth - m_material.GetFloat("_OutlineWidth");
        targetHighlightWidth = defaultHighlightWidth;
        targetHightlightSpeed = distance / transitionDuration;
        transitionTimer = 0f;

        transitioning = true;
    }
}
