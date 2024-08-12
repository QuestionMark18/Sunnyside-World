using System.Collections;
using System.Collections.Generic;
using UnityEditor.Sprites;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ItemDropPickupable : MyMonoBehaviour
{
    [SerializeField] Transform _picker;
    [SerializeField] float _pickUpSpeed = 5f;
    [SerializeField] bool _isPickedUp;

    private void Update()
    {
        PickingUp();
    }

    public void PickUp(Transform picker)
    {
        _picker = picker;
        _isPickedUp = true;
    }

    private void PickingUp()
    {
        if (!_isPickedUp) return;

        transform.parent.position = Vector3.MoveTowards(transform.parent.position, _picker.position, _pickUpSpeed * Time.deltaTime);
        float distance = Vector3.Distance(transform.parent.position, _picker.position);
        if (distance < 0.1f) PickedUp();
    }

    private void PickedUp()
    {
        _picker.GetComponent<PlayerCtrl>().PlayerInventory.AddItem(transform.parent.GetComponent<ItemDropCtrl>().ItemSO.ItemCode, 1);
        transform.parent.GetComponent<ItemDropCtrl>().ItemDropDespawn.DestroyObject();
        _picker = null;
        _isPickedUp = false;
    }
}
