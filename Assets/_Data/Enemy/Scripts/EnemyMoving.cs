using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoving : EnemyAbstract
{
    [SerializeField] protected PathMoving path;
    [SerializeField] protected int currentPointIndex = 0;
    [SerializeField] protected Point currentPoint;
    [SerializeField] protected float pointDistance = Mathf.Infinity;
    [SerializeField] protected float pointDistanceLimit = 1;
    [SerializeField] protected bool isFinish = false;
    [SerializeField] protected bool isMoving = false;

    [SerializeField] protected bool canMove = true;

    void FixedUpdate()
    {
        this.Moving();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPathMoving();
    }

    protected virtual void LoadPathMoving()
    {
        if (this.path != null) return;
        this.path =GameObject.Find("PathMoving_0").GetComponent<PathMoving>();
        Debug.Log(transform.name + "LoadPathMoving", gameObject);
    }

    protected virtual void Moving()
    {
        this.LoadMovingStatus();
        if (this.isFinish || !this.canMove || this.IsDead())
        {
            this.ctrl.Agent.isStopped = true;
            return;
        }

        this.GetTheNextPoint();
        this.ctrl.Agent.SetDestination(this.currentPoint.transform.position);
    }

    protected virtual bool IsDead()
    {
        return this.ctrl.DamageReceiver.IsDead();
    }

    protected virtual void LoadMovingStatus()
    {
        this.isMoving = !this.ctrl.Agent.isStopped;
        this.ctrl.Animator.SetBool("isMoving", isMoving);
    }


    protected virtual void GetTheNextPoint()
    {
        this.currentPoint = this.path.GetPoint(this.currentPointIndex);
        this.pointDistance = Vector3.Distance(this.currentPoint.transform.position,transform.position);
        if (this.pointDistance < this.pointDistanceLimit) this.currentPointIndex++;
        if (this.currentPointIndex > path.Points.Count - 1) this.isFinish = true;                         
    }
}
