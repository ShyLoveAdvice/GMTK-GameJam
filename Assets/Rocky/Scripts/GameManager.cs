using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public GameObject switchAnimalButtons;
    public Transform animalParent; //used to initialize variable 'animals'
    public MessageBox msgBox;
    [Header("Camera")]
    public CameraController camCtrl;
    public float closeCamSize;
    public Vector2 closeCamPosOffset, farCamPosOffset;
    [SerializeField] float fadeTime;
    [SerializeField] Image closeBGImg;
    [Header("Money")]
    public float initialMoney;
    public TextMeshProUGUI moneyText;

    Animal[] animals;
    int selectedAnimal;
	int prevSelectedAnimal;
    Coroutine fadeCoroutine;
    private float money;
    public float Money
    {
        get => money;
        set
        {
            money = value;
            moneyText.text = "Money: " + money.ToString("F2");
        }
    }
	//switch between close and far cam
	public void SwitchBetweenCloseAndFarCam(){
		if(selectedAnimal==-1){
			selectedAnimal=prevSelectedAnimal;
			SelectAnimal(animals[selectedAnimal]);
		}
		else{
			ChangeToFarCamera();
		}
	}
	public void OnAnimalCompleted(){
		for(int i=0;i<animals.Length;++i){
			if(!animals[i].completed)
				return;
		}
		FadeCloseBGImg(0,1);
	}
    void SelectAnimal(Animal animal)
    {
		//prevSelectedAnimal=animals[selectedAnimal];
        switchAnimalButtons.SetActive(true);
        if (animal != null)
            SFXPlayer.instance.PlayAnimalSFX(animal.type);
        camCtrl.ResizeNReposeCamera(animals[selectedAnimal].transform, closeCamSize, closeCamPosOffset);
        DraggableManager.instance.SetAnimal(animal);
    }
    public void NextAnimal()
    {
        if (selectedAnimal!=-1 && selectedAnimal < animals.Length-1 && animals[selectedAnimal].completed)
        {
            ++selectedAnimal;
            SelectAnimal(animals[selectedAnimal]);
        }
    }
    public void PrevAnimal()
    {
        if (selectedAnimal > 0)
        {
            --selectedAnimal;
            SelectAnimal(animals[selectedAnimal]);
        }
    }
    void ChooseAnimal(Vector2 mousePos)
    {
        float minDist = float.MaxValue;
        selectedAnimal = -1;
        for (int i = 0; i < animals.Length; ++i)
        {
            float dist = (mousePos - (Vector2)animals[i].transform.position).magnitude;
            if (dist < minDist)
            {
                selectedAnimal = i;
                minDist = dist;
            }
        }
        if (minDist<9f && (selectedAnimal == 0 || animals[selectedAnimal-1].completed))
        {
            SelectAnimal(animals[selectedAnimal]);
        }
        else
            selectedAnimal = -1;
    }
    public void ChangeToFarCamera()
    {
        switchAnimalButtons.SetActive(false);
        if (DraggableManager.instance.SelectedObject != null)
            DraggableManager.instance.SelectedObject = null;
        if (selectedAnimal != -1){
            DraggableManager.instance.SetAnimal(null);
			prevSelectedAnimal=selectedAnimal;
		}
        selectedAnimal = -1;
        int numCompletedAnimal;
        for(numCompletedAnimal=0;numCompletedAnimal< animals.Length; ++numCompletedAnimal)
            if (!animals[numCompletedAnimal].completed)
                break;
        numCompletedAnimal = (numCompletedAnimal / 5 + 1) * 5 - 1;
		if(numCompletedAnimal>=animals.Length) numCompletedAnimal=animals.Length-1;
        camCtrl.ResizeNReposeCamera(animals[0].transform, animals[numCompletedAnimal].transform, 5, farCamPosOffset);
    }
    void FadeCloseBGImg(float beginAlpha, float endAlpha)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeCloseBGImage_Coroutine(beginAlpha, endAlpha));
    }
    IEnumerator FadeCloseBGImage_Coroutine(float beginAlpha, float endAlpha)
    {
        Color color = closeBGImg.color;
        color.a = beginAlpha;
        WaitForFixedUpdate wait = new WaitForFixedUpdate();
        int i = (int)(fadeTime / Time.fixedDeltaTime);
        float dalpha = (endAlpha-beginAlpha) / i;
        for (; i > 0; ++i)
        {
            yield return wait;
            color.a += dalpha;
            closeBGImg.color = color;
        }
        fadeCoroutine = null;
    }
    private void Start()
    {
        //get animals
        int childCount = animalParent.childCount;
        animals = new Animal[childCount];
        for(int i = 0; i < childCount; ++i)
        {
            animals[i] = animalParent.GetChild(i).GetComponent<Animal>();
        }
        ChangeToFarCamera();
        //disable draggable uis
        DraggableManager.instance.SetAnimal(null);
        //set initial money
        Money = initialMoney;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeToFarCamera();
        }
        if (Input.GetMouseButtonDown(0) && selectedAnimal == -1)
        {
            ChooseAnimal(camCtrl.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition));
        }
    }
}
