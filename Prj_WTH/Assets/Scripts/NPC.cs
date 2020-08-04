using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Animator animator;

    public bool isMan;
    public string imageName;
    public int id;
    public string name;
    public int phoneNumber;
    public string address;
    public int stayDay;
    public string purpose;

    public void Awake()
    {
        animator = GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    public void Init()
    {
        Initialize();
        transform.localPosition = new Vector3(-760.0f, 0f, 0f);

        gameObject.SetActive(true);
        Debug.Log("gma");
    }

    private void Initialize()
    {
        isMan = Random.Range(0, 2) == 0 ? true : false;
        imageName = Image();
        name = Name();
        address = Address();
        id = Id();
        phoneNumber = PhoneNumber();
        stayDay = Random.Range(1, 3);
        purpose = Purpose();
    }
    #region aaa
    string Image()
    {
        string image = string.Empty;
        image = isMan ? "Man" : "Women";
        image += Random.Range(0, 1);
        return image;
    }
    int Id()
    {
        int value = 0;
        int rand = 0;
        for (int i = 0; i < 6; i++)
        {
            rand = Random.Range(0, 10);
            value += rand * (int)Mathf.Pow(10, i);
        }
        return value;
    }
    string Name()
    {
        string name = string.Empty;
        int rand;
        if(isMan)
        {
            rand = Random.Range(0, GameManager.Instance.manNames.Length);
            name = GameManager.Instance.manNames[rand];
        }
        else
        {
            rand = Random.Range(0, GameManager.Instance.wemenNames.Length);
            name = GameManager.Instance.wemenNames[rand];
        }

        return name;
    }
    int PhoneNumber()
    {
        return 0;
    }
    string Address()
    {
        return GameManager.Instance.addressList[Random.Range(0, GameManager.Instance.addressList.Length)];
    }
    string Purpose()
    {
        return GameManager.Instance.purposeList[Random.Range(0, GameManager.Instance.purposeList.Length)];
    }
    #endregion
}
