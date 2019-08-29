using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingSceneManager : MonoBehaviour
{
    public void Exit()
    {
        ButtonManager.Inst.Exit();
    }

    public void Restart()
    {
        ButtonManager.Inst.Restart();
    }
}
