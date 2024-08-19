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
    public Inventory briskInventory;
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
            if (selectedObject!=null)
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
    public void GetMoneyEarned()
    {
        if (animal == null)
        {
            Debug.Log("animal is null");
            return;
        }
        if (animal.completed)
            return;
        if (selectedObject != null) //if is still selecting an object, it might cover the entire animal. so this is to give unity some time to apply collider and avoid cheating
        {
            SelectedObject = null;
            StartCoroutine(GetMoneyEarnedDelayed());
        }
        else
        {
            if (animal.HomeIsComplete())
            {
                Capturer.AnalyzeResult res = Capturer.instance.Capture(animal.transform.position);
                float income = 0.0025f * res.totalNumPixelsCovered * Mathf.Pow(res.percent + .56f, 5.7f) + .2f;
                animal.completed = true;
                GameManager.instance.Money += income - priceSum;
                //convert draggable objects to static objects
                for (int i = brisks.Count - 1; i > -1; --i)
                {
                    GameObject obj = brisks[i].gameObject;
                    Destroy(obj.GetComponent<Rigidbody2D>());
                    Destroy(obj.GetComponent<Collider>());
                    Destroy(brisks[i]);
                }
                brisks.Clear();
                SetAnimal(animal);
                SFXPlayer.instance.PlayAnimalSFX(animal.type);
            }
            else
                GameManager.instance.msgBox.OpenMessageBox("your build is incomplete!");
        }
    }
    IEnumerator GetMoneyEarnedDelayed()
    {
        yield return new WaitForSeconds(0.05f);
        if (animal.HomeIsComplete())
        {
            Capturer.AnalyzeResult res = Capturer.instance.Capture(animal.transform.position);
            float income = 0.0025f * res.totalNumPixelsCovered * Mathf.Pow(res.percent + .56f, 5.7f) + .2f;
            animal.completed = true;
            GameManager.instance.Money += income - priceSum;
            //convert draggable objects to static objects
            for (int i = brisks.Count - 1; i > -1; --i)
            {
                GameObject obj = brisks[i].gameObject;
                Destroy(obj.GetComponent<Rigidbody2D>());
                Destroy(obj.GetComponent<Collider>());
                Destroy(brisks[i]);
            }
            brisks.Clear();
            SetAnimal(animal);
            SFXPlayer.instance.PlayAnimalSFX(animal.type);
        }
        else
            GameManager.instance.msgBox.OpenMessageBox("your build is incomplete!");
    }
    public void SetAnimal(Animal anim)
    {
        if (brisks == null)
            brisks = new List<DraggableObjects>();
        else
        {
            foreach(DraggableObjects e in brisks)
            {
                Destroy(e.gameObject);
            }
            brisks.Clear();
        }
        animal = anim;
        priceSum = 0;
        if (animal == null || animal.completed)
            briskPanelUI.gameObject.SetActive(false);
        else
        {
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
    public void CreateObject(DraggableObjects prefab)
    {
        if (prefab.GetRawPrice() + priceSum > GameManager.instance.Money)
        {
            GameManager.instance.msgBox.OpenMessageBox("you don't have enough money!");
            return;
        }
        GameObject ins = Instantiate(prefab.gameObject);
        ins.transform.position = animal.transform.position + new Vector3(0, 3, 0);
        DraggableObjects obj = ins.GetComponent<DraggableObjects>();
        brisks.Add(obj);
        UpdatePriceText();
        StartCoroutine(SelectObjDelayed(obj));
    }
    IEnumerator SelectObjDelayed(DraggableObjects obj)
    {
        yield return 0;
        SelectedObject = obj;
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
        editingTool.onEditted += (boundingPoints)=> { UpdatePriceText(); };
        BrickButtonManager.instance.InstantiateBrickButtons(briskInventory.brisks);
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
