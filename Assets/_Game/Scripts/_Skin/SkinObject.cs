using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinObject : GameUnit
{
    [SerializeField] private Transform weaponPosition;
    [SerializeField] private Transform headSkinItemPosition, backSkinItemPosition, tailSkinItemPosition, leftHandSkinItemPosition;
    [SerializeField] private SkinnedMeshRenderer pantMesh, bodyMesh;
    [SerializeField] private Material defaultBodyMat;

    public WeaponObject currentWeapon;
    private List<GameUnit> skinItemList = new List<GameUnit>();

    public Material CurrentBodyMat => bodyMesh.material;

    public void OnDespawn()
    {
        RemoveWeapon();
        ClearSkin();
    }

    // Skin Control
    public void ChangeSkin(SkinItem item)
    {
        foreach (SkinFragment skinFragment in item.skinFragments)
        {
            switch (skinFragment.skinType)
            {
                case SkinType.Head:
                    GameUnit headSkinFrag = SimplePool.Spawn<HeadSkin>(skinFragment.poolType, headSkinItemPosition);
                    skinItemList.Add(headSkinFrag);
                    break;
                case SkinType.Back:
                    GameUnit backSkinFrag = SimplePool.Spawn<BackSkin>(skinFragment.poolType, backSkinItemPosition);
                    skinItemList.Add(backSkinFrag);
                    break;
                case SkinType.LeftHand:
                    GameUnit leftHandSkinFrag = SimplePool.Spawn<LeftHandSkin>(skinFragment.poolType, leftHandSkinItemPosition);
                    skinItemList.Add(leftHandSkinFrag);
                    break;
                case SkinType.Tail:
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
        for (int i = 0; i < skinItemList.Count; i++)
        {
            if (skinItemList[i] != null)
            {
                SimplePool.Despawn(skinItemList[i]);
            }
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
