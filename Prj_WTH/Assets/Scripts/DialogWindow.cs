using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogWindow : MonoBehaviour
{
    public GameObject dialogPrefab;
    public string Message
    {
        set
        {
            dialogQueue.Enqueue(value);
        }
    }

    int diaCount = 0;
    string dia;
    string imageName;
    string[] sp_dia;
    float defaultHeight = 180f;
    float deleteHeight = 250f;
    Vector3 createPos;
    public List<GameObject> dialogs = new List<GameObject>(); 
    Queue<string> dialogQueue = new Queue<string>();
    Coroutine Loop = null;

    private void Awake()
    {
        dialogPrefab = Resources.Load<GameObject>("dialog");
    }

    private void OnEnable()
    {
        if (Loop != null)
        {
            Loop = null;
        }
        Loop = StartCoroutine(DialogLoop());
    }

    private void OnDisable()
    {
        StopCoroutine(Loop);
        Loop = null;

        for(int i = 0; i < dialogs.Count; i++)
        {
            Destroy(dialogs[i]);
        }
        dialogs.Clear();
        dialogQueue.Clear();
    }

    IEnumerator DialogLoop()
    {
        while (true)
        {
            if(dialogQueue.Count > 0)
            {
                dia = dialogQueue.Dequeue();
                sp_dia = dia.Split('^');
                if(sp_dia[0] == "0")
                {
                    //left
                    createPos = new Vector3(-70, defaultHeight - (diaCount * 50f), 0.0f);
                    imageName = "LeftDialog";
                }
                else
                {
                    //right
                    createPos = new Vector3(70, defaultHeight - (diaCount * 50f), 0.0f);
                    imageName = "RightDialog";
                }
                diaCount++;

                dialogPrefab.GetComponent<UISprite>().spriteName = imageName;
                dialogPrefab.GetComponentInChildren<UILabel>().text = sp_dia[1];
                GameObject obj = Instantiate(dialogPrefab, transform);
                obj.transform.localPosition = createPos;
                dialogs.Add(obj);
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                if (dialogs.Count > 0)
                {
                    float maxTime = 1.0f;
                    float time = 0f;
                    while (maxTime > time)
                    {
                        time += Time.deltaTime;
                        float dt = Time.deltaTime;
                        for(int i = 0; i < dialogs.Count; i++)
                        {
                            dialogs[i].transform.localPosition = new Vector3(dialogs[i].transform.localPosition.x, dialogs[i].transform.localPosition.y + (50 * dt / maxTime), 0f);
                            if (dialogs[i].transform.localPosition.y >= deleteHeight)
                            {
                                Destroy(dialogs[i]);
                                dialogs.RemoveAt(i);
                                diaCount--;
                            }
                        }
                        yield return null;
                    }
                }
            }
            yield return new WaitForSeconds(1.0f);
        }
    }

    IEnumerator ElevateDialog()
    {
        float maxTime = 1.0f;
        float time = 0f;
        while(maxTime > time)
        {
            time += Time.deltaTime;
            float dt = Time.deltaTime;
            foreach(GameObject obj in dialogs)
            {
                obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, obj.transform.localPosition.y + (50 * dt / maxTime), 0f);
            }
        }
        yield return null;
    }

}
