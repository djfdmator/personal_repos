using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private enum NPCState { None, Start, Stay, End };
    private NPCState state;

    public UISprite uiSprite;
    public Animator animator;

    public bool isMan;
    public string imageName;
    public int id;
    public string name;
    public int phoneNumber;
    public string address;
    public int stayDay;
    public string purpose;

    public Object DocPrefab;

    public GameObject doc;
    public GameObject coin;
    public GameObject key;

    private Coroutine CO_StateMachine;

    public void Awake()
    {
        DocPrefab = Resources.Load("DOC");
        uiSprite = GetComponent<UISprite>();
        animator = GetComponent<Animator>();
        gameObject.SetActive(false);
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
    }

    private void Initialize()
    {
        isMan = Random.Range(0, 2) == 0 ? true : false;
        imageName = Image();
        name = Name();
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
        doc = Instantiate(DocPrefab) as GameObject;
        doc.GetComponent<DOC>().SetData(name, address, phoneNumber, stayDay, purpose);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //TODO : 아이템 받았을때의 처리
        if (collision.GetComponent<DragObject>() != null)
        {
            if (collision.GetComponent<DragObject>().isSmall)
            {
                if (collision.CompareTag("Document"))
                {
                    doc = collision.gameObject;
                    Debug.Log("Document Collision");
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
