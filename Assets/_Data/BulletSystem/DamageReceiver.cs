using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageReceiver : HyDBehaviour
{
    [SerializeField] protected int currentHealth = 10;
    [SerializeField] protected int maxHealth= 10;
    [SerializeField] protected bool isDead = false;
    [SerializeField] protected bool isImmotal = false;

    public virtual void Receive(int damage, DamageSender damageSender)
    {
        if (!this.isImmotal) this.currentHealth -= damage;
        if (this.currentHealth < 0) this.currentHealth = 0;

        if (this.IsDead()) this.OnDead();
        else this.OnHurt();
    }

    public abstract void OnDead();
   

    public virtual bool IsDead()
    {
        return this.isDead = this.currentHealth <= 0;
    }

    public abstract void OnHurt();
    
}
