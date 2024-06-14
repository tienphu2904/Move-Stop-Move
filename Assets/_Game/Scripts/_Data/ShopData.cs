using System;
using System.Collections.Generic;

public enum ShopCategory
{
    Head = 0,
    Pant = 1,
    LeftHand = 2,
    Body = 3
}

public enum StatusType
{
    Lock = 0,
    Available = 1,
    Equipped = 2
}

[Serializable]
public class ShopItem
{
    public ShopCategory shopCategory;
    public int id;
    public StatusType statusType;
}