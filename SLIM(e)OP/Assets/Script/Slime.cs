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

    #endregion

    void Awake()
    { 
        _animator = GetComponentInChildren<Animator>();

        player = GameObject.FindGameObjectWithTag("Player");

    }

    private void Start()
    {
        Animate();
    }

    private void Update()
    {
        //가만 있을까? 움직일까?ㅇ..
        switch (state)
        {
            case Monster_State.IDLE:
                break;
            case Monster_State.MOVE:
                break;
            case Monster_State.ATTACK:
                break;

        }

        
    }

    private void FixedUpdate()
    {
        currentDistance = Vector2.Distance(transform.position, player.transform.position);
        _animator.SetFloat("distanceFromPlayer", currentDistance);
    }

    IEnumerator Animate()
    {
        yield return new WaitForSeconds(5f);
        while (true) 
        {
            _animator.SetBool(AnimatorWalk, true);
            yield return new WaitForSeconds(1f);

            _animator.transform.localScale = new Vector3(-1, 1, 1);
            yield return new WaitForSeconds(1f);

            _animator.SetBool(AnimatorWalk, false);
            yield return new WaitForSeconds(1f);

            _animator.SetTrigger(AnimatorAttack);
            yield return new WaitForSeconds(1f);

            _animator.SetTrigger(AnimatorAttack); 
            yield return new WaitForSeconds(1f);

            _animator.SetTrigger(AnimatorAttack);
            yield return new WaitForSeconds(5f);
        }
    }

}
