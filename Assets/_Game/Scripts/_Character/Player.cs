using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Transform weaponPosition;
    [SerializeField] private Transform headSkinItemPosition, backSkinItemPosition, tailSkinItemPosition, leftHandSkinItemPosition;
    [SerializeField] private SkinnedMeshRenderer pantMesh, bodyMesh;
    [SerializeField] private Material defaultBodyMat;
    private string currentAnim;
    private List<GameObject> skinItemList = new List<GameObject>();

    private void Start()
    {
        ChangeAnim(Constant.ANIM_IDLE);
    }

    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            anim.ResetTrigger(currentAnim);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }

    public void ChangeSkin(SkinItem item)
    {
        Transform parent = transform;
        foreach (SkinFragment skinFragment in item.skinFragments)
        {
            switch (skinFragment.skinType)
            {
                case SkinType.Head:
                    GameObject headSkinFrag = Instantiate(skinFragment.prbSkinItem, headSkinItemPosition);
                    skinItemList.Add(headSkinFrag);
                    break;
                case SkinType.Back:
                    GameObject backSkinFrag = Instantiate(skinFragment.prbSkinItem, backSkinItemPosition);
                    skinItemList.Add(backSkinFrag);
                    break;
                case SkinType.LeftHand:
                    GameObject leftHandSkinFrag = Instantiate(skinFragment.prbSkinItem, leftHandSkinItemPosition);
                    skinItemList.Add(leftHandSkinFrag);
                    break;
                case SkinType.Tail:
                    GameObject tailSkinFrag = Instantiate(skinFragment.prbSkinItem, tailSkinItemPosition);
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
        foreach (GameObject item in skinItemList)
        {
            Destroy(item.gameObject);
        }
        skinItemList.Clear();
        pantMesh.materials = new Material[0];
        bodyMesh.material = defaultBodyMat;
    }
}
