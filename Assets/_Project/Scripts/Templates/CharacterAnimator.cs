using UnityEngine;

public abstract class CharacterAnimator : MonoBehaviour
{
    [SerializeField] protected Animator animator; // Reference to the Animator component

    // Animation lists
    [SerializeField] protected AnimationClip[] idleAnimations;
    [SerializeField] protected AnimationClip[] walkAnimations;
    [SerializeField] protected AnimationClip[] deathAnimations;
    [SerializeField] protected AnimationClip[] attackAnimations;

    protected int currentIdleIndex = 0;
    protected int currentWalkIndex = 0;
    protected int currentDeathIndex = 0;
    protected int currentAttackIndex = 0;

    protected virtual void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    public virtual void PlayIdleAnimation()
    {
        if (idleAnimations.Length == 0) return;

        currentIdleIndex = (currentIdleIndex + 1) % idleAnimations.Length;
        animator.Play(idleAnimations[currentIdleIndex].name);
    }

    public virtual void PlayWalkAnimation()
    {
        if (walkAnimations.Length == 0) return;

        currentWalkIndex = (currentWalkIndex + 1) % walkAnimations.Length;
        animator.Play(walkAnimations[currentWalkIndex].name);
    }

    public virtual void PlayDeathAnimation()
    {
        if (deathAnimations.Length == 0) return;

        currentDeathIndex = (currentDeathIndex + 1) % deathAnimations.Length;
        animator.Play(deathAnimations[currentDeathIndex].name);
    }

    public virtual void PlayAttackAnimation()
    {
        if (attackAnimations.Length == 0) return;

        currentAttackIndex = (currentAttackIndex + 1) % attackAnimations.Length;
        animator.Play(attackAnimations[currentAttackIndex].name);
    }
}
