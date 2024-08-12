using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using UnityEngine;

public class Map
{
    private List<Cell> _cellMap;
    public List<Cell> CellMap
    {
        get { return _cellMap; }
        set { _cellMap = value; }
    }

    private List<Crop> _cropMap;
    public List<Crop> CropMap
    {
        get { return _cropMap; }
        set { _cropMap = value; }
    }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }
}