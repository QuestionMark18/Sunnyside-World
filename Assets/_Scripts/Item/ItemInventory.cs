using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemInventory
{
    public ItemSO ItemSO;
    public int ItemCount = 0;
    public int MaxStack = 99;
}
