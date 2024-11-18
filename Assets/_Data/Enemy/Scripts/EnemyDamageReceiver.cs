using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageReceiver : DamageReceiver
{

    [SerializeField] protected EnemyCtrl ctrl;
    [SerializeField] protected CapsuleCollider _collider;

    protected virtual void OnEnable()
    {
        this.Reborn();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCtrl();
        this.LoadCapsuleCollider();
    }

    protected virtual void LoadCtrl()
    {
        if (this.ctrl != null) return;
        this.ctrl = transform.GetComponentInParent<EnemyCtrl>();
        
    }
    protected virtual void LoadCapsuleCollider()
    {
        if (this._collider != null) return;
        this._collider = GetComponent<CapsuleCollider>();
        this._collider.center = new Vector3(0, 1, 0);
        this._collider.radius = 0.3f;
        this._collider.height = 1.5f;
        this._collider.isTrigger = true;
        Debug.Log(transform.name + "LoadCapsuleCollider", gameObject);


    }

    public override void OnDead()
    {
        this.ctrl.Animator.SetBool("isDead",this.isDead);
        this._collider.enabled = false;

        Invoke(nameof(DoDespawn), 4f);

    }

    protected virtual void DoDespawn()
    {
        this.ctrl.Despawn.DoDespawn();
    }

    public override void OnHurt()
    {
        this.ctrl.Animator.SetTrigger("onHurt");
    }

    protected virtual void Reborn()
    {
        this.currentHealth = this.maxHealth;
        this._collider.enabled = true;
    }
}
