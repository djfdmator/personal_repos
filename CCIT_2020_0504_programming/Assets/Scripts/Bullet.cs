using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float LiveTime = 3.0f;
    public float time = 0.0f;

    //public GameObject BulletHitHole;

    private void Awake()
    {
        //BulletHitHole = Resources.Load("BulletHitHole") as GameObject;
    }

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
        else if(other.CompareTag("Map"))
        {
            //GameObject hitHole = Instantiate(BulletHitHole, other.ClosestPoint(transform.position), Quaternion.FromToRotation(Vector3.forward, transform.position - other.ClosestPoint(transform.position)));
        }

        Destroy(gameObject);
    }
}
