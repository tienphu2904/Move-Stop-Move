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

    public void Setup(SkinItem item)
    {
        skinItem = item;
        itemImage.sprite = skinItem.prbItemSprite;
        switch (skinItem.status)
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

    public void OnPointerClick(PointerEventData eventData)
    {
        skinItemList.OnItemSelected(this);
    }

    public void SetActiveSelectFrame(Boolean isSelect)
    {
        selectFrame.SetActive(isSelect);
    }
}
