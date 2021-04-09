using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JoinRoom : MonoBehaviour
{
    [SerializeField]
    private InputField nicknameInput;

    [SerializeField]
    private InputField roomCodeInput;


    public void BackButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OKButton()
    {
        if (nicknameInput.text != "" && roomCodeInput.text != "")
        {
            //서버 코드와 플레이어 이름을 static 변수로 저장 -> 서버 프로그래밍 코드와 매치해보며 수정이 요구되면 알려주시거나 수정해주세요.
            GameSettings.roomCode = roomCodeInput.text;
            GameSettings.playerName = nicknameInput.text;
            SceneManager.LoadScene("WaitingRoom");
        }
        else
        {
            Debug.Log("You should fill in the blank!");
        }

    }

}
