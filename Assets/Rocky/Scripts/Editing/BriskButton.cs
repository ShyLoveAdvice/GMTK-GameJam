using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BriskButton : MonoBehaviour
{
    Button button;
    DraggableObjects briskPrefab;
    public DraggableObjects BriskPrefab
    {
        get => briskPrefab;
        set
        {
            if (briskPrefab != null)
            {
                Debug.LogError("BriskButton.BriskPrefab: briskPrefab was already given a value");
                return;
            }
            briskPrefab = value;
            if (button == null)
                button = GetComponent<Button>();
            button.onClick.AddListener(() => { 
                DraggableManager.instance.CreateObject(briskPrefab); 
            });
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if(button==null)
            button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
