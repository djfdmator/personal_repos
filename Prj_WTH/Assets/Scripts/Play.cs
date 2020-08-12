using System.Collections;
using UnityEngine;

public class Play : MonoBehaviour
{

    public float playTime = 61.0f;
    public Timer timer;
    public GameObject NpcObject;
    private Coroutine Loop;

    public bool existNpc = false;

    private void Awake()
    {
        timer = transform.Find("Timer").GetComponent<Timer>();
        NpcObject = transform.Find("NPC").gameObject;
    }

    private void OnEnable()
    {
        Initiallize();
    }

    private void OnDisable()
    {
        StopCoroutine(Loop);
        Loop = null;
    }

    private void Initiallize()
    {
        if(Loop != null)
        {
            StopCoroutine(Loop);
            Loop = null;
        }
        Loop = StartCoroutine(PlayLoop());
    }

    IEnumerator PlayLoop()
    {
        float mPlayTime = 0f;
        timer.SetTimer(playTime, mPlayTime);
        yield return new WaitForSeconds(1.0f);
        while (mPlayTime <= playTime)
        {
            if (!existNpc)
            {
                existNpc = true;
                NpcObject.GetComponent<NPC>().Init();
            }
            mPlayTime += Time.deltaTime;
            timer.SetTimer(playTime, mPlayTime);
            yield return null;
        }
        yield return null;
    }

    public void DisableNPC()
    {
        existNpc = false;
    }
}
