using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerAction : MyMonoBehaviour
{
    [Header("SelectBox")]
    [SerializeField] private Transform _selectBox;

    private void Update()
    {
        SelectBoxHandle();
        Actions();
    }

    private void SelectBoxHandle()
    {
        Vector3Int cellPos = MapManager.Instance.GetWorldToCell(transform.position);
        _selectBox.GetChild(0).transform.position = new Vector2(cellPos.x, cellPos.y);
        _selectBox.GetChild(1).transform.position = new Vector2(cellPos.x + 1, cellPos.y);
        _selectBox.GetChild(2).transform.position = new Vector2(cellPos.x, cellPos.y + 1);
        _selectBox.GetChild(3).transform.position = new Vector2(cellPos.x + 1, cellPos.y + 1);
    }

    private void Actions()
    {
        ClearGrass();
        Plant();
        Harvest();
    }

    private void ClearGrass()
    {
        if (InputManager.Instance.LeftMouse)
        {
            TileBase tb_curGrass = MapManager.Instance.GetGrassTile(transform.position);
            if (tb_curGrass != null)
            {
                MapManager.Instance.SetGrassTile(transform.position, null);
            }
            else Debug.Log("Cell has been dug");
        }
    }

    private void Plant()
    {
        if (InputManager.Instance.RightMouse)
        {
            TileBase tb_curGrass = MapManager.Instance.GetGrassTile(transform.position);
            if (tb_curGrass == null)
            {
                Vector3Int cellPos = MapManager.Instance.GetWorldToCell(transform.position);
                FarmManager.Instance.PlantCrop(new Vector2Int(cellPos.x, cellPos.y), CropSpawner.Instance.Crops_1);
            }
            else Debug.Log("Can not plant on grass");

        }
    }

    private void Harvest()
    {
        if (InputManager.Instance.Key_F)
        {
            Vector3Int cellPos = MapManager.Instance.GetWorldToCell(transform.position);
            FarmManager.Instance.HarvestCrop(new Vector2Int(cellPos.x, cellPos.y));
        }
    }
}
