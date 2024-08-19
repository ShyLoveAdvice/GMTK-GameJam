using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highlight : MonoBehaviour
{
    public Material highlightMat;
    // Start is called before the first frame update
    void Start()
    {
        highlightMat = GetComponent<Image>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
