using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropSpawner : Spawner
{
    private static CropSpawner _instance;
    public static CropSpawner Instance => _instance;

    // variable to testing
    public string Crops_1 = "Beetroot";
    public string Crops_2 = "Sunflower";

    protected override void Awake()
    {
        if (_instance != null) return;
        _instance = this;

        base.Awake();
    }

    public Transform Planted(string prefabName, Vector2 pos, Quaternion rot)
    {
        return Spawn(prefabName, pos, rot);
    }
}
