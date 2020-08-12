using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : DragObject
{
    public UILabel idNumber;
    public bool isFront = true;

    public override void Awake()
    {
        base.Awake();

        idNumber = big.transform.Find("Number").GetComponent<UILabel>();

        if(isFront)
        {
            big.spriteName = "BigToken";
            idNumber.gameObject.SetActive(false);
        }
    }

    public void SetIdNumber(int _id)
    {
        idNumber.text = _id.ToString();
    }

    public void FlipButton()
    {
        isFront = !isFront;
        if(isFront)
        {
            big.spriteName = "BigToken";
            idNumber.gameObject.SetActive(false);
        }
        else
        {
            big.spriteName = "BackToken";
            idNumber.gameObject.SetActive(true);
        }
    }
}
