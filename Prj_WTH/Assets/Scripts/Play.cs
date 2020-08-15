using System.Collections;
using UnityEngine;

public class Play : MonoBehaviour
{

    public float playTime = 61.0f;
    public float fadeTime = 0f;
    public Timer timer;
    public GameObject NpcObject;
    public GameObject dayStart;
    public GameObject dayStartButton;

    TypewriterEffect typewriterEffect;
    private Coroutine Loop;

    public bool existNpc = false;

    private void Awake()
    {
        timer = transform.Find("Timer").GetComponent<Timer>();
        NpcObject = transform.Find("NPC").gameObject;
        dayStart = transform.Find("DayStart").gameObject;
        dayStartButton = dayStart.transform.Find("Button").gameObject;
        typewriterEffect = dayStart.GetComponentInChildren<TypewriterEffect>();
    }

    private void OnEnable()
    {
        typewriterEffect.GetComponent<UILabel>().text = "호텔 카운터 업무 " + GameManager.Instance.curPlayDay + "일차....";
    }

    public void GameStart()
    {
        if (!typewriterEffect.isActive)
        {
            dayStartButton.SetActive(false);
            Initiallize();
        }
    }

    private void OnDisable()
    {
        if (Loop != null)
        {
            StopCoroutine(Loop);
            Loop = null;
        }
        dayStart.GetComponent<UIPanel>().alpha = 1.0f;
        dayStart.SetActive(true);
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
        float mFadeTime = 0f;
        UIPanel dayStartPanel = dayStart.GetComponent<UIPanel>();
        while(mFadeTime <= fadeTime)
        {
            mFadeTime += Time.deltaTime;
            dayStartPanel.alpha = (fadeTime - mFadeTime) / fadeTime;
            yield return null;
        }
        dayStart.SetActive(false);

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
