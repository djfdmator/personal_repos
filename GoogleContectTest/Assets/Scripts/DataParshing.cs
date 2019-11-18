//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

///// <summary>
///// 이것은 데이터베이스 개발자용 클래스 입니다.
///// 데이터베이스 개발자가 아니라면 변경하지 말아주세요
///// </summary>

//#if UNITY_EDITOR
//public class DataParshing : MonoBehaviour
//{

//    //CSV Parse Variable
//    public string fileName;
//    public TableTemplate<_Temp_Table_Monster> tableTemplate;
//    public TableTemplate<_Temp_Table_Weapon> tableTemplate2;
//    public TableTemplate<_Temp_Table_Armor> tableTemplate3;
//    public TableTemplate<_Temp_Table_ActiveSkill> tableTemplate4;
//    public TableTemplate<_Temp_Table_Passive> tableTemplate5;
//    public TableTemplate<_Temp_Table_ItemStat> tableTemplate6;

//    public void CSVLoad()
//    {
        
//        switch (fileName)
//        {
//            case "monster":
//                tableTemplate = new TableTemplate<_Temp_Table_Monster>(fileName);
//                tableTemplate.Load();
//                Debug.Log(tableTemplate.GetDataCount());
//                break;
//            case "weapon":
//                tableTemplate2 = new TableTemplate<_Temp_Table_Weapon>(fileName);
//                tableTemplate2.Load();
//                Debug.Log(tableTemplate2.GetDataCount());
//                break;
//            case "armor":
//                tableTemplate3 = new TableTemplate<_Temp_Table_Armor>(fileName);
//                tableTemplate3.Load();
//                Debug.Log(tableTemplate3.GetDataCount());
//                break;
//            case "active":
//                tableTemplate4 = new TableTemplate<_Temp_Table_ActiveSkill>(fileName);
//                tableTemplate4.Load();
//                Debug.Log(tableTemplate4.GetDataCount());
//                break;
//            case "passive":
//                tableTemplate5 = new TableTemplate<_Temp_Table_Passive>(fileName);
//                tableTemplate5.Load();
//                Debug.Log(tableTemplate5.GetDataCount());
//                break;
//            case "ItemStat":
//                tableTemplate6 = new TableTemplate<_Temp_Table_ItemStat>(fileName);
//                tableTemplate6.Load();
//                Debug.Log(tableTemplate6.GetDataCount());
//                break;
//        }
//    }

//    public void Change_Table()
//    {
//        switch (fileName)
//        {
//            case "monster":
//                Change_Monster_Table();
//                break;
//            case "weapon":
//                Change_Weapon_Table();
//                break;
//            case "armor":
//                Change_Armor_Table();
//                break;
//            case "active":
//                Change_ActiveSkill_Table();
//                break;
//            case "passive":
//                Change_Passive_Table();
//                break;
//            case "ItemStat":
//                Change_ItemStat_Table();
//                break;
//        }
//    }

//    void Change_ItemStat_Table()
//    {
//        //Reset Table
//        string sqlQuery = "DELETE FROM ItemStat";
//        DataTransaction.Inst.DEB_dbcmd.CommandText = sqlQuery;
//        DataTransaction.Inst.DEB_dbcmd.ExecuteNonQuery();

//        //Insert Data into Table
//        for (int i = 0; i < tableTemplate6.GetDataCount(); i++)
//        {
//            int Num = tableTemplate6.GetData(i).num;
//            int[] S = tableTemplate6.GetData(i).statPerLevel;
            
//            sqlQuery = "INSERT INTO ItemStat(Num, S1, S2, S3, S4, S5, S6, S7, S8, S9) " +
//                        "values(" + Num + "," + S[0] + "," + S[1] + "," + S[2] + "," + S[3] + "," + S[4] + "," + S[5] + "," + S[6] + "," + S[7] + "," + S[8] + ")";
//            DataTransaction.Inst.DEB_dbcmd.CommandText = sqlQuery;
//            DataTransaction.Inst.DEB_dbcmd.ExecuteNonQuery();
//        }
//        Debug.Log("Finish ItemStat Table");
//    }

//    void Change_Passive_Table()
//    {
//        //Reset Table
//        string sqlQuery = "DELETE FROM Passive";
//        DataTransaction.Inst.DEB_dbcmd.CommandText = sqlQuery;
//        DataTransaction.Inst.DEB_dbcmd.ExecuteNonQuery();

//        //Insert Data into Table
//        for (int i = 0; i < tableTemplate5.GetDataCount(); i++)
//        {
//            int Num = tableTemplate5.GetData(i).num;
//            string Name = tableTemplate5.GetData(i).name;
//            int World = tableTemplate5.GetData(i).world;
//            string Description = tableTemplate5.GetData(i).description;
//            string ImageName = tableTemplate5.GetData(i).imageName;
//            int PassiveType = (int)tableTemplate5.GetData(i).passiveType;
//            int E1 = tableTemplate5.GetData(i).statPerLV[0];
//            int E2 = tableTemplate5.GetData(i).statPerLV[1];
//            int E3 = tableTemplate5.GetData(i).statPerLV[2];
//            int E4 = tableTemplate5.GetData(i).statPerLV[3];

//            sqlQuery = "INSERT INTO Passive(Num, Name, World, Description, ImageName, PassiveType, E1, E2, E3, E4) " +
//                        "values(" + Num + ",'" + Name + "'," + World + ",'" + Description + "','" + ImageName + "'," + PassiveType + "," + E1 + "," + E2 + "," + E3 + "," + E4 + ")";
//            DataTransaction.Inst.DEB_dbcmd.CommandText = sqlQuery;
//            DataTransaction.Inst.DEB_dbcmd.ExecuteNonQuery();
//        }
//        Debug.Log("Finish Passive Table");
//    }

//    void Change_ActiveSkill_Table()
//    {
//        //Reset Table
//        string sqlQuery = "DELETE FROM ActiveSkill";
//        DataTransaction.Inst.DEB_dbcmd.CommandText = sqlQuery;
//        DataTransaction.Inst.DEB_dbcmd.ExecuteNonQuery();

//        //Insert Data into Table
//        for (int i = 0; i < tableTemplate4.GetDataCount(); i++)
//        {
//            int Num = tableTemplate4.GetData(i).num;
//            string Name = tableTemplate4.GetData(i).name;
//            string Description = tableTemplate4.GetData(i).description;
//            float MpCost = tableTemplate4.GetData(i).mpCost;
//            int Attack_Count = tableTemplate4.GetData(i).attack_Count; //공격횟수
//            float Active_Time = tableTemplate4.GetData(i).active_Time ; // 실행 속도
//            float CoolDown = tableTemplate4.GetData(i).coolDown; // 쿨타임
//            float Attack_Range = tableTemplate4.GetData(i).attack_Range; //사정거리
//            string Attack_Type = tableTemplate4.GetData(i).attack_Type;
//            float Attack_Power = tableTemplate4.GetData(i).attack_Power; //데미지
//            string ImageName = tableTemplate4.GetData(i).imageName;

//            sqlQuery = "INSERT INTO ActiveSkill(Num, Name, Description, MpCost, Attack_Count, Active_Time, CoolDown, Attack_Range, Attack_Type, Attack_Power, ImageName) " +
//                        "values(" + Num + ",'" + Name + "','" + Description + "'," + MpCost + "," + Attack_Count + "," + Active_Time + "," + CoolDown + "," + Attack_Range + ",'" + Attack_Type + "'," + Attack_Power + ",'" + ImageName + "')";
//            DataTransaction.Inst.DEB_dbcmd.CommandText = sqlQuery;
//            DataTransaction.Inst.DEB_dbcmd.ExecuteNonQuery();
//        }
//        Debug.Log("Finish Active Table");
//    }

//    void Change_Armor_Table()
//    {
//        //Reset Table
//        string sqlQuery = "DELETE FROM ArmorTable";
//        DataTransaction.Inst.DEB_dbcmd.CommandText = sqlQuery;
//        DataTransaction.Inst.DEB_dbcmd.ExecuteNonQuery();

//        //Insert Data into Table
//        for (int i = 0; i < tableTemplate3.GetDataCount(); i++)
//        {
//            int Num = tableTemplate3.GetData(i).num;
//            string Name = tableTemplate3.GetData(i).name;
//            float Hp = tableTemplate3.GetData(i).hp;
//            int Item_Value = tableTemplate3.GetData(i).item_Value;
//            string Description = tableTemplate3.GetData(i).description;
//            int Rarity = (int)tableTemplate3.GetData(i).rarity;
//            int Class = (int)tableTemplate3.GetData(i).item_Class;
//            string ImageName = tableTemplate3.GetData(i).imageName;

//            sqlQuery = "INSERT INTO ArmorTable(Num, Name, Hp, Item_Value, Description, Rarity, Class, ImageName) " +
//                        "values(" + Num + ",'" + Name + "'," + Hp + "," + Item_Value + ",'" + Description + "'," + Rarity + "," + Class + ",'" + ImageName + "')";
//            DataTransaction.Inst.DEB_dbcmd.CommandText = sqlQuery;
//            DataTransaction.Inst.DEB_dbcmd.ExecuteNonQuery();
//        }
//        Debug.Log("Finish Armor Table");
//    }

//    void Change_Weapon_Table()
//    {
//        //Reset Table
//        string sqlQuery = "DELETE FROM WeaponTable";
//        DataTransaction.Inst.DEB_dbcmd.CommandText = sqlQuery;
//        DataTransaction.Inst.DEB_dbcmd.ExecuteNonQuery();

//        //Insert Data into Table
//        for (int i = 0; i < tableTemplate2.GetDataCount(); i++)
//        {
//            int Num = tableTemplate2.GetData(i).num;
//            string Name = tableTemplate2.GetData(i).name;
//            float Damage = tableTemplate2.GetData(i).damage;
//            int Attack_Count = tableTemplate2.GetData(i).attack_Count;
//            float Attack_Range = tableTemplate2.GetData(i).attack_Range;
//            string Attack_Type = tableTemplate2.GetData(i).attack_Type;
//            float Attack_Speed = tableTemplate2.GetData(i).attack_Speed;
//            int Item_Value = tableTemplate2.GetData(i).item_Value;
//            string Description = tableTemplate2.GetData(i).description;
//            int Skill_Index = tableTemplate2.GetData(i).skill_Index;
//            int Rarity = (int)tableTemplate2.GetData(i).rarity;
//            int Class = (int)tableTemplate2.GetData(i).item_Class;
//            string ImageName = tableTemplate2.GetData(i).imageName;

//            sqlQuery = "INSERT INTO WeaponTable(Num, Name, Damage, Attack_Count, Attack_Range, Attack_Type, Attack_Speed, Item_Value, Description, Skill_Index, Rarity, Class, ImageName) " +
//                        "values(" + Num + ",'" + Name + "'," + Damage + "," + Attack_Count + "," + Attack_Range + ",'" + Attack_Type + "'," + Attack_Speed + "," + Item_Value + ",'" + Description + "'," + Skill_Index + "," + Rarity + "," + Class + ",'" + ImageName + "')";
//            DataTransaction.Inst.DEB_dbcmd.CommandText = sqlQuery;
//            DataTransaction.Inst.DEB_dbcmd.ExecuteNonQuery();
//        }
//        Debug.Log("Finish Weapon Table");
//    }

//    void Change_Monster_Table()
//    {
//        //Reset Table
//        string sqlQuery = "DELETE FROM MonsterTable";
//        DataTransaction.Inst.DEB_dbcmd.CommandText = sqlQuery;
//        DataTransaction.Inst.DEB_dbcmd.ExecuteNonQuery();

//        //Insert Data into Table
//        for (int i = 0; i < tableTemplate.GetDataCount(); i++)
//        {
//            int Num = tableTemplate.GetData(i).num;
//            int Region = (int)tableTemplate.GetData(i).region;
//            string Name = tableTemplate.GetData(i).name;
//            float Damage = tableTemplate.GetData(i).damage;
//            float Hp = tableTemplate.GetData(i).hp;
//            int Rarity = (int)tableTemplate.GetData(i).monster_Rarity;
//            int Size = (int)tableTemplate.GetData(i).size;
//            float Attack_Range = tableTemplate.GetData(i).attack_Range;
//            string Attack_Type = tableTemplate.GetData(i).attack_Type;
//            float Attack_Speed = tableTemplate.GetData(i).attack_Speed;
//            float Chase_Range = tableTemplate.GetData(i).chase_Range;
//            float Move_Speed = tableTemplate.GetData(i).move_Speed;
//            int Category = (int)tableTemplate.GetData(i).category;
//            string Description = tableTemplate.GetData(i).description;
//            string ImageName = tableTemplate.GetData(i).imageName;

//            sqlQuery = "INSERT INTO MonsterTable(Num, Region, Name, Damage, Hp, Rarity, Size, Attack_Range, Attack_Type, Attack_Speed, Chase_Range, Move_Speed, Category, Description, ImageName) " +
//                        "values(" + Num + "," + Region + ",'" + Name + "'," + Damage + "," + Hp + "," + Rarity + "," + Size + "," + Attack_Range + ",'" + Attack_Type + "'," + Attack_Speed + "," + Chase_Range + "," + Move_Speed + "," + Category + ",'" + Description + "','" + ImageName + "')";
//            DataTransaction.Inst.DEB_dbcmd.CommandText = sqlQuery;
//            DataTransaction.Inst.DEB_dbcmd.ExecuteNonQuery();
//        }
//        Debug.Log("Finish Monster Table");
//    }

//}
//#endif