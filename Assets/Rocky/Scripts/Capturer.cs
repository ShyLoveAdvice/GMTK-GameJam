using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capturer : MonoBehaviour
{
    public Camera cam;
    public Material mat;
    public int imgWidth, imgHeight;
    [ContextMenu("capture")]
    public void Capture()
    {
        cam.gameObject.SetActive(true);
        RenderTexture rt = new RenderTexture(imgWidth, imgHeight, 16);
        rt.Create();
        cam.clearFlags = CameraClearFlags.Color;
        cam.backgroundColor = new Color(0,0,0,0);
        cam.targetTexture = rt;
        cam.cullingMask = LayerMask.GetMask("Animal");
        cam.Render();
        Texture2D animaltex = RT2Tex2D(rt);
        cam.cullingMask = LayerMask.GetMask("Brisk");
        cam.Render();
        Texture2D brisktex = RT2Tex2D(rt);
        mat.SetTexture("_MainTex", brisktex);
        cam.gameObject.SetActive(false);
        AnalyzeTex(animaltex, animaltex, brisktex);
    }
    void AnalyzeTex(Texture2D animal_inverse, Texture2D animal, Texture2D brisk)
    {
        Color[] animpixels = animal.GetPixels();
        Color[] briskpixels = brisk.GetPixels();
        Color transparent = new Color(0, 0, 0, 0);
        int overlaps = 0;
        for(int i = animpixels.Length - 1; i >= 0; --i)
        {
            if (animpixels[i] != transparent && briskpixels[i] != transparent)
                ++overlaps;
        }
        Debug.Log($"texture size=({animal.width},{animal.height}), overlaps={overlaps}");
    }
    Texture2D RT2Tex2D(RenderTexture rt)
    {
        RenderTexture.active = rt;
        Texture2D tex = new Texture2D(rt.width, rt.height, TextureFormat.ARGB32, false);
        tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        tex.Apply();
        RenderTexture.active = null;
        return tex;
    }
}
