using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : DragObject
{
    public UILabel idNumber;
    public bool isFront = true;

    private AudioSource audioSource;

    public override void Awake()
    {
        base.Awake();

        idNumber = big.transform.Find("Number").GetComponent<UILabel>();
        audioSource = GetComponent<AudioSource>();
        if(isFront)
        {
            big.spriteName = "BigToken";
            idNumber.gameObject.SetActive(false);
        }

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

    public void SetIdNumber(int _id)
    {
        idNumber.text = _id.ToString();
    }

    public override void DragStartEvent()
    {
        base.DragStartEvent();
        audioSource.Play();
    }

    public override void DragEndEvent()
    {
        base.DragEndEvent();
        audioSource.Play();
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
