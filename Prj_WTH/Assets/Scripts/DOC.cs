using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOC : DragObject
{
    public UILabel name;
    public UILabel address;
    public UILabel phoneNumber;
    public UILabel stayDate;
    public UILabel purpose;
    //public Transform purpose;
    public UISprite stamp;

    public override void Awake()
    {
        base.Awake();

        name = big.transform.Find("Name").GetComponent<UILabel>();
        address = big.transform.Find("Address").GetComponent<UILabel>();
        phoneNumber = big.transform.Find("PhoneNumber").GetComponent<UILabel>();
        stayDate = big.transform.Find("StayDay").GetComponent<UILabel>();
        purpose = big.transform.Find("Purpose").GetComponent<UILabel>();
        stamp = big.transform.Find("Stamp").GetComponent<UISprite>();

        stamp.gameObject.SetActive(false);

        if (transform.parent != desk)
        {
            transform.parent = desk;
        }
        transform.localScale = Vector3.one;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    private void Start()
    {
        Invoke("ColliderOn", 1.0f);
    }

    public void ColliderOn()
    {
        GetComponent<BoxCollider2D>().enabled = true;
    }

    public void SetData(string _name, string _address, int _phoneNumber, int _stayDate, string _purpose)
    {
        name.text = _name;
        address.text = _address;
        phoneNumber.text = _phoneNumber.ToString();
        stayDate.text = _stayDate == 1 ? "1박 2일" : "2박 3일";
        purpose.text = _purpose;
    }
}
