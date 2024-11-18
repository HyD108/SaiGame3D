using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class DamageSender : HyDBehaviour
{
    [SerializeField] protected int damage = 1;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] public Collider _collider;

    private void OnTriggerEnter(Collider other)
    {
        this.DamageSending(other);
    }

    protected virtual DamageReceiver DamageSending(Collider other)
    {
        DamageReceiver dameReceiver = other.GetComponent<DamageReceiver>();
        if (dameReceiver == null) return null;
        dameReceiver.Receive(damage, this);
        return dameReceiver;
        //Debug.Log(transform.name + "" + other.name, other.gameObject);
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigidbody();
        this.LoadCollider();
    }

    protected virtual void LoadCollider()
    {
        if (_collider != null) return;
        this._collider = GetComponent<Collider>();
        this._collider.isTrigger = true;
        Debug.Log(transform.name + "LoadCollider", gameObject);
    }

    protected virtual void LoadRigidbody()
    {
        if(rb != null) return;
        this.rb = GetComponent<Rigidbody>();
        this.rb.useGravity = false;
        Debug.Log(transform.name + "LoadRigidbody", gameObject);
    }
}
