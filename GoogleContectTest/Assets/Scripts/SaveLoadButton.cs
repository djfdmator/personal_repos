using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SaveLoadButton : MonoBehaviour
{

    public void OnSaveGame()
    {
        DataBase.Inst.SaveToCloud();

    }

    public void OnLoadGame()
    {
        DataBase.Inst.LoadFromCloud();
    }
}
