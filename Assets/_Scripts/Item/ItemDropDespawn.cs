using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropDespawn : DespawnByTime
{
    public override void DestroyObject()
    {
        ItemDropSpawner.Instance.Despawn(transform.parent);
    }
}
