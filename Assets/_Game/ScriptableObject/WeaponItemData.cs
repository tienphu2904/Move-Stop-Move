using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct WeaponItem
{
    public int id;
    public int cost;
    public string name;
    public Sprite prbItemSprite;
    public PoolType poolType;
}

[CreateAssetMenu(menuName = "My Assets/Weapon Item Data")]
public class WeaponItemData : ScriptableObject
{
    public List<WeaponItem> itemDataList;
}
