using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGSetter : Singleton<BGSetter>
{
    Image image;
    public Sprite dayImage;
    public Sprite nightImage;
    private void Start() {
        image = GetComponent<Image>();
    }
    public void SetDayImage()
    {
        image.sprite = dayImage;
    }
    public void SetNightImage()
    {
        image.sprite = nightImage;
    }
}
