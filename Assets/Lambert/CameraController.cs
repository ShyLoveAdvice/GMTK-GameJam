using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Sirenix.OdinInspector;

public class CameraController : Singleton<CameraController>
{
    public float transitionTime = 1.5f;
    public GameObject closeCamera;
    public GameObject farCamera;
    public Transform follow;

    public float lerp_time;

    CinemachineBrain cinemachineBrain;
    CinemachineVirtualCamera activeVirtualCam;
    Camera m_camera;
    float lerp_pos_speed;
    float lerp_size_speed;
    Vector3 target_lerp_pos;
    float target_lerp_size;
    bool lerping;

    public override void Awake()
    {
        base.Awake();
        m_camera = GetComponent<Camera>();
        cinemachineBrain = GetComponent<CinemachineBrain>();
        cinemachineBrain.m_DefaultBlend.m_Time = transitionTime;
    }
    private void Start() {
        activeVirtualCam = cinemachineBrain.ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>();
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.P))
        {
            ChangeToCloseCamera(follow);
        }

        if(lerping)
        {
            if(Vector3.Distance(transform.position, target_lerp_pos) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target_lerp_pos, lerp_pos_speed * Time.deltaTime);
            }

            if(Mathf.Abs(m_camera.orthographicSize - target_lerp_size) > 0.1f)
            {
                activeVirtualCam.m_Lens.OrthographicSize += lerp_size_speed * Time.deltaTime;
            }
            
            if(Vector3.Distance(transform.position, target_lerp_pos) < 0.1f && Mathf.Abs(m_camera.orthographicSize - target_lerp_size) < 0.1f)
            {
                transform.position = target_lerp_pos;
                activeVirtualCam.m_Lens.OrthographicSize = target_lerp_size;
                lerping = false;
            }

            
        }
    }
    public void ResizeNReposeCamera(Transform leftMost, Transform rightMost, float width)
    {
        target_lerp_pos = (rightMost.position + leftMost.position) / 2f;
        target_lerp_size = (rightMost.position.x - leftMost.position.x + width) * (9f/32f);

        cinemachineBrain.ActiveVirtualCamera.Follow = null;

        lerp_pos_speed = Vector3.Magnitude(target_lerp_pos - transform.position) / lerp_time;
        lerp_size_speed = (target_lerp_size - m_camera.orthographicSize) / lerp_time;

        lerping = true;
    }
    public void ChangeToFarCamera()
    {
        closeCamera.SetActive(false);
        farCamera.SetActive(true);

        lerping = false;
        activeVirtualCam = cinemachineBrain.ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>();
    }
    public void ChangeToCloseCamera(Transform follow)
    {
        farCamera.SetActive(false);
        closeCamera.SetActive(true);
        closeCamera.GetComponent<CinemachineVirtualCamera>().Follow = follow;

        lerping = false;
        activeVirtualCam = cinemachineBrain.ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>();
    }
}
