using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : HyDBehaviour where T : PoolObj 
{
    [SerializeField] protected Transform holder;
    [SerializeField] protected int spawnCount = 0;
    [SerializeField] protected List<T> inPoolObj = new();


    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadHolder();
    }

    protected virtual void LoadHolder()
    {
        if (this.holder != null) return;
        this.holder = transform.Find("Holder");
        if (this.holder == null)
        {
            this.holder = new GameObject("Holder").transform;
            this.holder.parent = transform;
        }
    }

    public virtual T Spawn(T prefab)
    {
        T newObject = this.GetFromPool(prefab);
        if(newObject == null)
        {
            newObject = Instantiate(prefab);
            this.spawnCount++;
            this.UpdateName(newObject.transform, prefab.transform);
            newObject.transform.parent = this.holder;
        }
        return newObject;
    }

    public virtual T Spawn(T prefab, Vector3 position)
    {
        T newObject = this.Spawn(prefab);
        newObject.transform.position = position;
        return newObject;
    }

    public virtual T Spawn(T prefab, Vector3 position, Quaternion rot)
    {
        T newObject = this.Spawn(prefab, position);
        newObject.transform.rotation = rot;
        return newObject;
    }

    public virtual void Despawn(T obj)
    {
        if(obj is MonoBehaviour monoBehaviour)
        {
            monoBehaviour.gameObject.SetActive(false);
            this.AddObjToPool(obj);
        }
    }

    protected virtual void AddObjToPool(T obj)
    {
         this.inPoolObj.Add(obj);
    }

    protected virtual string UpdateName(Transform newObj, Transform prefab)
    {
        return newObj.name = prefab.name + "_" + this.spawnCount;
    }

    protected virtual T GetFromPool(T prefab)
    {
        foreach (T obj in this.inPoolObj)
        {
            if(prefab.GetName() == obj.GetName())
            {
                this.RemoveObjFromPool(obj);
                return obj;
            }
            
        }
        return null;
    }

    protected virtual void RemoveObjFromPool(T obj)
    {
        this.inPoolObj.Remove(obj);
    }
}
