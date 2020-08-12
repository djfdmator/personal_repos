using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamp : DragObject
{
    public bool isRedStamp = false;
    private string stampImageName = "Admission";

    private UISprite stamp = null;
    private bool isRepresentation = false;

    public override void Awake()
    {
        base.Awake();

        if(isRedStamp)
        {
            stampImageName = "Negative";
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Stamp")
        {
            Debug.Log("StampEnter");
            stamp = collision.GetComponent<UISprite>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Stamp")
        {
            Debug.Log("StampExit");
            stamp = null;
        }
    }

    public override void DragEndEvent()
    {
        base.DragEndEvent();

        if(stamp != null)
        {
            if(!isRepresentation)
            {
                Debug.Log("stamping");
                StartCoroutine(Representation());
            }
        }
    }

    IEnumerator Representation()
    {
        isRepresentation = true;
        isStopDrag = true;
        stamp.transform.parent.parent.GetComponent<DOC>().isStopDrag = true;
        yield return null;

        transform.localPosition = stamp.transform.parent.parent.transform.localPosition + stamp.transform.localPosition + new Vector3(0f, 85f, 0f);
        string sn = big.spriteName;
        yield return new WaitForSeconds(0.5f);
        big.spriteName = sn + "1";
        stamp.enabled = true;
        if(isRedStamp)
        {
            stamp.spriteName = "Negative";
        }
        else
        {
            stamp.spriteName = "Admission";
        }
        yield return new WaitForSeconds(0.5f);
        big.spriteName = sn;

        yield return null;
        isStopDrag = false;
        stamp.transform.parent.parent.GetComponent<DOC>().isStopDrag = false;
        isRepresentation = false;
    }
}
