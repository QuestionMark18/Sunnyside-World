using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public int X { get; set; }
    public int Y { get; set; }
    public ECellState CellState { get; set; }

    public Cell()
    {
    }

    public Cell(int x, int y, ECellState cellState)
    {
        this.X = x;
        this.Y = y;
        this.CellState = cellState;
    }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }
}