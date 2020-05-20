using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BulletCount : MonoBehaviour
{
    private TextMeshProUGUI bulletCount;
    private Image reload;

    private void Awake()
    {
        bulletCount = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        reload = transform.Find("Reload").GetComponent<Image>();
    }

    public void ChangeBulletCount(int _curAmmo, int _maxAmmoCount)
    {
        bulletCount.text = _curAmmo + "/" + _maxAmmoCount;
    }

    public IEnumerator Reload(float _reloadTime, int _maxAmmoCount)
    {
        float time = 0;
        while(time < _reloadTime)
        {
            time += Time.deltaTime;
            reload.fillAmount = Mathf.Lerp(0f, 1.0f, time / _reloadTime);
            yield return null;
        }
        ChangeBulletCount(30, _maxAmmoCount);
        reload.fillAmount = 0f;
    }
}
