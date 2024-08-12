using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryDropItem : MyMonoBehaviour
{
    private void Update()
    {
        if (InputManager.Instance.Key_Q) DropItem(0);
    }

    private void DropItem(int index)
    {
        Inventory inventory = transform.parent.GetComponent<Inventory>();
        Debug.Log(inventory.ItemInventoryList[0].ItemSO.ItemCode);

        Vector2 dropPos = transform.position;
        dropPos.x += 1f;
        ItemDropSpawner.Instance.Drop(inventory.ItemInventoryList[0], dropPos, Quaternion.identity);
    }
}
