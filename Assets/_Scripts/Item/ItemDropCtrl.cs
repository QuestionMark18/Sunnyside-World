using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropCtrl : MyMonoBehaviour
{
    [SerializeField] private ItemSO _itemSO;
    public ItemSO ItemSO => _itemSO;

    [SerializeField] private ItemDropDespawn _itemDropDespawn;
    public ItemDropDespawn ItemDropDespawn => _itemDropDespawn;

    [SerializeField] private ItemDropPickupable _itemDropPickupable;
    public ItemDropPickupable ItemDropPickupable => _itemDropPickupable;

    [SerializeField] private ItemInventory _itemInventory;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadItemSO();
        LoadItemDropDespawn();
        LoadItemDropPickupable();
    }

    private void LoadItemSO()
    {
        if (_itemSO != null) return;
        _itemSO = Resources.Load<ItemSO>("SO/Items/" + transform.name); ;
        Debug.Log($"{transform.name}: LoadItemSO", gameObject);
    }

    private void LoadItemDropDespawn()
    {
        if (_itemDropDespawn != null) return;
        _itemDropDespawn = GetComponentInChildren<ItemDropDespawn>();
        Debug.Log($"{transform.name}: LoadItemDropDespawn", gameObject);
    }

    private void LoadItemDropPickupable()
    {
        if (_itemDropPickupable != null) return;
        _itemDropPickupable = GetComponentInChildren<ItemDropPickupable>();
        Debug.Log($"{transform.name}: LoadItemDropPickupable", gameObject);
    }

    public void SetItemInventory(ItemInventory itemInventory)
    {
        _itemInventory = itemInventory;
    }
}
