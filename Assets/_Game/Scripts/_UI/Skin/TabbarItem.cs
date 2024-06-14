using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class TabbarItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public ShopCategory shopCategory;
    public SkinItemData itemData;
    public TabbarGroup tabbarGroup;
    public Image background;

    private void Start()
    {
        background = GetComponent<Image>();
        tabbarGroup.Subcribe(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tabbarGroup.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabbarGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabbarGroup.OnTabExit(this);
    }
}
