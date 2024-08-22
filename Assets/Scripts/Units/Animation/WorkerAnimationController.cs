using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerAnimationController : MonoBehaviour
{
    private const string RUN_TRIGGER = "Run";
    private const string Work_Trigger = "Work";

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void RunAnimation()
    {
        _animator.SetTrigger(RUN_TRIGGER);
    }

    public void WorkAnimation()
    {
        _animator.SetTrigger(Work_Trigger);
    }
}