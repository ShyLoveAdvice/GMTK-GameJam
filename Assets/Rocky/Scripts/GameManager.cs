using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public CameraController camCtrl;
    public Animal[] animals;
    public Transform follow;
    void ChooseAnimal(Vector2 mousePos)
    {
        float minDist = float.MaxValue;
        int selectedAnimal = -1;
        for (int i = 0; i < animals.Length; ++i)
        {
            float dist = (mousePos - (Vector2)animals[i].transform.position).magnitude;
            if (dist < minDist)
            {
                selectedAnimal = i;
                minDist = dist;
            }
        }
        if (selectedAnimal==0 || animals[selectedAnimal].completed)
        {
            camCtrl.ChangeToCloseCamera(animals[selectedAnimal].transform);
        }
    }
    void ChangeToFarCamera()
    {
        int numCompletedAnimal;
        for(numCompletedAnimal=0;numCompletedAnimal< animals.Length; ++numCompletedAnimal)
            if (!animals[numCompletedAnimal].completed)
                break;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            camCtrl.ChangeToFarCamera();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            camCtrl.ChangeToCloseCamera(follow);
        }
    }
}
