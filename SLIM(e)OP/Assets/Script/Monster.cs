using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Monster_State
{
    NONE,
    IDLE,
    CHASE,
    ATTACK
}

public class Monster : MonoBehaviour
{
    #region parameter

    public float Health
    {
        get { return _health; }
        set
        {
            if (!firstHit)
            {
                hpBar.parent.gameObject.SetActive(true);
                firstHit = true;
            }
            //Clamp health between 0-100
            _health = Mathf.Clamp(value, 0, _health);

            EventManager.Instance.PostNotification(EVENT_TYPE.HEALTH_CHANGE, this, _health);
            if (_health == 0)
            {
                EventManager.Instance.PostNotification(EVENT_TYPE.DEAD, this, _health);
            }
        }
    }

    #endregion

    #region variables
    public float _health = 100;
    private float maxHealth;

    private bool firstHit = false;
    private Transform hpBar;
    public bool isGroggy = false;

    private DamageEffect damageEffect;
    #endregion

    public virtual void Start()
    {
        damageEffect = GameObject.FindGameObjectWithTag("damage").GetComponent<DamageEffect>();
        hpBar = transform.Find("HpBar").Find("bar");
        hpBar.parent.gameObject.SetActive(false);

        maxHealth = Health;
        EventManager.Instance.AddListener(EVENT_TYPE.HEALTH_CHANGE, OnEvent);
        EventManager.Instance.AddListener(EVENT_TYPE.DEAD, OnEvent);
    }

    //-------------------------------------------------------
    //Called when events happen
    public void OnEvent(EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        //Detect event type
        switch (Event_Type)
        {
            case EVENT_TYPE.HEALTH_CHANGE:
                OnHealthChange(Sender, (float)Param);
                break;
            case EVENT_TYPE.DEAD:
                OnDead(Sender, (float)Param);
                break;
        }
    }
    //-------------------------------------------------------
    //Function called when health changes
    void OnHealthChange(Component Enemy, float NewHealth)
    {
        //If health has changed of this object
        if (this.GetInstanceID() != Enemy.GetInstanceID()) return;
        float ScaleX = _health / maxHealth;
        hpBar.localScale = new Vector3(ScaleX, 1.0f, 1.0f);
        isGroggy = true;
        damageEffect.OnDamageEffect(transform.position, 10.0f);
        Debug.Log("Object: " + gameObject.name + " Health is: " + NewHealth.ToString());
    }
    //-------------------------------------------------------
    //-------------------------------------------------------
    //Function called when dead
    void OnDead(Component Enemy, float NewHealth)
    {
        //If health has changed of this object
        if (this.GetInstanceID() != Enemy.GetInstanceID()) return;

        gameObject.SetActive(false);
        Debug.Log("Object: " + gameObject.name + " Health is: " + NewHealth.ToString());
    }
    //-------------------------------------------------------
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Slime")
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
