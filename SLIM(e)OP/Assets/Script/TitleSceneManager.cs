using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        //SceneManager.LoadSceneAsync("Main");
    }

    public void GameStart()
    {
        ButtonManager.Inst.GameStartButton();
    }
}
