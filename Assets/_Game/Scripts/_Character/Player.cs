using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Player : CharacterObject
{
    [Header("Player")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject circleOutline;

    private WeaponShopItem EquippedWeaponData => DataManager.Ins.weaponItemList.Find(i => i.statusType == StatusType.Equipped);
    private ShopItem CurrentSkinData => DataManager.Ins.shopItemList.Find(i => i.statusType == StatusType.Equipped);

    private void Update()
    {
        if (GameManager.Ins.IsState(GameState.Gameplay) && !isDead)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 nextPoint = JoystickControl.direct * speed * Time.deltaTime + TF.position;

                TF.position = CheckGround(nextPoint);

                if (JoystickControl.direct != Vector3.zero)
                {
                    TF.forward = JoystickControl.direct;
                }
                isMoving = true;
                ChangeAnimation(Constant.ANIM_IS_RUN);
            }

            if (Input.GetMouseButtonUp(0))
            {
                isMoving = false;
                ChangeAnimation(Constant.ANIM_IS_IDLE);
            }

            if (currentTarget == null)
            {
                FindTarget();
            }
            else
            {
                currentTarget.EnableTargetMark(true);
                if (!isMoving && characterSkin.currentWeapon.CanAttack)
                {
                    OnAttack();
                }
            }
        }
    }

    //Target control
    public override void AddTarget(CharacterObject target)
    {
        base.AddTarget(target);
    }

    public override void RemoveTarget(CharacterObject target)
    {
        base.RemoveTarget(target);
        target.EnableTargetMark(false);
    }

    // ICharacter inherit
    public override void OnInit()
    {
        base.OnInit();
        characterSkin.ChangeWeapon(LevelManager.Ins.weaponItemData.itemDataList[EquippedWeaponData.id]);
        characterSkin.ChangeSkin(LevelManager.Ins.shopItemData.SkinItemDataList[(int)CurrentSkinData.shopCategory].itemDataList[CurrentSkinData.id]);
        characterName = Constant.CHARACTER_NAME;
        SetCircleOutLine();
        //Update Anim
        ChangeAnimation(Constant.ANIM_IS_IDLE);
    }

    public override void OnPlay()
    {
        nameTag.OnInit(characterName, characterSkin.CurrentBodyMat.color);
        SetCircleOutLine();
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
    }

    public override void OnAttack()
    {
        base.OnAttack();
    }

    public override void OnDeath(CharacterObject attacker)
    {
        base.OnDeath(attacker);
        nameTag.OnDespawn();
    }

    public override void OnKill()
    {
        base.OnKill();
        CameraFollow.Ins.UpdateWithPlayerSize(size);
    }

    private void SetCircleOutLine()
    {
        circleOutline.SetActive(GameManager.Ins.IsState(GameState.Gameplay) ? true : false);
    }
}
