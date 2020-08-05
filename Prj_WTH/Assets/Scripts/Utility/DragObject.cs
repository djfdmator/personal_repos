﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    protected Transform playDesk;
    protected Transform desk;
    protected Vector3 InitialPos;
    private Vector3 padding = Vector3.zero;

    private BoxCollider2D col;
    protected UISprite small;
    protected UISprite big;

    public bool isDrag = false;
    public bool isSmall = true;

    public virtual void Awake()
    {
        playDesk = GameObject.FindGameObjectWithTag("PlayDesk").transform;
        desk = GameObject.FindGameObjectWithTag("Desk").transform;
        InitialPos = transform.localPosition;

        col = GetComponent<BoxCollider2D>();
        small = transform.Find("Small").GetComponent<UISprite>();
        big = transform.Find("Big").GetComponent<UISprite>();

        if(isSmall)
        {
            big.gameObject.SetActive(false);
        }
        else
        {
            small.gameObject.SetActive(false);
        }
    }

    void OnDragStart()
    {
        DragStartEvent();
    }

    void OnDragEnd()
    {
        DragEndEvent();
    }

    void OnDrag()
    {
        DragEvent();
    }

    public virtual void DragStartEvent()
    {
        isDrag = true;
        padding = transform.localPosition - (Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 0f) - transform.parent.localPosition);
    }

    public virtual void DragEndEvent()
    {
        isDrag = false;
        padding = Vector3.zero;
    }

    public virtual void DragEvent()
    {
        transform.localPosition = Input.mousePosition - new Vector3(Screen.width/2, Screen.height/2, 0f) - transform.parent.localPosition + padding;

        if(Input.mousePosition.x < 450)
        {
            if (transform.parent != desk)
            {
                isSmall = true;
                transform.parent = desk;
                transform.localPosition = Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 0f) - transform.parent.localPosition;
                padding = transform.localPosition - (Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 0f) - transform.parent.localPosition);
                small.gameObject.SetActive(true);
                big.gameObject.SetActive(false);
                UIWidget wid = small.GetComponent<UIWidget>();
                col.size = new Vector2(wid.width, wid.height);
            }
        }
        else
        {
            if (transform.parent != playDesk)
            {
                isSmall = false;
                transform.parent = playDesk;
                small.gameObject.SetActive(false);
                big.gameObject.SetActive(true);
                UIWidget wid = big.GetComponent<UIWidget>();
                col.size = new Vector2(wid.width, wid.height);
            }
        }
    }
}
