using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour
{
    public GameObject[] events;

    //private Queue<Transform> curEvents2 = new Queue<Transform>();


    public List<Transform> curEvents = new List<Transform>();
    public int curEventIndex = 0;

    private void Awake()
    {
        events = new GameObject[GameManager.Instance.playDay];
        for(int i = 0; i < GameManager.Instance.playDay; i++)
        {
            events[i] = transform.Find("Event" + (i + 1)).gameObject;
        }
    }

    private void OnEnable()
    {
        curEventIndex = 0;
        for (int i = 0; i < events[GameManager.Instance.curPlayDay].transform.childCount; i++)
        {
            curEvents.Add(events[GameManager.Instance.curPlayDay].transform.Find((i + 1).ToString()));
            if (i != 0) curEvents[i].gameObject.SetActive(false);
        }
    }

    public void NextButton()
    {
        if(curEvents[curEventIndex].GetComponentInChildren<TypewriterEffect>() != null)
        {
            TypewriterEffect typewriterEffect = curEvents[curEventIndex].GetComponentInChildren<TypewriterEffect>();
            if(typewriterEffect.isActive)
            {
                typewriterEffect.Finish();
                return;
            }
        }

        curEvents[curEventIndex].gameObject.SetActive(false);
        curEventIndex++;

        if(curEvents.Count == curEventIndex)
        {
            SkipButton();
        }
        else
        {
            curEvents[curEventIndex].gameObject.SetActive(true);
        }
    }

    public void SkipButton()
    {
        GameManager.Instance.curPlayDay++;
        GameManager.Instance.GotoScene(GameManager.SceneStatus.PLAY);
    }
}
