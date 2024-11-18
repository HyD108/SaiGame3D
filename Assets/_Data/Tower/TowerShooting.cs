using System.Collections.Generic;
using UnityEngine;

public class TowerShooting : TowerAbstract
{

    [SerializeField] protected EnemyCtrl target;
    [SerializeField] protected string bulletName = "Bullet";

    [SerializeField] int fireIndex = 0;
    [SerializeField] float timer = 0f;
    [SerializeField] float delay = 1f;
    [SerializeField] protected List<FirePoint> firePoints = new();

    protected virtual void FixedUpdate()
    {
        this.GetTarget();
        this.LookAtTarget();
        this.Shooting();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadFirePoints();
    }

    protected virtual void LoadFirePoints()
    {
        if(this.firePoints.Count > 0) return;
        FirePoint[] points = this.ctrl.GetComponentsInChildren<FirePoint>();
        this.firePoints = new List<FirePoint>(points);

    }

    protected virtual void Shooting()
    {
        this.timer += Time.fixedDeltaTime;
        if (this.target == null) return;
        if (this.timer < this.delay) return;
        this.timer = 0f;
        FirePoint firePoint = this.GetFirePoints();
        EffectCtrl bulletPrefab = EffectSpawnerCtrl.Instance.Prefabs.GetName(this.bulletName);
        EffectCtrl newEffect = EffectSpawnerCtrl.Instance.Spawner.Spawn(bulletPrefab, firePoint.transform.position,firePoint.transform.rotation);
        newEffect.gameObject.SetActive(true);
    }

    protected virtual FirePoint GetFirePoints()
    {
        this.fireIndex++;
        if( fireIndex >= this.firePoints.Count ) this.fireIndex = 0;
        return firePoints[this.fireIndex];
    }

    protected virtual void GetTarget()
    {
       
        this.target = this.ctrl.Radar.GetTarget();
    }

    protected virtual void LookAtTarget()
    {
        if (this.target == null) return;
        this.ctrl.Rotator.LookAt(this.target.transform.position);
    }

}
