using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class SkinItemList : MonoBehaviour
{
    [SerializeField] private SkinItemUICard prbSkinItem;
    [SerializeField] private GameObject buyButton, selectButton, equippedButton;
    [SerializeField] private TextMeshProUGUI skinPrice;
    // public SkinItemData currentSkinItemData;
    public ShopCategory currentShopCategory;
    public SkinItemUICard currentSkinItem;
    public List<SkinItemUICard> skinItemUIList = new List<SkinItemUICard>();
    private ShopItem currentShopItemData => (DataManager.Ins.UserData.Dict["ShopData"] as List<ShopItem>).Find(i => i.id == currentSkinItem.skinItem.id && i.shopCategory == currentShopCategory);
    private int coinValue => ((PlayerData)DataManager.Ins.UserData.Dict["PlayerData"]).coin;

    public void SpawnListItem(SkinItemData itemData, ShopCategory shopCategory)
    {
        // currentSkinItemData = itemData;
        currentShopCategory = shopCategory;
        for (int i = 0; i < itemData.itemDataList.Count; i++)
        {
            SkinItemUICard skinItem = Instantiate(prbSkinItem, transform);
            skinItem.skinItemList = this;
            skinItem.Setup(itemData.itemDataList[i], currentShopCategory);
            skinItemUIList.Add(skinItem);
        }
        OnTrySkinItem(skinItemUIList.First());
    }

    public void RemoveAllListItem()
    {
        for (int i = 0; i < skinItemUIList.Count; i++)
        {
            Destroy(skinItemUIList[i].gameObject);
        }
        skinItemUIList.Clear();
    }

    public void OnTrySkinItem(SkinItemUICard skinItem)
    {
        if (currentSkinItem != skinItem)
        {
            currentSkinItem = skinItem;
            ResetItemList();
            skinItem.SetActiveSelectFrame(true);
            skinPrice.text = currentSkinItem.skinItem.cost.ToString();
            LevelManager.Ins.player.ChangeSkin(skinItem.skinItem);

            switch (currentShopItemData.statusType)
            {
                case StatusType.Lock:
                    SetStatusButton(buyButtonStatus: true);
                    break;
                case StatusType.Available:
                    SetStatusButton(selectButtonStatus: true);
                    break;
                case StatusType.Equipped:
                    SetStatusButton(equippedButtonStatus: true);
                    break;
            }
        }
    }

    public void ResetItemList()
    {
        foreach (SkinItemUICard item in skinItemUIList)
        {
            if (currentSkinItem != null && item == currentSkinItem)
            {
                continue;
            }
            item.SetActiveSelectFrame(false);
        }
        LevelManager.Ins.player.ClearSkin();
    }

    public SkinItemUICard EquippedSkin()
    {
        ShopItem equippedShopItemData = (DataManager.Ins.UserData.Dict["ShopData"] as List<ShopItem>).Find(i => i.statusType == StatusType.Equipped);
        return skinItemUIList.Find(i => i.skinItem.id == equippedShopItemData.id && i.shopCategory == equippedShopItemData.shopCategory);
    }

    public void BuySkinButton()
    {
        if (currentSkinItem.skinItem.cost < coinValue)
        {
            OnBuySkin();
            OnEquipSkin();
            //TODO update coin
            SetStatusButton(equippedButtonStatus: true);
        }
    }

    public void SelectSkinButton()
    {
        OnEquipSkin();
        SetStatusButton(equippedButtonStatus: true);
    }

    public void UnSelectSkinButton()
    {
        // OnUnequipSkin();
        // SetStatusButton(selectButtonStatus: true);
    }

    private void OnBuySkin()
    {
        UserData userData = DataManager.Ins.UserData;

        PlayerData playerData = userData.GetData("PlayerData", new PlayerData());
        playerData.coin -= currentSkinItem.skinItem.cost;
        playerData.currentSkinItem = currentShopItemData;

        userData.SetData("PlayerData", playerData);

        DataManager.Ins.UserData = userData;
    }

    private void OnEquipSkin()
    {
        UserData userData = DataManager.Ins.UserData;

        List<ShopItem> shopItem = userData.Dict["ShopData"] as List<ShopItem>;
        for (int i = 0; i < shopItem.Count; i++)
        {
            if (shopItem[i].statusType == StatusType.Equipped)
            {
                shopItem[i].statusType = StatusType.Available;
            }

            if (shopItem[i].shopCategory == currentShopCategory && shopItem[i].id == currentSkinItem.skinItem.id)
            {
                shopItem[i].statusType = StatusType.Equipped;
            }
        }
        userData.SetData("ShopData", shopItem);
        DataManager.Ins.UserData = userData;
        UpdateSkinItemUIList();
    }

    private void SetStatusButton(bool buyButtonStatus = false,
                                    bool selectButtonStatus = false,
                                    bool equippedButtonStatus = false)
    {
        buyButton.SetActive(buyButtonStatus);
        selectButton.SetActive(selectButtonStatus);
        equippedButton.SetActive(equippedButtonStatus);
    }

    private void UpdateSkinItemUIList()
    {
        foreach (SkinItemUICard item in skinItemUIList)
        {
            item.UpdateUI();
        }
    }

    // private void OnUnequipSkin()
    // {
    //     UserData userData = DataManager.Ins.UserData;

    //     List<ShopItem> shopItem = userData.Dict["ShopData"] as List<ShopItem>;
    //     for (int i = 0; i < shopItem.Count; i++)
    //     {
    //         if (shopItem[i].shopCategory == currentShopCategory)
    //         {
    //             if (shopItem[i].statusType == StatusType.Equipped)
    //             {
    //                 shopItem[i].statusType = StatusType.Available;
    //             }
    //         }
    //     }
    //     userData.SetData("ShopData", shopItem);
    //     DataManager.Ins.UserData = userData;
    //     currentSkinItem.UpdateUI();
    // }


}
