using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void ChangeScene(string _scene)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(_scene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenLink(string _link)
    {
        Application.OpenURL(_link);
    }
}
