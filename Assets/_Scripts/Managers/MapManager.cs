using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class MapManager : MyMonoBehaviour
{
    private static MapManager _instance;
    public static MapManager Instance => _instance;

    [Header("TileMap")]
    [SerializeField] private Tilemap _tm_mapSize;
    [SerializeField] private Tilemap _tm_water;
    [SerializeField] private Tilemap _tm_ground;
    [SerializeField] private Tilemap _tm_grass;
    [SerializeField] private Tilemap _tm_onGround;

    [Header("Map")]
    [SerializeField] private Map _fullMap = new Map();
    public Map FullMap => _fullMap;

    protected override void Awake()
    {
        if (_instance != null) return;
        _instance = this;

        base.Awake();
    }

    public Vector3Int GetWorldToCell(Vector3 worldPos)
    {
        return _tm_mapSize.WorldToCell(worldPos);
    }
    public Vector3 GetCellToWorld(Vector3Int cellPos)
    {
        return _tm_mapSize.CellToWorld(cellPos);
    }

    public TileBase GetGrassTile(Vector3Int cellPos)
    {
        return _tm_grass.GetTile(cellPos);
    }
    public TileBase GetGrassTile(Vector3 worldPos)
    {
        Vector3Int cellPos = GetWorldToCell(worldPos);
        return _tm_grass.GetTile(cellPos);
    }

    public void SetGrassTile(Vector3Int cellPos, TileBase tileBase)
    {
        _tm_grass.SetTile(cellPos, tileBase);
    }
    public void SetGrassTile(Vector3 worldPos, TileBase tileBase)
    {
        Vector3Int cellPos = GetWorldToCell(worldPos);
        _tm_grass.SetTile(cellPos, tileBase);
    }

    public void WriteMapToFirebase()
    {
        //Debug.Log(_fullMap.ToJson());
        FirebaseManager.Instance.WriteDataToFirebase(_fullMap.ToJson(), "Map");
    }
}
