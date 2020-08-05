using System.Collections;
using UnityEngine;

public class Play : MonoBehaviour
{

    public float playTime = 300.0f;
    public GameObject NpcObject;
    private Coroutine Loop;

    public DialogWindow dialogWindow;
    private bool existNpc = false;

    private void Awake()
    {
        NpcObject = transform.Find("NPC").gameObject;
        dialogWindow = transform.Find("Background").Find("dialogWindow").GetComponent<DialogWindow>();
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
        yield return new WaitForSeconds(1.0f);
        while (mPlayTime <= playTime)
        {
            if (!existNpc)
            {
                existNpc = true;
                NpcObject.GetComponent<NPC>().Init();

            }
            mPlayTime += Time.deltaTime;
        }
        yield return null;
    }

    public void DisableNPC()
    {
        existNpc = false;
    }
}
