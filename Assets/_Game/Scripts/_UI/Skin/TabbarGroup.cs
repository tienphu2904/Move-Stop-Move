using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TabbarGroup : MonoBehaviour
{
    [SerializeField] private SkinItemList skinItemList;
    public List<TabbarItem> tabbarItemList;
    private TabbarItem currentTabbarItem;

    public void Subcribe(TabbarItem tabbarItem)
    {
        if (tabbarItem == null)
        {
            tabbarItemList = new List<TabbarItem>();
        }
        tabbarItemList.Add(tabbarItem);
        OnTabSelected(tabbarItemList.First());
    }

    public void OnTabEnter(TabbarItem tabbarItem)
    {
        ResetTab();
        if (currentTabbarItem == null || tabbarItem != currentTabbarItem)
        {
            tabbarItem.background.ChangeAlpha(.5f);
        }
    }

    public void OnTabExit(TabbarItem tabbarItem)
    {
        ResetTab();
    }

    public void OnTabSelected(TabbarItem tabbarItem)
    {
        currentTabbarItem = tabbarItem;
        ResetTab();
        tabbarItem.background.ChangeAlpha(0f);
        skinItemList.RemoveAllListItem();
        skinItemList.SpawnListItem(tabbarItem.itemData, tabbarItem.shopCategory);
    }

    public void ResetTab()
    {
        foreach (TabbarItem item in tabbarItemList)
        {
            if (currentTabbarItem != null && item == currentTabbarItem)
            {
                continue;
            }
            item.background.ChangeAlpha(1f);
        }
    }
}
