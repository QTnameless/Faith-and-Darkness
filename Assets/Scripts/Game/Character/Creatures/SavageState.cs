using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SavageState : MonoBehaviour
{
    public bool isComplete { get; protected set; }

    protected float startTime;
    public float time => Time.time - startTime;

    protected Rigidbody2D body;
    protected Animator animator;

    public virtual void Enter() { }
    public virtual void Do() { }
    public virtual void FixedDo() { }
    public virtual void Exit() { }

    public void Setup(Rigidbody2D _body, Animator _animator)
    {
        body = _body;
        animator = _animator;
    }

    public void Initialise()
    {
        isComplete = false;
        startTime = Time.time;
    }

}
