using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class _Temp_Table_Passive
{
    public readonly int num;
    public readonly int world;
    public readonly string name;
    public readonly string description;
    public readonly string imageName;

    //효과 변수
    public readonly PASSIVE_TYPE passiveType;
    public readonly int[] statPerLV;

    public _Temp_Table_Passive(string[] inputData)
    {
        int count = 0;

        num = Convert.ToInt32(inputData[count++]);
        world = 0;
        name = inputData[count++];
        passiveType = (PASSIVE_TYPE)Enum.Parse(typeof(PASSIVE_TYPE), inputData[count++]);
        int[] tempStatPerLV = { Convert.ToInt32(inputData[count++]), Convert.ToInt32(inputData[count++]), Convert.ToInt32(inputData[count++]), Convert.ToInt32(inputData[count++]) };
        statPerLV = tempStatPerLV;
        imageName = inputData[count++];
        description = inputData[count++];
    }
}

public class _Temp_Table_ActiveSkill
{
    public readonly int num;
    public readonly string name;
    public readonly float mpCost;
    public readonly float attack_Power; //데미지
    public readonly int attack_Count; //공격횟수
    public readonly float coolDown; // 쿨타임
    public readonly float active_Time; // 실행 속도
    public readonly float attack_Range; //사정거리
    public readonly string attack_Type;
    public readonly string imageName;
    public readonly string description;

    public _Temp_Table_ActiveSkill(string[] inputData)
    {
        int count = 0;
        num = Convert.ToInt32(inputData[count++]);
        name = inputData[count++];
        mpCost = Convert.ToInt32(inputData[count++]);
        attack_Power = Convert.ToSingle(inputData[count++]);
        attack_Type = inputData[count++];
        attack_Count = Convert.ToInt32(inputData[count++]);
        coolDown = Convert.ToSingle(inputData[count++]);
        active_Time = 0.0f;
        attack_Range = 0.0f;
        imageName = inputData[count++];
        description = inputData[count++];
    }

    public _Temp_Table_ActiveSkill(_Temp_Table_ActiveSkill src)
    {
        num = src.num;
        name = src.name;
        mpCost = src.mpCost;
        attack_Power = src.attack_Power;
        attack_Type = src.attack_Type;
        attack_Count = src.attack_Count;
        coolDown = src.coolDown;
        active_Time = src.active_Time;
        attack_Range = src.attack_Range;
        imageName = src.imageName;
        description = src.description;
    }
}

public class _Temp_Table_Armor
{
    public readonly int num;
    public readonly string name;
    public readonly RARITY rarity;
    public readonly float hp;
    public readonly Item_CLASS item_Class;
    public readonly int item_Value;
    public readonly string imageName;
    public readonly string description;

    public _Temp_Table_Armor(string[] inputData)
    {
        int count = 0;
        num = Convert.ToInt32(inputData[count++]);
        name = inputData[count++];
        rarity = (RARITY)Enum.Parse(typeof(RARITY), inputData[count++]);
        hp = Convert.ToSingle(inputData[count++]);
        item_Class = Item_CLASS.Armor;
        item_Value = Convert.ToInt32(inputData[count++]);
        imageName = inputData[count++];
        description = inputData[count++];
    }

    public _Temp_Table_Armor(_Temp_Table_Armor src)
    {
        num = src.num;
        name = src.name;
        rarity = src.rarity;
        hp = src.hp;
        item_Class = src.item_Class;
        item_Value = src.item_Value;
        imageName = src.imageName;
        description = src.description;
    }
}

public class _Temp_Table_Weapon
{
    public readonly int num;
    public readonly string name;
    public readonly Item_CLASS item_Class;
    public readonly RARITY rarity;
    public readonly float damage;
    public readonly float attack_Speed;
    public readonly float attack_Range;
    public readonly int attack_Count;
    public readonly int skill_Index;
    public readonly string imageName;
    public readonly string monsterDrop; // 몬스터가 드랍하는 아이템 월드로 묶을지 함 생각해봐야함 아직 추가 안했음
    public readonly string description;
    public readonly string attack_Type; //원거린지 근거린지 설명용
    public readonly int item_Value;

    public _Temp_Table_Weapon(string[] inputData)
    {
        int count = 0;
        num = Convert.ToInt32(inputData[count++]);
        name = inputData[count++];
        item_Class = (Item_CLASS)Enum.Parse(typeof(Item_CLASS), inputData[count++]);
        rarity = (RARITY)Enum.Parse(typeof(RARITY), inputData[count++]);
        damage = Convert.ToSingle(inputData[count++]);
        attack_Speed = Convert.ToSingle(inputData[count++]);
        attack_Range = Convert.ToSingle(inputData[count++]);
        attack_Count = Convert.ToInt32(inputData[count++]);
        skill_Index = Convert.ToInt32(inputData[count++]);
        imageName = inputData[count++];
        monsterDrop = inputData[count++];
        description = inputData[count++];
        attack_Type = "test";
        item_Value = 0;
    }

    public _Temp_Table_Weapon(_Temp_Table_Weapon src)
    {
        num = src.num;
        name = src.name;
        item_Class = src.item_Class;
        rarity = src.rarity;
        damage = src.damage;
        attack_Speed = src.attack_Speed;
        attack_Range = src.attack_Range;
        attack_Count = src.attack_Count;
        skill_Index = src.skill_Index;
        imageName = src.imageName;
        monsterDrop = src.monsterDrop;
        description = src.description;
        attack_Type = "test";
        item_Value = 0;
    }
}

public class _Temp_Table_Monster
{
    public readonly int num;
    public readonly Monster_Region region; // 몬스터 출현 지역.. 맵마다 몬스터 나오는 것은 맵 정보에 넣기
    public readonly string name;
    public readonly float damage;
    public readonly float hp;
    public readonly Monster_Rarity monster_Rarity;
    public readonly Monster_Size size;
    public readonly float attack_Range;
    public readonly string attack_Type; //원거린지 근거린지 설명용
    public readonly float attack_Speed;
    public readonly float chase_Range; // 인식 거리
    public readonly float move_Speed; // 이동 속도
    public readonly Monster_Category category;
    public readonly string description;
    public readonly string imageName;
    public readonly string dropItem; // 몬스터가 드랍하는 아이템 월드로 묶을지 함 생각해봐야함 아직 추가 안했음

    public _Temp_Table_Monster(string[] inputData)
    {
        int count = 0;
        num = Convert.ToInt32(inputData[count++]);
        name = inputData[count++];
        category = (Monster_Category)Enum.Parse(typeof(Monster_Category),inputData[count++]);
        region = (Monster_Region)Enum.Parse(typeof(Monster_Region), inputData[count++]);
        monster_Rarity = (Monster_Rarity)Enum.Parse(typeof(Monster_Rarity), inputData[count++]);
        size = (Monster_Size)Enum.Parse(typeof(Monster_Size), inputData[count++]);
        hp =  Convert.ToSingle(inputData[count++]);
        damage = Convert.ToSingle(inputData[count++]);
        attack_Speed = Convert.ToSingle(inputData[count++]);
        chase_Range = Convert.ToSingle(inputData[count++]);
        attack_Range = Convert.ToSingle(inputData[count++]);
        move_Speed = Convert.ToSingle(inputData[count++]);
        imageName = inputData[count++];
        dropItem = inputData[count++];
        description = "test";
        attack_Type = "test";
    }

    public _Temp_Table_Monster(_Temp_Table_Monster src)
    {
        num = src.num;
        name = src.name;
        category = src.category;
        region = src.region;
        monster_Rarity = src.monster_Rarity;
        size = src.size;
        hp = src.hp;
        damage = src.damage;
        attack_Speed = src.attack_Speed;
        chase_Range = src.chase_Range;
        attack_Range = src.attack_Range;
        move_Speed = src.move_Speed;
        imageName = src.imageName;
        dropItem = src.dropItem;
        description = "test";
        attack_Type = "test";
    }

}

public class _TABLE_CREATURE 
{
		public readonly int nID;
		public readonly string szCreatureFile;
		public readonly string szCreatureProfile;
		public readonly string szNameFix;
        public readonly string szCreatureNickName;
		public readonly string szCreatureName_EN;
		public readonly string szCreatureName_JP;
		public readonly string szCreatureName_CN;
		public readonly string szCreatureInfo_KR;
		public readonly string szCreatureInfo_EN;
		public readonly string szCreatureInfo_JP;
		public readonly string szCreatureInfo_CN;
		public readonly string szBookInfo_KR;
		public readonly string szBookInfo_EN;
		public readonly string szBookInfo_JP;
		public readonly string szBookInfo_CN;
		public readonly int nUsePendatID;
		public readonly int nUseBook;
		public readonly int n_SkillID01;
    	public readonly int n_SkillID02;
		public readonly int n_SkillID03;
		public readonly int n_SkillID04;

        public _TABLE_CREATURE( string[] inputData )
        {
            int count = 0;
            this.nID = Convert.ToInt32(inputData[count++]);
            this.szCreatureFile = inputData[count++];
		    this.szCreatureProfile = inputData[count++];
		    this.szNameFix = inputData[count++];
            this.szCreatureNickName = inputData[count++];
		    this.szCreatureName_EN = inputData[count++];
		    this.szCreatureName_JP = inputData[count++];
		    this.szCreatureName_CN = inputData[count++];	
		    this.szCreatureInfo_KR =inputData[count++];
		    this.szCreatureInfo_EN =inputData[count++];
		    this.szCreatureInfo_JP =inputData[count++];
		    this.szCreatureInfo_CN =inputData[count++];
		    this.szBookInfo_KR = inputData[count++];
		    this.szBookInfo_EN = inputData[count++];
		    this.szBookInfo_JP = inputData[count++];
		    this.szBookInfo_CN = inputData[count++];
		    this.nUsePendatID = Convert.ToInt32(inputData[count++]);
		    this.nUseBook = Convert.ToInt32(inputData[count++]);
		    this.n_SkillID01 = Convert.ToInt32(inputData[count++]);
		    this.n_SkillID02 = Convert.ToInt32(inputData[count++]);
		    this.n_SkillID03 = Convert.ToInt32(inputData[count++]);
		    this.n_SkillID04 = Convert.ToInt32(inputData[count++]);

        }
        
        public _TABLE_CREATURE( _TABLE_CREATURE src )
        {
            this.nID = src.nID;
		    this.szCreatureFile = src.szCreatureFile;
		    this.szCreatureProfile = src.szCreatureProfile;
		    this.szNameFix = src.szNameFix;
		    this.szCreatureNickName = src.szCreatureNickName;
		    this.szCreatureName_EN = src.szCreatureName_EN;
		    this.szCreatureName_JP = src.szCreatureName_JP;
		    this.szCreatureName_CN = src.szCreatureName_CN;	
		    this.szCreatureInfo_KR = src.szCreatureInfo_KR;
		    this.szCreatureInfo_EN =src.szCreatureInfo_EN;
		    this.szCreatureInfo_JP =src.szCreatureInfo_JP;
		    this.szCreatureInfo_CN =src.szCreatureInfo_CN;
		    this.szBookInfo_KR = src.szBookInfo_KR;
		    this.szBookInfo_EN = src.szBookInfo_EN;
		    this.szBookInfo_JP = src.szBookInfo_JP;
		    this.szBookInfo_CN = src.szBookInfo_CN;
		    this.nUsePendatID = src.nUsePendatID;
		    this.nUseBook = src.nUseBook;
		    this.n_SkillID01 = src.n_SkillID01;
		    this.n_SkillID02 = src.n_SkillID02;
		    this.n_SkillID03 = src.n_SkillID03;
		    this.n_SkillID04 = src.n_SkillID04;
         }
}



