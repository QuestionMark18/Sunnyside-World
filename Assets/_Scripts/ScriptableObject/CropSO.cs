using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CropSO", menuName = "SO/CropSO")]
public class CropSO : ScriptableObject
{
    public string CropName = "no_name";
    public float GrowthTime = 0f;
    public List<Sprite> SpriteList;
    public EItemCode DropItem;
}
