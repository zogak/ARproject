using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;


public class MainUI : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void NextScene()
    {
        GameManager.manager.activate = false;
        SceneManager.LoadScene("GamePlay2");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
