using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AnimatorController : MonoBehaviour
{
    [SerializeField] Animator anim;

    void LateUpdate()
    {
        if (Player.Instance.IsDead()) PlayVictoryAnimation();
    }

    public void PlayMovementAnimations(bool _isMoving)
    {
        anim.SetBool("Is Moving", _isMoving);
    }

    public void PlayAttackAnimation()
    {
        anim.SetTrigger("Attack" + Random.Range(0, 2));
    }

    public void PlayDeathAnimation()
    {
        anim.SetTrigger("Dead" + Random.Range(0, 1));
    }

    public void PlayVictoryAnimation()
    {
        anim.SetBool("Victory",true);
    }

    private void OnDisable()
    {
        anim.SetBool("Victory",false);
    }
}
