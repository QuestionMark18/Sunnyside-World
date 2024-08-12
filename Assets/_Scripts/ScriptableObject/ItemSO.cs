using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "SO/ItemSO")]
public class ItemSO : ScriptableObject
{
    public string Name = "no_name";
    public string Description = "no_description";
    public EItemCode ItemCode = EItemCode.None;
    public EItemType ItemType = EItemType.None;
    public int ItemMaxStack = 99;
}