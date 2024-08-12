using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Inventory : MyMonoBehaviour
{
    [SerializeField] private int _maxSlot = 3;

    [SerializeField] private List<ItemInventory> _itemInventoryList = new List<ItemInventory>();
    public List<ItemInventory> ItemInventoryList => _itemInventoryList;

    private void Start()
    {
        AddItem(EItemCode.Beetroot, 2);
    }

    public bool IsInventoryFull()
    {
        return _itemInventoryList.Count >= _maxSlot;
    }

    public bool AddItem(EItemCode itemCode, int addCount)
    {
        ItemSO itemSO = GetItemSO(itemCode);
        if (itemSO == null) return false;

        int addRemain = addCount;
        int newCount;
        int addMore;
        ItemInventory currentSlot;

        do
        {
            currentSlot = GetSlotNotFullStack(itemCode);
            if (currentSlot == null) currentSlot = CreateNewSlot(itemSO);
            if (currentSlot == null)
            {
                Debug.Log($"Inventory is full. Item count remain: {addRemain}");
                return false;
            };

            newCount = currentSlot.ItemCount + addRemain;
            if (newCount > currentSlot.MaxStack)
            {
                addMore = currentSlot.MaxStack - currentSlot.ItemCount;
                newCount = currentSlot.ItemCount + addMore;
                addRemain -= addMore;
            }
            else
            {
                addRemain = 0;
            }
            currentSlot.ItemCount = newCount;
        }
        while (addRemain > 0);
        return true;
    }

    private ItemSO GetItemSO(EItemCode itemCode)
    {
        var itemsSO = Resources.LoadAll("SO/Items", typeof(ItemSO));
        foreach (ItemSO itemSO in itemsSO)
        {
            if (itemSO.ItemCode == itemCode) return itemSO;
        }
        Debug.Log("ItemSO not found");
        return null;
    }

    private ItemInventory GetSlotNotFullStack(EItemCode itemCode)
    {
        foreach (ItemInventory slot in _itemInventoryList)
        {
            if (slot.ItemSO.ItemCode != itemCode) continue;
            if (slot.ItemCount >= slot.MaxStack) continue;
            return slot;
        }
        return null;
    }

    private ItemInventory CreateNewSlot(ItemSO itemSO)
    {
        if (IsInventoryFull()) return null;
        ItemInventory newSlot = new ItemInventory
        {
            ItemSO = itemSO,
            MaxStack = itemSO.ItemMaxStack,
        };
        _itemInventoryList.Add(newSlot);
        return newSlot;
    }
}