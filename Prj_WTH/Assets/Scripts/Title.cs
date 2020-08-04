using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Title : MonoBehaviour
{

    public void PlayButton()
    {
        GameManager.Instance.GotoScene(GameManager.SceneStatus.PLAY);
    }

    public void ExitButton()
    {
        GameManager.Instance.Exit();
    }
}
