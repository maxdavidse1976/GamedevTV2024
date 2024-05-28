using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animator : MonoBehaviour
{
    public static Player_Animator Instance;
    [SerializeField] Animator anim;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayMovementAnimations(Vector2 _input)
    {
        anim.SetFloat("X Input", _input.x);
        anim.SetFloat("Y Input", _input.y);
    }

    public void PlayShootAnimation()
    {
        anim.SetTrigger("Shoot");
    }

    public void PlayDeathAnimation()
    {
        anim.SetBool("Dead", Player.Instance.IsDead());
    }
}
