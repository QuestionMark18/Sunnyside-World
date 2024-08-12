using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class PlayerPickup : MyMonoBehaviour
{
    [SerializeField] private CircleCollider2D _circleCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Inventory inventory = transform.parent.GetComponent<PlayerCtrl>().PlayerInventory;
        if (inventory.IsInventoryFull()) return;

        ItemDropPickupable itemPickupable = collision.transform.GetComponent<ItemDropPickupable>();
        if (itemPickupable)
        {
            itemPickupable.PickUp(transform.parent);
            Debug.Log($"Picked {collision.transform.parent.name}");
        }
    }
}
