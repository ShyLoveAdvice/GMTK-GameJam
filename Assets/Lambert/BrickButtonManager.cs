using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrickButtonManager : Singleton<BrickButtonManager>
{
    public GameObject briskButtonPrefab;
    public void InstantiateBrickButtons(GameObject[] brickPrefabs)
    {
        for (int i = 0; i < brickPrefabs.Length; i++)
        {
            BriskButton briskButton = Instantiate(briskButtonPrefab).GetComponent<BriskButton>();
            briskButton.transform.SetParent(transform);
            briskButton.BriskPrefab = brickPrefabs[i];
        }
    }
}
