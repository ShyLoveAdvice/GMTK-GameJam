using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorialCanvas;
    public GameObject[] messages;

    int messageIdx;
    private void Start()
    {
        messageIdx = 0;
        for (int i = 1; i < messages.Length; ++i)
            messages[i].gameObject.SetActive(false);
    }
    public void NextMessage()
    {
        messages[messageIdx].SetActive(false);
        messageIdx++;
        if (messageIdx == messages.Length)
        {
            Destroy(tutorialCanvas);
            return;
        }
        messages[messageIdx].SetActive(true);
    }
}
