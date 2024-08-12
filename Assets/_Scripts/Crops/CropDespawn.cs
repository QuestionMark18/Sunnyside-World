using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropDespawn : DespawnByTime
{
    public override void DestroyObject()
    {
        CropSpawner.Instance.Despawn(transform.parent);
    }
}
