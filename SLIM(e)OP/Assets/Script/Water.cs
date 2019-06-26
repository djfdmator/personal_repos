using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public float stayTime = 0.0f;
    public float slimeBreath = 0.3f;
    public Vector3 preTrans;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Slime")
        {
            stayTime += Time.deltaTime;
            if (slimeBreath <= stayTime)
            {
                collision.gameObject.GetComponent<PlayerController>().HP -= 50.0f;
                collision.gameObject.transform.position = preTrans - (collision.gameObject.transform.position - preTrans).normalized;
                stayTime = 0.0f;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Slime")
        {
            preTrans = collision.transform.position;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Slime")
        {
            stayTime = 0.0f;
        }
    }

}
