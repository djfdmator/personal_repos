using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manual : MonoBehaviour
{
    public enum Rule { a, b, c, d };

    public string headstring;
    private UILabel manualLabel;

    private void Awake()
    {
        manualLabel = transform.Find("ManualLabel").GetComponent<UILabel>();
    }

    public void SetManualText(Rule rule)
    {
        switch (rule)
        {
            case Rule.a:
                A();
                break;
            case Rule.b:
                break;
            case Rule.c:
                break;
            case Rule.d:
                break;
        }
    }

    void A()
    {
        manualLabel.text = headstring + "a";
    }
    void B()
    {

    }
    void C()
    {

    }
    void D()
    {

    }
}

