using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoSingleton<ButtonManager>
{
    public void GameStartButton()
    {
        SceneManager.LoadScene("Main");
    }
}
