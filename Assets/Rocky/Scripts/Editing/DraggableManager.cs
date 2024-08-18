using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DraggableManager : Singleton<DraggableManager>
{
    public EditingTool editingTool;
    public BriskInventory briskInventory;
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] Transform briskPanelUI;

    private DraggableObjects selectedObject;
    public DraggableObjects SelectedObject
    {
        get => selectedObject;
        set
        {
            selectedObject = value;
            if (value != null)
                InitializeTool();
        }
    }
    void InitializeTool()
    {
        editingTool.gameObject.SetActive(true);
        editingTool.TargetObj=selectedObject.transform;
    }
    void DisableTool()
    {
        editingTool.gameObject.SetActive(false);
    }
    private void Start()
    {
        for (int i = 0; i < briskInventory.brisks.Length; ++i)
        {
            GameObject but = Instantiate(buttonPrefab);
            BriskButton bb = but.GetComponent<BriskButton>();
            bb.BriskPrefab = briskInventory.brisks[i];
            but.transform.SetParent(briskPanelUI.transform, false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) //deselect
        {
            selectedObject = null;
            DisableTool();
        }
    }
    private void FixedUpdate()
    {
    }
}
