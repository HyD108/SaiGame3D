using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]

public class TowerRadar : HyDBehaviour
{
    [SerializeField] protected SphereCollider _collider;
    [SerializeField] protected Rigidbody _rigidbody;

    [SerializeField] protected EnemyCtrl nearest;
    [SerializeField] protected List<EnemyCtrl> enemies = new();

    protected virtual void FixedUpdate()
    {
        this.FindNearestEnemy();
    }

    protected virtual void FindNearestEnemy()
    {
        float nearestDistance = Mathf.Infinity;
        float enemyDistance;
        foreach (EnemyCtrl enemy in this.enemies)
        {
            enemyDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (enemyDistance < nearestDistance)
            {
                nearestDistance = enemyDistance;
                this.nearest = enemy;
            }
        }
    }

    protected virtual void OnTriggerEnter(Collider collider)
    {

        EnemyCtrl enemyCtrl = collider.GetComponentInParent<EnemyCtrl>();
        if (enemyCtrl == null) return;

        this.AddEnemies(enemyCtrl);

        //Debug.Log(transform.name + " " + collider.name, collider.gameObject);
    }
    protected virtual void OnTriggerExit(Collider collider)
    {
        EnemyCtrl enemyCtrl = collider.GetComponentInParent<EnemyCtrl>();
        if (enemyCtrl == null) return;

        this.RemoveEnemy(enemyCtrl);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCollider();
        this.LoadRigidbody();
    }

    protected virtual void LoadCollider()
    {
        if(this._collider != null) return;
        this._collider = GetComponent<SphereCollider>();
        this._collider.isTrigger = true;
        this._collider.radius = 12;
    }
    protected virtual void LoadRigidbody()
    {
        if (this._rigidbody != null) return;
        this._rigidbody = GetComponent<Rigidbody>();
        this._rigidbody.useGravity = false;
    }

    private void RemoveEnemy(EnemyCtrl enemyCtrl)
    {
        if(this.nearest ==  enemyCtrl) this.nearest = null;
        this.enemies.Remove(enemyCtrl);
    }

    protected virtual void AddEnemies(EnemyCtrl enemyCtrl)
    {
        this.enemies.Add(enemyCtrl);
    }

    public EnemyCtrl GetTarget()
    {
        return this.nearest;
    }
}
