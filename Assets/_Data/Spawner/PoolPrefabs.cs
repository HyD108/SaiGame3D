using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolPrefabs<T> : HyDBehaviour where T : PoolObj
{
    [SerializeField] protected List<T> prefabs;

    protected override void Awake()
    {
        base.Awake();
        this.HidePrefabs();
    }

    protected virtual void HidePrefabs()
    {
        foreach ( T prefab in prefabs )
        {
            prefab.gameObject.SetActive(false);
        }
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPrefabs();
    }

    private void LoadPrefabs()
    {
        if (prefabs.Count > 0) return;
        foreach (Transform child in transform)
        {
            T newPrefab = child.GetComponent<T>();
            if (newPrefab != null) this.prefabs.Add(newPrefab); 
        }
        Debug.Log(transform.name + ": LoadPrefabs", gameObject);
    }

    public virtual T GetRandom()
    {
        int rand = UnityEngine.Random.Range(0, this.prefabs.Count);
        return prefabs[rand];
    }

    public virtual T GetName(string name)
    {
        foreach ( T prefab in this.prefabs)
        {
            if (prefab.name != name) continue;
                return prefab;
        }
        return null;

    }
}
