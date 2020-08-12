using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour
{
    private bool isTweenning = false;
    public bool isFrom = true;
    public float tweenPlayTime;
    public Transform tweenTarget;
    public Vector3 from = Vector3.zero;
    public Vector3 to = Vector3.zero;

    public AudioSource audioSource;
    public AudioClip drawerClose;
    public AudioClip drawerOpen;

    public GameObject[] taps = new GameObject[5]; 
    public GameObject[] keys = new GameObject[5];
    public int curTap = 0;

    private void Awake()
    {
        Transform tap = tweenTarget.Find("Taps");
        Transform key = tweenTarget.Find("Keys");
        for (int i = 0; i < 5; i ++)
        {
            taps[i] = tap.Find((i + 1) + "Floor").gameObject;
            taps[i].GetComponent<BoxCollider2D>().offset = new Vector2(-15f, 0f);
            taps[i].GetComponent<BoxCollider2D>().size = new Vector2(20f, 84f);
            keys[i] = key.Find((i + 1) + "Floor").gameObject;
            keys[i].SetActive(false);
        }
        OnOffTaps(false);
    }

    void Start()
    {
        tweenTarget.localPosition = from;
        audioSource = GetComponent<AudioSource>();
    }

    public void ToggleTaps(string _name)
    {
        int n = curTap - 1;
        if (curTap != 0)
        {
            taps[n].transform.localPosition = new Vector3(30f, taps[n].transform.localPosition.y, 0f);
            taps[n].GetComponent<BoxCollider2D>().offset = new Vector2(-15f, 0f);
            taps[n].GetComponent<BoxCollider2D>().size = new Vector2(20f, 84f);
            keys[n].SetActive(false);
        }
        curTap = int.Parse(_name.ToCharArray()[0].ToString());
        n = curTap - 1;
        taps[n].transform.localPosition = new Vector3(0f, taps[n].transform.localPosition.y, 0f);
        taps[n].GetComponent<BoxCollider2D>().offset = new Vector2(0f, 0f);
        taps[n].GetComponent<BoxCollider2D>().size = new Vector2(50f, 84f);
        keys[n].SetActive(true);
    }

    private void OnOffTaps(bool isOn)
    {
        for (int i = 0; i < 5; i++)
        {
            taps[i].GetComponent<BoxCollider2D>().enabled = isOn;
        }
    }

    public void ToggleDraw()
    {
        if (!isTweenning)
        {
            isTweenning = true;
            if (isFrom)
            {
                audioSource.clip = drawerOpen;
                audioSource.Play();
                StartCoroutine(TweenPosition(from, to));
                OnOffTaps(true);
            }
            else
            {
                audioSource.clip = drawerClose;
                audioSource.Play();
                StartCoroutine(TweenPosition(to, from));
                OnOffTaps(false);
            }
            isFrom = !isFrom;
        }
    }

    IEnumerator TweenPosition(Vector3 from, Vector3 to)
    {
        float time = 0;
        while (time <= tweenPlayTime)
        {
            time += Time.deltaTime;
            tweenTarget.localPosition = Vector3.Lerp(from, to, time / tweenPlayTime);
            yield return null;
        }

        yield return null;
        isTweenning = false;
    }
}
