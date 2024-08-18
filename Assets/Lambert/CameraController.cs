using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : Singleton<CameraController>
{
    public float transitionTime = 1.5f;
    public GameObject closeCamera;
    public GameObject farCamera;
    public Transform follow;
    public override void Awake()
    {
        base.Awake();

        GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time = transitionTime;
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.P))
        {
            ChangeToCloseCamera(follow);
        }
    }
    public void ChangeToFarCamera()
    {
        closeCamera.SetActive(false);
        farCamera.SetActive(true);
    }
    public void ChangeToCloseCamera(Transform follow)
    {
        farCamera.SetActive(false);
        closeCamera.SetActive(true);
        closeCamera.GetComponent<CinemachineVirtualCamera>().Follow = follow;
    }
}
