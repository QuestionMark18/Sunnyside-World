using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.Rendering.DebugUI.Table;

public abstract class Spawner : MyMonoBehaviour
{
    [SerializeField] protected Transform holder;
    [SerializeField] protected List<Transform> prefabList;
    [SerializeField] protected List<Transform> poolObjects;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPrefab();
        LoadHolder();
    }

    private void LoadPrefab()
    {
        if (prefabList.Count > 0) return;
        Transform prefabObj = transform.Find("Prefabs");
        foreach (Transform prefab in prefabObj)
        {
            prefabList.Add(prefab);
        }
        HidePrefabs();
        Debug.Log(transform.name + ": LoadPrefab", gameObject);
    }

    private void LoadHolder()
    {
        if (holder != null) return;
        holder = transform.Find("Holder");
        Debug.Log(transform.name + ": LoadHolder", gameObject);
    }

    private void HidePrefabs()
    {
        foreach (Transform prefab in prefabList)
        {
            prefab.gameObject.SetActive(false);
        }
    }

    public Transform Spawn(string prefabName, Vector2 pos, Quaternion rot)
    {
        Transform prefab = GetPrefabByName(prefabName);
        if (prefab == null)
        {
            Debug.Log("Prefab not found: " + prefabName);
            return null;
        }

        Transform newPrefab = GetPrefabFormPool(prefab);
        newPrefab.SetPositionAndRotation(pos, rot);
        newPrefab.parent = holder;
        newPrefab.gameObject.SetActive(true);
        return newPrefab;
    }

    private Transform GetPrefabByName(string prefabName)
    {
        foreach (Transform prefab in prefabList)
        {
            if (prefab.name == prefabName) return prefab;
        }
        return null;
    }

    private Transform GetPrefabFormPool(Transform prefab)
    {
        foreach (Transform obj in poolObjects)
        {
            if (obj.name == prefab.name)
            {
                poolObjects.Remove(obj);
                return obj;
            }
        }

        Transform newPrefab = Instantiate(prefab);
        newPrefab.name = prefab.name;
        return newPrefab;
    }

    public void Despawn(Transform obj)
    {
        poolObjects.Add(obj);
        obj.gameObject.SetActive(false);
    }
}