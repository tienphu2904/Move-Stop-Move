using System;
using System.Collections.Generic;
using UnityEngine;


public enum SkinType
{
    Head = 0,
    Back = 1,
    LeftHand = 2,
    Tail = 3,
    Pant = 4,
    Body = 5
}

[Serializable]
public struct SkinFragment
{
    public SkinType skinType;
    public PoolType poolType;
    public Material material;
}

[Serializable]
public struct SkinItem
{
    public int id;
    public int cost;
    public Sprite prbItemSprite;
    public SkinFragment[] skinFragments;
}

[CreateAssetMenu(menuName = "My Assets/Skin Item Data")]
public class SkinItemData : ScriptableObject
{
    public List<SkinItem> itemDataList;
}
