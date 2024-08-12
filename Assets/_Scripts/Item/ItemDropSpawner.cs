using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropSpawner : Spawner
{
    private static ItemDropSpawner _instance;
    public static ItemDropSpawner Instance => _instance;

    public string Item_1 = "Beetroot";
    public string Item_2 = "Sunflower";

    protected override void Awake()
    {
        if (_instance != null) return;
        _instance = this;

        base.Awake();
    }

    public Transform Drop(EItemCode itemCode, Vector2 pos, Quaternion rot)
    {
        return Spawn(itemCode.ToString(), pos, rot);
    }

    public Transform Drop(ItemInventory itemInventory, Vector2 pos, Quaternion rot)
    {
        EItemCode itemCode = itemInventory.ItemSO.ItemCode;
        Transform itemDrop = Spawn(itemInventory.ItemSO.ItemCode.ToString(), pos, rot);
        itemDrop.GetComponent<ItemDropCtrl>().SetItemInventory(itemInventory);
        return itemDrop;
    }
}
