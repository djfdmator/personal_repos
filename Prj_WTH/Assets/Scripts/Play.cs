using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play : MonoBehaviour
{
    public float playTime = 300.0f;

    IEnumerator PlayLoop()
    {
        float mPlayTime = playTime;
        bool existNpc = false;
        yield return new WaitForSeconds(5.0f);
        while(mPlayTime <= 0.0f)
        {
              
            mPlayTime -= Time.deltaTime;
        }
        yield return null;
    }
}
