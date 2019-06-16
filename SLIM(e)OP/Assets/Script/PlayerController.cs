using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State_Slime
{
    NONE,
    IDLE,
    MOVE,
    DASH
}

public class PlayerController : MonoBehaviour
{
    #region parameter
    public float Charge
    {
        get { return _charge; }
        set
        {
            _charge = value;

            if (_charge >= 1200.0f)
            {
                 _charge = 1200.0f;
            }

            power_bar.value = _charge / 1200.0f;
        }
    }

    public float HP
    {
        get { return _hp; }
        set
        {
            _hp = value;
            hp_bar.value = _hp / 100.0f;

            if (_hp <= 0.0f)
            {
                Debug.Log("Die");
            }
        }
    }

    #endregion

    #region variable
    public float _charge = 0.0f;
    public float _hp = 100.0f;

    //외부 참조 오브젝트 컴포넌트
    private Animator player_anime;
    public UIProgressBar hp_bar;
    public UIProgressBar power_bar;

    //move speed
    public float speed = 0.0f;
    // if you start power charge, you get low move speed
    public float chargeSpeed;
    // if value == 1.0f, start move anime
    public float anime_move = 0.0f;
    //이동 가능 조건
    public Vector2 moveInput;

    //dash forward
    public Vector3 mouse;
    public Vector2 DashWay;
    public GameObject DashVFX;
    public int DashFoward = 1;

    //animator 관련 변수
    int Walk = Animator.StringToHash("Walk");
    int Dash = Animator.StringToHash("Dash");

    //오브젝트 컴포넌트 변수 모음
    private Rigidbody2D slime_Rigid;
    private Animator animator;

    //캐릭터의 현재 상태
    public State_Slime state_slime = State_Slime.NONE;
    #endregion

    private void Awake()
    {
        animator = GetComponent<Animator>();
        slime_Rigid = GetComponent<Rigidbody2D>();
        player_anime = GetComponent<Animator>();
        DashVFX = transform.GetChild(0).gameObject;
        
        hp_bar.value = 1.0f;
        power_bar.value = 0.0f;
        power_bar.gameObject.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(Animate());
    }

    //애니메이션 작동
    IEnumerator Animate()
    {
        while (true)
        {
            switch (state_slime)
            {
                case State_Slime.IDLE:
                    animator.SetBool(Walk, false);
                    yield return null;//new WaitForSeconds(0.2f);
                    break;
                case State_Slime.MOVE:
                    animator.SetBool(Walk, true);
                    yield return null;// new WaitForSeconds(0.2f);
                    break;
                case State_Slime.DASH:
                    animator.SetTrigger(Dash);
                    yield return null;// new WaitForSeconds(0.2f);
                    break;
            }
            yield return null;
        }
    }
    private void Update()
    {
        Slime_State();
    }

    private void Slime_State()
    {
        if (slime_Rigid.velocity.magnitude <= 0.1f)
        {
            state_slime = State_Slime.IDLE;
        }
        else if (0.1f < slime_Rigid.velocity.magnitude && slime_Rigid.velocity.magnitude <= 0.2f)
        {
            state_slime = State_Slime.MOVE;
        }
        else if (0.2f < slime_Rigid.velocity.magnitude)
        {
            state_slime = State_Slime.DASH;
        }
    }

    void FixedUpdate()
    {
        //게임상에서 마우스의 위치를 계산한다.
        mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);
        DashWay = castPoint.origin-transform.position;

        chargeSpeed = 1.0f;

        //키 입력
        InputKey();
    }

    void InputKey()
    {
        //key == Space bar
        if (Input.GetKey(KeyCode.Space))
        {
            //대쉬 방향 표시 회전각 계산
            DashVFX.SetActive(true);
            float angle = Mathf.Atan2(DashWay.y, DashWay.x) * Mathf.Rad2Deg;
            DashVFX.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            DashVFX.transform.localScale = new Vector3(DashFoward * (_charge / 1200.0f), 0.8f, 0.0f);

            //대쉬 파워 게이지바 
            power_bar.gameObject.SetActive(true);
            Charge += 20.0f;
            chargeSpeed = 2.0f;
        }

        // key == WASD
        if (state_slime != State_Slime.DASH)
        {
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            slime_Rigid.velocity = moveInput.normalized * speed / chargeSpeed * Time.deltaTime;

            if(Input.GetAxisRaw("Horizontal") < 0.0f)
            {
                transform.localScale = new Vector3(-1.0f, 1.0f, 0.0f);
                DashFoward = -1;
            }
            else if(Input.GetAxisRaw("Horizontal") > 0.0f)
            {
                transform.localScale = new Vector3(1.0f, 1.0f, 0.0f);
                DashFoward = 1;
            }
        }

        //key up Space bar - 대쉬
        if (Input.GetKeyUp(KeyCode.Space))
        {
            DashVFX.SetActive(false);
            power_bar.gameObject.SetActive(false);
            slime_Rigid.AddForce(DashWay.normalized * _charge, ForceMode2D.Force);
            Charge = 0.0f;
        }

        //이동을 천천히 멈춘다.
        slime_Rigid.velocity = Vector3.Slerp(slime_Rigid.velocity, Vector2.zero, 5.0f * Time.deltaTime);

        //stop move
        //if (Input.GetKey(KeyCode.E))
        //{
        //    slime_Rigid.velocity = Vector2.zero;
        //}
    }
}
