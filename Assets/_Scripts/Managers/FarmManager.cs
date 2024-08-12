using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FarmManager : MyMonoBehaviour
{
    private static FarmManager _instance;
    public static FarmManager Instance => _instance;

    [SerializeField] private Map _map;

    [SerializeField] private int _mapWidth = 30;
    [SerializeField] private int _mapHeight = 30;
    [SerializeField] private int _offsetX = 15;
    [SerializeField] private int _offsetY = 15;
    [SerializeField] private Tilemap _tm_mapSize;

    private Crop[,] _cropMap;
    private HashSet<Vector2Int> _occupiedCell = new HashSet<Vector2Int>();

    protected override void Awake()
    {
        if (_instance != null) return;
        _instance = this;

        base.Awake();

    }

    private void Start()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        _map = MapManager.Instance.FullMap;
        _cropMap = new Crop[_mapWidth, _mapHeight];

        for (int y = 0; y < _mapHeight; y++)
        {
            for (int x = 0; x < _mapWidth; x++)
            {
                //Debug.Log($"Cell:{X - _offsetX},{_offsetY - Y - 1}");
                _cropMap[x, y] = new Crop(x - _offsetX, _offsetY - y - 1, null, null);
            }
        }
    }

    public void PlantCrop(Vector2Int cellPos, string cropName)
    {
        if (_occupiedCell.Contains(cellPos))
        {
            Debug.Log("Cell has planted Crop");
            return;
        }

        Vector2 cropPos = new Vector3(cellPos.x + 0.5f, cellPos.y + 0.5f);
        string newId = Guid.NewGuid().ToString();

        Transform newCrop = CropSpawner.Instance.Planted(cropName, cropPos, Quaternion.identity);
        newCrop.GetComponent<CropCtrl>().CropGrowth.BePlanted();
        int x = cellPos.x + _offsetX;
        int y = _offsetY - cellPos.y - 1;
        //Debug.Log($"PlantCrop:{X},{Y}");
        SetCrop(x, y, newCrop.name, newId);
        newCrop.name = newCrop.name + "_" + newId;

        _occupiedCell.Add(cellPos);
        WriteCropMapToFirebase();
        Debug.Log($"Crop planted at: {cellPos.x},{cellPos.y}");
    }

    public void HarvestCrop(Vector2Int cellPos)
    {
        if (!_occupiedCell.Contains(cellPos))
        {
            Debug.Log("No crop to harvest");
            return;
        }

        int x = cellPos.x + _offsetX;
        int y = _offsetY - cellPos.y - 1;

        Transform holder = GameObject.Find("CropSpawner").transform.Find("Holder");
        Transform harvestCrop = holder.Find(_cropMap[x, y].Name + "_" + _cropMap[x, y].Id);
        CropCtrl harvestCropCtrl = harvestCrop.GetComponent<CropCtrl>();
        if (harvestCropCtrl.CropGrowth.GrowthStage == 4)
        {
            harvestCrop.name = _cropMap[x, y].Name;
            SetCrop(x, y);
            _occupiedCell.Remove(cellPos);
            harvestCropCtrl.CropDespawn.DestroyObject();
            //ItemDropSpawner.Instance.Spawn(harvestCropCtrl.CropSO.DropItem.ToString(), new Vector2(cellPos.x + 0.5f, cellPos.y), Quaternion.identity);
            ItemDropSpawner.Instance.Drop(harvestCropCtrl.CropSO.DropItem, new Vector2(cellPos.x + 0.5f, cellPos.y), Quaternion.identity);

            WriteCropMapToFirebase();
            Debug.Log($"Crop harvested at: {cellPos.x},{cellPos.y}");
        }
        else Debug.Log("Crop is not fully grow");
    }

    private void SetCrop(int x, int y, string name = null, string id = null)
    {
        _cropMap[x, y].Name = name;
        _cropMap[x, y].Id = id;
    }

    private void WriteCropMapToFirebase()
    {
        List<Crop> cropMap = new List<Crop>();
        for (int y = 0; y < _cropMap.GetLength(1); y++)
        {
            for (int x = 0; x < _cropMap.GetLength(0); x++)
            {
                cropMap.Add(_cropMap[x, y]);
            }
        }
        _map.CropMap = cropMap;
        MapManager.Instance.WriteMapToFirebase();
    }
}
