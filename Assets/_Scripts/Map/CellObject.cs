using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellObject
{
    public int X;
    public int Y;

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }
}
