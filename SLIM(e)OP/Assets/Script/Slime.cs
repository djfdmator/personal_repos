using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Monster
{
    #region variable
    static int AnimatorWalk = Animator.StringToHash("Walk");
    static int AnimatorAttack = Animator.StringToHash("Attack");
    Animator _animator;
    #endregion

    void Awake()
    { 
        _animator = GetComponentInChildren<Animator>();
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
