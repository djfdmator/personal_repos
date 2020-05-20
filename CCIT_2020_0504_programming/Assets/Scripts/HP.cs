using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HP : MonoBehaviour
{
    public TextMeshProUGUI hpText;

    private void Awake()
    {
        hpText = GetComponent<TextMeshProUGUI>();
    }

    public void ChangeHPText(float _hp)
    {
        hpText.text = _hp.ToString("f1");
    }
}
