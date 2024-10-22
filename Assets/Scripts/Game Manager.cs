using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Controls controls;

    private GameObject pauseUI;

    public bool paused = false;
    
    private void Awake()
    {
        controls = new Controls();

        controls.Game.Pause.performed += context => Pause();

        pauseUI = GameObject.Find("Pause UI");
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
}
