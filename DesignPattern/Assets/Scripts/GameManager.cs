using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DesignPattern;

public class GameManager : MonoSingleton<GameManager>
{
    public string name = "SingleTonManager";

    protected override void Awake_Imp()
    {
        base.Awake_Imp();

        DontDestroyOnLoad(this);
    }
}
