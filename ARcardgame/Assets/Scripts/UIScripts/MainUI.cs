using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;


public class MainUI : MonoBehaviour
{
   public void CreateRoom()
    {
        SceneManager.LoadScene("CreateRoom");
    }

    public void JoinRoom()
    {
        SceneManager.LoadScene("GameAccess");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
