using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageEffect : MonoBehaviour
{
    private Text[] text = new Text[8];

    private Text cur;

    private float speed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponentsInChildren<Text>();    

        foreach(Text obj in text)
        {
            obj.gameObject.SetActive(false);
        }
    }

    public void OnDamageEffect(Vector3 _position, float _damage)
    {

        foreach(Text obj in text)
        {
            if(!(obj.IsActive()))
            {
                cur = obj;
                break;
            }
        }

        cur.transform.localPosition = Camera.main.WorldToScreenPoint(_position);
        
        cur.text = "-" + _damage.ToString();
        cur.gameObject.SetActive(true);

        StartCoroutine(OnEffect(cur));
    }

    IEnumerator OnEffect(Text _text)
    {
        Text temp = _text;
        float EffectTime = 1.0f;
        float time = 0.0f;

        float x = temp.transform.localPosition.x - 384.0f;
        float y = temp.transform.localPosition.y - 512.0f;
        float z = temp.transform.localPosition.z;

        while (time < EffectTime)
        {
            y += speed * Time.deltaTime;
           
            temp.transform.localPosition = new Vector3(x - 80.0f, y - 80.0f, z);

            time += Time.deltaTime;
            yield return null;
        }

        temp.gameObject.SetActive(false);
        StopCoroutine(OnEffect(null));
    }
}
