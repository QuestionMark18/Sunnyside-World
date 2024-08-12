using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Despawn : MyMonoBehaviour
{
    private void Update()
    {
        Despawning();
    }

    private void Despawning()
    {
        if (!CanDespawn()) return;
        DestroyObject();
    }

    public virtual void DestroyObject()
    {
        Destroy(transform.parent.gameObject);
    }

    protected abstract bool CanDespawn();
}
