using System;
using System.Collections.Generic;
using UnityEngine;

public enum StatusType
{
    Lock,
    Available,
    Equipped
}

public enum SkinType
{
    Head,
    Back,
    LeftHand,
    Tail,
    Pant,
    Body
}

[Serializable]
public struct SkinFragment
{
    public SkinType skinType;
    public GameObject prbSkinItem;
    public Material material;
}

[Serializable]
public struct SkinItem
{
    public int id;
    public int cost;
    public StatusType status;
    public Sprite prbItemSprite;
    public SkinFragment[] skinFragments;
}

[CreateAssetMenu(menuName = "My Assets/Skin Item Data")]
public class SkinItemData : ScriptableObject
{
    public List<SkinItem> itemDataList;
}
