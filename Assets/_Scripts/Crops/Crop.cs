using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Crop : CellObject
{
    public string Name;
    public string Id;

    public Crop(int x, int y, string name, string id)
    {
        this.X = x;
        this.Y = y;
        this.Name = name;
        this.Id = id;
    }
}
