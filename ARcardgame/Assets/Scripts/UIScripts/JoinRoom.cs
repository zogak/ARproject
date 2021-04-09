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
            //���� �ڵ�� �÷��̾� �̸��� static ������ ���� -> ���� ���α׷��� �ڵ�� ��ġ�غ��� ������ �䱸�Ǹ� �˷��ֽðų� �������ּ���.
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
