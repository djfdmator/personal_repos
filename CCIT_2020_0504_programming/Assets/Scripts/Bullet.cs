using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float LiveTime = 3.0f;
    public float time = 0.0f;

    private void Update()
    {
        time += Time.deltaTime;
        if(time >= LiveTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            other.GetComponent<AI_Enemy>().ChangeHealth(-GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AttackDamage);
        }

        Destroy(gameObject);
    }
}
