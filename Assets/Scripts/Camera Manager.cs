using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using TreeEditor;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera currentCam; //Make sure this variable is always the main camera that has the highest priority

    private CinemachineBasicMultiChannelPerlin perlin;

    public CinemachineVirtualCamera mainCam;

    private void Start()
    {
        //Set the current cam
        currentCam = mainCam;
        currentCam.Priority = 100;
    }

    public void ScreenShake(float amplitude, float frequency, float length)
    {
        perlin = currentCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        perlin.m_AmplitudeGain = amplitude;
        perlin.m_FrequencyGain = frequency;

        StartCoroutine(ShakeTimer(length));
    }

    public void ChangeCamera(CinemachineVirtualCamera oldCam, CinemachineVirtualCamera cam)
    {
        oldCam.Priority = 0;
        cam.Priority = 100;

        currentCam = cam;
    }

    private IEnumerator ShakeTimer(float length)
    {
        yield return new WaitForSeconds(length);

        //Reset noise
        perlin.m_AmplitudeGain = 0;
        perlin.m_FrequencyGain = 0;
    }
}
