using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreateRoomUI : MonoBehaviour
{
    [SerializeField]
    private InputField nicknameInput;

    [SerializeField]
    private List<Button> playerCountButtons;

    [SerializeField]
    private List<Button> roundCountButtons;

    private CreateGameRoomData roomData;

    // Start is called before the first frame update
    void Start()
    {
        roomData = new CreateGameRoomData() { playerCount = 2, roundCount = 1 };

    }


    public void UpdatePlayerCount(int count)
    {
        //roomData 중 플레이어 수
        roomData.playerCount = count;

        for (int i = 0; i < playerCountButtons.Count; i++)
        {
            if (i == count - 2)
            {
                playerCountButtons[i].image.color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                playerCountButtons[i].image.color = new Color(1f, 1f, 1f, 0f);
            }
        }
    }
        
    public void UpdateRoundCount(int count)
    {
        //roomData 중 라운드 수
        roomData.roundCount = count;

        int listCount = 0;

        if(count == 1)
        {
            listCount = 0;
        }else if(count == 3)
        {
            listCount = 1;
        }else if(count == 5)
        {
            listCount = 2;
        }

        for (int i = 0; i < roundCountButtons.Count; i++)
        {
            if (i == listCount)
            {
                roundCountButtons[i].image.color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                roundCountButtons[i].image.color = new Color(1f, 1f, 1f, 0f);
            }
        }
    }
    
    public void BackButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OKButton()
    {
        if(nicknameInput.text != "")
        {
            //플레이어 이름 저장
            GameSettings.playerName = nicknameInput.text;
            SceneManager.LoadScene("WaitingRoom");
        }
        else
        {
            Debug.Log("You should make your nickname!");
        }
        
    }

    //만들 서버(방)과 관련된 정보
    public class CreateGameRoomData
    {
        public int playerCount;
        public int roundCount;
    }
}
