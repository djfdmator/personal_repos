using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public DialogWindow dialogWindow;

    private enum NPCState { None, Start, Stay, End };
    private NPCState state;

    public UISprite uiSprite;
    public Animator animator;

    public bool isMan;
    public string imageName;
    public int id;
    public string mName;
    public int phoneNumber;
    public string address;
    public int stayDay;
    public string purpose;

    public Object DocPrefab;
    public Object CoinPrefab;

    public GameObject doc = null;
    public GameObject coin = null;
    public GameObject key = null;

    private Coroutine CO_StateMachine;

    public void Awake()
    {
        DocPrefab = Resources.Load("DOC");
        CoinPrefab = Resources.Load("Token");
        uiSprite = GetComponent<UISprite>();
        animator = GetComponent<Animator>();
        gameObject.SetActive(false);
        dialogWindow = transform.parent.Find("Background").Find("dialogWindow").GetComponent<DialogWindow>();
    }

    private void OnEnable()
    {
        if (CO_StateMachine != null)
        {
            StopCoroutine(CO_StateMachine);
            CO_StateMachine = null;
        }

        CO_StateMachine = StartCoroutine(StateMachine());
    }

    private void OnDisable()
    {
        if (CO_StateMachine != null)
        {
            StopCoroutine(CO_StateMachine);
            CO_StateMachine = null;
        }
    }

    public void Init()
    {
        Initialize();
        transform.localPosition = new Vector3(-760.0f, 0f, 0f);
        state = NPCState.Start;
        gameObject.SetActive(true);
        dialogWindow.Message = "1^어서오세요.";
        dialogWindow.Message = "0^안녕하세요.";
    }

    private void Initialize()
    {
        doc = null;
        coin = null;
        key = null;

        isMan = Random.Range(0, 2) == 0 ? true : false;
        imageName = Image();
        mName = Name();
        address = Address();
        id = Id();
        phoneNumber = PhoneNumber();
        stayDay = Random.Range(1, 3);
        purpose = Purpose();

        uiSprite.spriteName = imageName;
    }

    IEnumerator StateMachine()
    {
        while (true)
        {
            switch (state)
            {
                case NPCState.None:
                    yield return null;
                    break;
                case NPCState.Start:
                    animator.Play("Appear");
                    yield return new WaitForSeconds(1.5f);
                    SubmitObj();
                    state = NPCState.Stay;
                    break;
                case NPCState.Stay:
                    StayCheck();
                    yield return null;
                    break;
                case NPCState.End:
                    yield return null;
                    break;
                default:
                    yield return null;
                    break;
            }
        }
    }

    private void SubmitObj()
    {
        GameObject docTemp = Instantiate(DocPrefab) as GameObject;
        docTemp.GetComponent<DOC>().SetData(mName, address, phoneNumber, stayDay, purpose);

        GameObject tokenTemp = Instantiate(CoinPrefab) as GameObject;
        tokenTemp.GetComponent<Token>().SetIdNumber(id);

    }

    private void StayCheck()
    {
        if (doc != null && doc.GetComponent<DOC>().stamp.enabled)
        {
            dialogWindow.Message = "1^안녕히 가세요.";
            if (doc.GetComponent<DOC>().stamp.spriteName == "Negative")
            {
                state = NPCState.End;
                animator.Play("Out");
                Debug.Log("손님 내쫒음");
                dialogWindow.Message = "0^이런....";
            }
            else
            {
                //if (key != null)
                //{
                state = NPCState.End;
                animator.Play("In");
                Debug.Log("손님 받음");
                dialogWindow.Message = "0^네~";
                //}
            }

        }
    }

    public void AnimEnding()
    {
        Debug.Log("AnimEnding");
        Destroy(doc);
        doc = null;
        gameObject.SetActive(false);
        transform.parent.GetComponent<Play>().DisableNPC();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //TODO : 아이템 받았을때의 처리
        if (collision.GetComponent<DragObject>() != null && collision.GetComponent<DragObject>().isSmall && !collision.GetComponent<DragObject>().isDrag)
        {
            if (collision.CompareTag("Document"))
            {
                Debug.Log("Document Collision");
                if (collision.GetComponent<DOC>().stamp.enabled)
                {
                    doc = collision.gameObject;
                    doc.SetActive(false);
                }
                else
                {
                    collision.GetComponent<TweenPosition>().PlayForward();
                }
            }

        }
    }

    #region setting variable
    string Image()
    {
        string image = string.Empty;
        image = isMan ? "Man" : "Women";
        image += Random.Range(0, 1);
        return image;
    }
    int Id()
    {
        int value = 0;
        int rand = 0;
        for (int i = 0; i < 6; i++)
        {
            rand = Random.Range(0, 10);
            value += rand * (int)Mathf.Pow(10, i);
        }
        return value;
    }
    string Name()
    {
        string name = string.Empty;
        int rand;
        if (isMan)
        {
            rand = Random.Range(0, GameManager.Instance.manNames.Length);
            name = GameManager.Instance.manNames[rand];
        }
        else
        {
            rand = Random.Range(0, GameManager.Instance.wemenNames.Length);
            name = GameManager.Instance.wemenNames[rand];
        }

        return name;
    }
    int PhoneNumber()
    {
        return 0;
    }
    string Address()
    {
        return GameManager.Instance.addressList[Random.Range(0, GameManager.Instance.addressList.Length)];
    }
    string Purpose()
    {
        return GameManager.Instance.purposeList[Random.Range(0, GameManager.Instance.purposeList.Length)];
    }
    #endregion
}
