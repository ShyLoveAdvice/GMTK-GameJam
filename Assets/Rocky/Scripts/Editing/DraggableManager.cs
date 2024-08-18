using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DraggableManager : Singleton<DraggableManager>
{
    public Animal animal;
    public EditingTool editingTool;
    public BriskInventory briskInventory;
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] Transform briskPanelUI;
    [SerializeField] TextMeshProUGUI priceText;

    float priceSum;
    private DraggableObjects selectedObject;
    private List<DraggableObjects> brisks;
    public float PriceSum{
        get => priceSum;
    }
    public DraggableObjects SelectedObject
    {
        get => selectedObject;
        set
        {
            if (value == null && selectedObject!=null)
                selectedObject.EnableCollider(true);
            selectedObject = value;
            if (value != null)
            {
                InitializeTool();
                selectedObject.EnableCollider(false);
            }
            else
                DisableTool();
        }
    }
    public void GetScore()
    {
        if (animal == null)
            return;
        if (animal.HomeIsComplete())
        {
            Capturer.instance.Capture(animal.transform.position);
        }
        else
            Debug.Log("home incomplete!");
    }
    public void SetAnimal(Animal anim)
    {
        if (anim == null)
        {
            foreach(DraggableObjects e in brisks)
            {
                Destroy(e.gameObject);
            }
            brisks.Clear();
            briskPanelUI.gameObject.SetActive(false);
        }
        else
        {
            priceSum = 0;
            animal = anim;
            briskPanelUI.gameObject.SetActive(true);
            UpdatePriceText();
        }
    }
    public void CalculatePriceSum()
    {
        priceSum = 0;
        for(int i=0;i<brisks.Count; i++)
        {
            priceSum += brisks[i].GetScaledPrice();
        }
    }
    public void UpdatePriceText()
    {
        CalculatePriceSum();
        priceText.text = priceSum.ToString("F2");
    }
    void InitializeTool()
    {
        editingTool.gameObject.SetActive(true);
        editingTool.TargetObj=selectedObject;
    }
    void DisableTool()
    {
        editingTool.gameObject.SetActive(false);
    }
    public void CreateObject(GameObject prefab)
    {
        GameObject ins = Instantiate(prefab);
        brisks.Add(ins.GetComponent<DraggableObjects>());
        UpdatePriceText();
    }
    public void SellObject()
    {
        int i = brisks.IndexOf(selectedObject);
        brisks[i] = brisks[brisks.Count - 1];
        brisks.RemoveAt(brisks.Count - 1);
        Destroy(selectedObject.gameObject);
        SelectedObject = null;
        UpdatePriceText();
    }
    private void Start()
    {
        brisks = new List<DraggableObjects>();
        editingTool.onEditted += UpdatePriceText;
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
            SelectedObject = null;
        }
    }
    private void FixedUpdate()
    {
    }
}
