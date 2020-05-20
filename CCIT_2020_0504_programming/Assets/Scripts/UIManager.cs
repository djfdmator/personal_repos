using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private HP HP;

    private BulletCount bulletCount;
    private Coroutine Co_Reload;

    private void Awake()
    {
        HP = transform.Find("HP").GetComponent<HP>();
        bulletCount = transform.Find("BulletCounter").GetComponent<BulletCount>();
    }

    public void ChangeBulletCount(int _curAmmo, int _maxAmmoCount)
    {
        bulletCount.ChangeBulletCount(_curAmmo, _maxAmmoCount);
    }

    public void Reload(float _reloadTime, int _maxAmmoCount)
    {
        if(Co_Reload != null)
        {
            StopCoroutine(Co_Reload);
            Co_Reload = null;
        }

        Co_Reload = StartCoroutine(bulletCount.Reload(_reloadTime, _maxAmmoCount));
    }

    public void ChangeHP(float _hp)
    {
        HP.ChangeHPText(_hp);
    }
}
