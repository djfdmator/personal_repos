using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Title : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton()
    {
        GameManager.Instance.GotoScene(GameManager.SceneStatus.PLAY);
    }

    public void ExitButton()
    {
        GameManager.Instance.Exit();
    }
}
