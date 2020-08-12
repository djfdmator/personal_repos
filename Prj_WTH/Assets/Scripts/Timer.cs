using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private UILabel timer;

    private void Awake()
    {
        timer = GetComponent<UILabel>();
    }
    
    public void SetTimer(float playTime, float curTime)
    {
        if (timer != null)
        {
            float time = playTime - curTime;
            if (time <= 0f) time = 0f;
            int min = (int)time / 60;
            int sec = (int)time % 60;
            string mintext = min.ToString();
            string sectext = sec.ToString();
            if (sec < 10) sectext = "0" + sectext;
            timer.text = mintext + ":" + sectext;
        }
    }
}
