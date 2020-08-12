using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Title : MonoBehaviour
{

    public void PlayButton()
    {
        InitGame();
        GameManager.Instance.GotoScene(GameManager.SceneStatus.LOAD);
    }

    //게임 초기화
    private void InitGame()
    {
        GameManager.Instance.curPlayDay = 0;
    }

    public void ExitButton()
    {
        GameManager.Instance.Exit();
    }
}
