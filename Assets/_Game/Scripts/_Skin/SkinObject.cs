using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinObject : GameUnit
{
    [SerializeField] private Transform weaponPosition;
    [SerializeField] private Transform headSkinItemPosition, backSkinItemPosition, tailSkinItemPosition, leftHandSkinItemPosition;
    [SerializeField] private SkinnedMeshRenderer pantMesh, bodyMesh;
    [SerializeField] private Material defaultBodyMat;

    internal WeaponObject currentWeapon;
    private WeaponShopItem currentWeaponData => (DataManager.Ins.UserData.Dict["WeaponShopData"] as List<WeaponShopItem>).Find(i => i.statusType == StatusType.Equipped);

    internal SkinItem currentSkin;
    private ShopItem currentSkinData => (DataManager.Ins.UserData.Dict["ShopData"] as List<ShopItem>).Find(i => i.statusType == StatusType.Equipped);

    private List<GameUnit> skinItemList = new List<GameUnit>();

    // Skin Control
    public void ChangeSkin(SkinItem item)
    {
        currentSkin = item;
        foreach (SkinFragment skinFragment in item.skinFragments)
        {
            switch (skinFragment.skinType)
            {
                case SkinType.Head:
                    // GameObject headSkinFrag = Instantiate(skinFragment.prbSkinItem, headSkinItemPosition);
                    GameUnit headSkinFrag = SimplePool.Spawn<HeadSkin>(skinFragment.poolType, headSkinItemPosition);
                    skinItemList.Add(headSkinFrag);
                    break;
                case SkinType.Back:
                    // GameObject backSkinFrag = Instantiate(skinFragment.prbSkinItem, backSkinItemPosition);
                    GameUnit backSkinFrag = SimplePool.Spawn<BackSkin>(skinFragment.poolType, backSkinItemPosition);
                    skinItemList.Add(backSkinFrag);
                    break;
                case SkinType.LeftHand:
                    // GameObject leftHandSkinFrag = Instantiate(skinFragment.prbSkinItem, leftHandSkinItemPosition);
                    GameUnit leftHandSkinFrag = SimplePool.Spawn<LeftHandSkin>(skinFragment.poolType, leftHandSkinItemPosition);
                    skinItemList.Add(leftHandSkinFrag);
                    break;
                case SkinType.Tail:
                    // GameObject tailSkinFrag = Instantiate(skinFragment.prbSkinItem, tailSkinItemPosition);
                    GameUnit tailSkinFrag = SimplePool.Spawn<TailSkin>(skinFragment.poolType, tailSkinItemPosition);
                    skinItemList.Add(tailSkinFrag);
                    break;
                case SkinType.Pant:
                    pantMesh.material = skinFragment.material;
                    break;
                case SkinType.Body:
                    bodyMesh.material = skinFragment.material;
                    break;
                default:
                    break;
            }
        }
    }

    public void ClearSkin()
    {
        foreach (GameUnit item in skinItemList)
        {
            // Destroy(item.gameObject);
            SimplePool.Despawn(item);
        }
        skinItemList.Clear();
        pantMesh.materials = new Material[0];
        bodyMesh.material = defaultBodyMat;
    }

    //Weapon Control
    public void ChangeWeapon(WeaponItem item)
    {
        RemoveWeapon();
        currentWeapon = SimplePool.Spawn<WeaponObject>(item.poolType, weaponPosition);
    }

    public void RemoveWeapon()
    {
        if (currentWeapon != null)
        {
            SimplePool.Despawn(currentWeapon);
        }
    }
}
