using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class BulletDamageSender : DamageSender
{
    [SerializeField] protected SphereCollider sphereCollider;
    [SerializeField] protected EffectDespawn despawn;
    protected override void LoadCollider()
    {
        if (_collider != null) return;
        this._collider = GetComponent<Collider>();
        this._collider.isTrigger = true;
        this.sphereCollider = (SphereCollider)this._collider;
        this.sphereCollider.radius = 0.3f;
        Debug.Log(transform.name + "LoadCollider", gameObject);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadDespawn();
    }

    protected virtual void LoadDespawn()
    {
        if (this.despawn != null) return;
        this.despawn = transform.GetComponentInChildren<EffectDespawn>();
        Debug.Log(transform.name + ": LoadDespawn", gameObject);
    }
    protected override DamageReceiver DamageSending(Collider other)
    {
        DamageReceiver damageReceiver = base.DamageSending(other);
        if (damageReceiver == null) return null;
        this.despawn.DoDespawn(); 
        return damageReceiver;
    }
}
