using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAbstract : HyDBehaviour
{
    [SerializeField] protected EnemyCtrl ctrl;
    public EnemyCtrl Ctrl => ctrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyCtrl();
    }
    protected virtual void LoadEnemyCtrl()
    {
        if (this.ctrl != null) return;
        this.ctrl = GetComponentInParent<EnemyCtrl>();
        Debug.Log(transform.name + "LoadEnemyCtrl", gameObject);
    }
}
