using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public CameraController camCtrl;
    public Transform animalParent; //used to initialize variable 'animals'

    Animal[] animals;
    int selectedAnimal;
    private float money;
    public float Money
    {
        get => money;
        set
        {
            money = value;
        }
    }
    void SelectAnimal(Animal animal)
    {
        camCtrl.ResizeNReposeCamera(animals[selectedAnimal].transform, 5);
        DraggableManager.instance.SetAnimal(animal);
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
        if (selectedAnimal == 0 || animals[selectedAnimal].completed)
        {
            SelectAnimal(animals[selectedAnimal]);
        }
        else
            selectedAnimal = -1;
    }
    void ChangeToFarCamera()
    {
        if (selectedAnimal != -1)
            DraggableManager.instance.SetAnimal(null);
        selectedAnimal = -1;
        int numCompletedAnimal;
        for(numCompletedAnimal=0;numCompletedAnimal< animals.Length; ++numCompletedAnimal)
            if (!animals[numCompletedAnimal].completed)
                break;
        numCompletedAnimal = (numCompletedAnimal / 5 + 1) * 5;
        camCtrl.ResizeNReposeCamera(animals[0].transform, animals[numCompletedAnimal].transform, 5);
    }
    private void Start()
    {
        int childCount = animalParent.childCount;
        animals = new Animal[childCount];
        for(int i = 0; i < childCount; ++i)
        {
            animals[i] = animalParent.GetChild(i).GetComponent<Animal>();
        }
        ChangeToFarCamera();
        DraggableManager.instance.SetAnimal(null);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ChangeToFarCamera();
        }
        if (Input.GetMouseButtonDown(0) && selectedAnimal == -1)
        {
            ChooseAnimal(camCtrl.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition));
        }
    }
}
