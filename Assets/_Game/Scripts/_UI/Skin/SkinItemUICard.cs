using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkinItemUICard : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private GameObject selectFrame, lockImage, equippedImage;
    public SkinItem skinItem;
    public SkinItemList skinItemList;
    public ShopCategory shopCategory;

    public void Setup(SkinItem item, ShopCategory shopCategory)
    {
        this.shopCategory = shopCategory;
        skinItem = item;
        itemImage.sprite = skinItem.prbItemSprite;
        UpdateUI();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        skinItemList.OnTrySkinItem(this);
    }

    public void SetActiveSelectFrame(Boolean isSelect)
    {
        selectFrame.SetActive(isSelect);
    }

    public void UpdateUI()
    {
        ShopItem shopData = DataManager.Ins.shopItemList.Find(i => i.id == skinItem.id && i.shopCategory == shopCategory);
        switch (shopData.statusType)
        {
            case StatusType.Lock:
                lockImage.SetActive(true);
                equippedImage.SetActive(false);
                break;
            case StatusType.Available:
                lockImage.SetActive(false);
                equippedImage.SetActive(false);
                break;
            case StatusType.Equipped:
                lockImage.SetActive(false);
                equippedImage.SetActive(true);
                break;
        }
    }
}
