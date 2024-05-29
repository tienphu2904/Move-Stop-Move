using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkinItemList : MonoBehaviour
{
    [SerializeField] private SkinItemUICard prbSkinItem;
    private SkinItemData itemData;
    public SkinItemUICard currentSkinItem;
    private List<SkinItemUICard> skinItemUIList = new List<SkinItemUICard>();

    public void SpawnListItem(SkinItemData itemData)
    {
        this.itemData = itemData;
        for (int i = itemData.itemDataList.Count - 1; i >= 0; i--)
        {
            SkinItemUICard skinItem = Instantiate(prbSkinItem, transform);
            skinItem.skinItemList = this;
            skinItem.Setup(itemData.itemDataList[i]);
            skinItemUIList.Add(skinItem);
        }
        OnItemSelected(skinItemUIList.First());
    }

    public void RemoveAllListItem()
    {
        for (int i = 0; i < skinItemUIList.Count; i++)
        {
            Destroy(skinItemUIList[i].gameObject);
        }
        skinItemUIList.Clear();
    }

    public void OnItemSelected(SkinItemUICard skinItem)
    {
        if (currentSkinItem != skinItem)
        {
            currentSkinItem = skinItem;
            ResetItemList();
            skinItem.SetActiveSelectFrame(true);
            LevelManager.Ins.player.ChangeSkin(skinItem.skinItem);
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
}
