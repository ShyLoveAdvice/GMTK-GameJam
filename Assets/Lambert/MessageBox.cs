using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour
{
    public Text messageText;
    public void OpenMessageBox(string text)
    {
        messageText.text = text;
        this.gameObject.SetActive(true);
    }
    public void CloseMessageBox()
    {
        this.gameObject.SetActive(false);
    }
}
