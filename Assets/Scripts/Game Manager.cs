using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Controls controls;

    private GameObject pauseUI;

    public bool paused = false;

    private PlayerMovement player;
    
    private void Awake()
    {
        controls = new Controls();

        controls.Game.Pause.performed += context => Pause();

        pauseUI = GameObject.Find("Pause UI");

        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        pauseUI.SetActive(paused);
    }

    public void Pause()
    {
        if (!paused)
        {
            paused = true;

            Time.timeScale = 0;
        }
        else
        {
            paused = false;

            Time.timeScale = 1;
        }
    }

    public void ControllerRumble(float leftMotorIntensity, float rightMotorIntensity, float time)
    {
        if (player != null)
        {
            if (player.isGamepad)
            {
                Gamepad.current.SetMotorSpeeds(0.50f, 0.50f);
                StartCoroutine(StopControllerRumble(time));
            }
        }
    }

    private IEnumerator StopControllerRumble(float time)
    {
        yield return new WaitForSeconds(time);
        InputSystem.ResetHaptics();
    }
}
