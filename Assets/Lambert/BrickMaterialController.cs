using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickMaterialController : MonoBehaviour
{
    public float lerpDuration;
    public Material BrickMat;
    public bool lerping;
    float targetValue;
    float targetSpeed;
    float lerpTimer;
    private void Update()
    {
        if (lerping)
        {
            if (lerpTimer < lerpDuration)
            {
                lerpTimer += Time.deltaTime;
                BrickMat.SetFloat("_TexturePixelSize", BrickMat.GetFloat("_TexturePixelSize") + targetSpeed * Time.deltaTime);
            }
            else
            {
                BrickMat.SetFloat("_TexturePixelSize", targetValue);
                lerping = false;
            }
        }
    }
    public void LerpBrickMat(float finalPixelSize)
    {
        lerping = true;

        targetValue = finalPixelSize;
        targetSpeed = (targetValue - BrickMat.GetFloat("_TexturePixelSize")) / lerpDuration;
        lerpTimer = 0f;
    }
}
