using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    private Transform playDesk;
    private Transform desk;
    public string bigImageName;
    public string smallImageName;
    Vector3 InitialPos;
    UISprite uisprite;
    UIWidget widget;
    public bool isDrag = false;
    public bool isSmall = true;

    private void Awake()
    {
        playDesk = GameObject.FindGameObjectWithTag("PlayDesk").transform;
        desk = GameObject.FindGameObjectWithTag("Desk").transform;
        InitialPos = transform.localPosition;
        uisprite = GetComponent<UISprite>();
        widget = GetComponent<UIWidget>();
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
    }

    public virtual void DragEndEvent()
    {
        isDrag = false;
    }

    public virtual void DragEvent()
    {
        transform.localPosition = Input.mousePosition - new Vector3(Screen.width/2, Screen.height/2, 0f) - transform.parent.localPosition;

        if(Input.mousePosition.x < 450)
        {
            if (transform.parent != desk)
            {
                isSmall = true;
                transform.parent = desk;
                widget.ParentHasChanged();
                uisprite.spriteName = smallImageName;
                widget.MakePixelPerfect();
            }
        }
        else
        {
            if (transform.parent != playDesk)
            {
                isSmall = false;
                transform.parent = playDesk;
                widget.ParentHasChanged();
                uisprite.spriteName = bigImageName;
                widget.MakePixelPerfect();
            }
        }
    }
}
