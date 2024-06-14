using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Assets/Shop Item Data")]
public class ShopItemData : ScriptableObject
{
    public List<SkinItemData> SkinItemDataList;
}
