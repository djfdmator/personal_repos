﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mobs : MonoBehaviour
{
    #region parameter

    public int Health
    {
        get { return _health; }
        set
        {
            //Clamp health between 0-100
            _health = Mathf.Clamp(value, 0, 100);

            //Post notification - health has been changed
            EventManager.Instance.PostNotification(EVENT_TYPE.HEALTH_CHANGE, this, _health);
            if(_health == 0)
            {
                EventManager.Instance.PostNotification(EVENT_TYPE.DEAD, this, _health);
            }
        }
    }

    #endregion

    #region variables
    public int _health = 100;

    static int AnimatorWalk = Animator.StringToHash("Walk");
	static int AnimatorAttack = Animator.StringToHash("Attack");
	Animator _animator;

    #endregion

	void Start()
	{
        EventManager.Instance.AddListener(EVENT_TYPE.HEALTH_CHANGE, OnEvent);
        EventManager.Instance.AddListener(EVENT_TYPE.DEAD, OnEvent);
        StartCoroutine(Animate());
	}
    private void Update()
    {
        
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

    //-------------------------------------------------------
    //Called when events happen
    public void OnEvent(EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        //Detect event type
        switch (Event_Type)
        {
            case EVENT_TYPE.HEALTH_CHANGE:
                OnHealthChange(Sender, (int)Param);
                break;
            case EVENT_TYPE.DEAD:
                OnDead(Sender, (int)Param);
                break;
        }
    }
    //-------------------------------------------------------
    //Function called when health changes
    void OnHealthChange(Component Enemy, int NewHealth)
    {
        //If health has changed of this object
        if (this.GetInstanceID() != Enemy.GetInstanceID()) return;

        
        Debug.Log("Object: " + gameObject.name + " Health is: " + NewHealth.ToString());
    }
    //-------------------------------------------------------
    //-------------------------------------------------------
    //Function called when dead
    void OnDead(Component Enemy, int NewHealth)
    {
        //If health has changed of this object
        if (this.GetInstanceID() != Enemy.GetInstanceID()) return;

        gameObject.SetActive(false);
        Debug.Log("Object: " + gameObject.name + " Health is: " + NewHealth.ToString());
    }
    //-------------------------------------------------------
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Slime")
        {
            GameObject slime = collision.gameObject;
            int damage = slime.GetComponent<PlayerController>().Damage;
            if (slime.GetComponent<PlayerController>().state_slime == State_Slime.DASH)
            {
                Health -= damage;
            }
        }
    }
}
