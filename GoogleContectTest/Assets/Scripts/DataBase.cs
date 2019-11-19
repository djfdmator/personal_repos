using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase : MonoSingleton<DataBase>
{
    #region PlayerData

    public int _mp;
    public List<string> item;
    public List<string> passive;
    public int manajem;
    public int curStage;

    #endregion

    public string data;

    public void SaveToCloud()
    {
        //string data = string.Format("{0}", );
        GoogleClass.Inst.SaveToCloud("김동하");
    }

    public void LoadFromCloud()
    {
        GoogleClass.Inst.LoadFromCloud();
                string[] split = data.Trim().Split(',');
    }
}
