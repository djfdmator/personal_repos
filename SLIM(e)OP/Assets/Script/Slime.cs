using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Monster
{
    #region variable

    static int AnimatorWalk = Animator.StringToHash("Walk");
    static int AnimatorAttack = Animator.StringToHash("Attack");
    Animator _animator;

    private Monster_State state = Monster_State.NONE;

    public float action_intervalTime = 0.0f;

    GameObject player;
    public float currentDistance;
    public int rand_IdleBehaviour = 0; // 0 = idle, 1 = move ;; animation
    Vector2 Forward;
    Rigidbody2D rigid;

    #endregion

    void Awake()
    {
        _animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();

        player = GameObject.FindGameObjectWithTag("Player");

    }

    private void Start()
    {
        StartCoroutine("Animate");
    }

    private void FixedUpdate()
    {
        currentDistance = Vector2.Distance(transform.position, player.transform.position);
        _animator.SetFloat("distance", currentDistance);

        if (currentDistance <= 1.0f)
        {
            //Attack
            state = Monster_State.ATTACK;
        }
        else if (1.0f < currentDistance && currentDistance <= 2.5f)
        {
            //Chase
            state = Monster_State.CHASE;
            Vector2 forward = player.transform.position - transform.position;
            rigid.velocity = forward * 1.2f;
        }
        else
        {
            //Idle
            state = Monster_State.IDLE;
            if (rand_IdleBehaviour == 0)
            {
                //Idle
            }
            else
            {
                //Move
                rigid.velocity = Forward * 1.2f;
            }
        }
    }

    Vector2 RandomMoveForward2D()
    {
        //need fixed circle range;; 
        float dir_x = Random.Range(-2, 2);
        float dir_y = Random.Range(-2, 2);

        return new Vector2(dir_x, dir_y);
    }

    IEnumerator Animate()
    {

        //가만 있을까? 움직일까?ㅇ..
        while (true)
        {
            switch (state)
            {
                case Monster_State.IDLE:
                    if (rand_IdleBehaviour == 0)
                    {
                        //Idle
                        _animator.SetBool("move", false);
                        yield return new WaitForSeconds(2f);
                    }
                    else
                    {
                        //Move
                        _animator.SetBool("move", true);
                        yield return new WaitForSeconds(2f);
                    }
                    rand_IdleBehaviour = Random.Range(0, 2);
                    Forward = RandomMoveForward2D();
                    Debug.Log(rand_IdleBehaviour);
                    break;
                case Monster_State.CHASE:
                    _animator.SetBool("move", true);
                    yield return new WaitForSeconds(1f);
                    break;
                case Monster_State.ATTACK:
                    _animator.SetBool("move", false);
                    player.GetComponent<PlayerController>().HP -= 5.0f;
                    yield return new WaitForSeconds(1f);
                    break;

            }
            yield return null;
        }

    }

}
