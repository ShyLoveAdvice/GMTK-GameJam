using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capturer : Singleton<Capturer>
{
    public Camera cam;
    public int imgWidth, imgHeight;
    public AnalyzeResult Capture(Vector2 camPosition)
    {
        cam.gameObject.SetActive(true);
        cam.transform.position = new Vector3(camPosition.x, camPosition.y, -10);
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
        cam.cullingMask = LayerMask.GetMask("AnimalInv");
        cam.Render();
        Texture2D animalInvtex = RT2Tex2D(rt);
        cam.gameObject.SetActive(false);
        return AnalyzeTex(animalInvtex, animaltex, brisktex);
    }
    AnalyzeResult AnalyzeTex(Texture2D animal_inverse, Texture2D animal, Texture2D brisk)
    {
        Color[] animInvPixels = animal_inverse.GetPixels();
        Color[] animpixels = animal.GetPixels();
        Color[] briskpixels = brisk.GetPixels();
        Color transparent = new Color(0, 0, 0, 0);
        AnalyzeResult res;
        res.totalNumPixelsCovered = 0;
        res.numPixelsCovered = 0;
        res.numPixelsTransparent = 0;
        for(int i = briskpixels.Length - 1; i >= 0; --i)
        {
            if (animInvPixels[i]==transparent && animpixels[i] == transparent)
            {
                ++res.numPixelsTransparent;
                if (briskpixels[i] != transparent)
                    ++res.numPixelsCovered;
            }
            if (briskpixels[i] != transparent)
                ++res.totalNumPixelsCovered;
        }
        res.percent = res.numPixelsCovered / res.numPixelsTransparent;
        Debug.Log($"total pixels covered={res.totalNumPixelsCovered}, transparent pixels={res.numPixelsTransparent}, num pixels covered={res.numPixelsCovered}, covered percent={res.percent}"); ;
        return res;
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
    public struct AnalyzeResult
    {
        public int totalNumPixelsCovered, numPixelsTransparent, numPixelsCovered;
        public float percent;
    }
}
