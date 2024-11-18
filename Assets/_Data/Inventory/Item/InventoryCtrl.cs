using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryCtrl : HyDBehaviour
{
    [SerializeField] protected List<ItemInventory> item = new();
    public List<ItemInventory> Item => item;
}
