using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyDBehaviour : MonoBehaviour
{
    protected virtual void Reset()
    {
        this.LoadComponents();
    }
    protected virtual void Awake()
    {
        this.LoadComponents();
    }
    protected virtual void Start()
    {
        
    }

    protected virtual void LoadComponents()
    {
        // For Override
    }
}
