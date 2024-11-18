using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyCtrl : PoolObj
{
    [SerializeField] protected NavMeshAgent agent;
    public NavMeshAgent Agent => agent;

    [SerializeField] protected Animator animator;
    public Animator Animator => animator;

    [SerializeField] protected EnemyDamageReceiver damageReceiver;
    public EnemyDamageReceiver DamageReceiver => damageReceiver;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAgent();
        this.LoadAnimator();
        this.LoadDamageReceiver();
    }

    protected virtual void LoadDamageReceiver()
    {
        if (this.damageReceiver != null) return;
        this.damageReceiver = transform.GetComponentInChildren<EnemyDamageReceiver>();
        Debug.Log(transform.name + "LoadDamageReceiver", gameObject);
    }

    protected virtual void LoadAnimator()
    {
        if (this.agent != null) return;
        this.agent = GetComponent<NavMeshAgent>();
        Debug.Log(transform.name + "LoadAnimator", gameObject);
    }

    protected virtual void LoadAgent()
    {
        if (this.animator != null) return;
       this.animator = transform.Find("Model").GetComponent<Animator>();
        Debug.Log(transform.name + "LoadAgents", gameObject);
    }
}
