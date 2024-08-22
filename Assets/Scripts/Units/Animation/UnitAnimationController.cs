using UnityEngine;

public class UnitAnimationController : MonoBehaviour
{
    private const string ATTACK_TRIGGER = "Attack";
    private const string DIE_TRIGGER = "Die";
    private const string RUN_TRIGGER = "Run";
    private const string IDLE_TRIGGER = "Idle";
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void IdleTrigger()
    {
        _animator.SetTrigger(IDLE_TRIGGER);
    }

    public void RunTrigger()
    {
        _animator.SetTrigger(RUN_TRIGGER);
    }

    public void DieTrigger()
    {
        _animator.SetTrigger(DIE_TRIGGER);
    }

    public void AttackTrigger()
    {
        _animator.SetTrigger(ATTACK_TRIGGER);
    }
}