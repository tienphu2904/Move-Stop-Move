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
    private WeaponShopItem equippedWeaponData => (DataManager.Ins.UserData.Dict["WeaponShopData"] as List<WeaponShopItem>).Find(i => i.statusType == StatusType.Equipped);
    private ShopItem currentSkinData => (DataManager.Ins.UserData.Dict["ShopData"] as List<ShopItem>).Find(i => i.statusType == StatusType.Equipped);

    private void Start()
    {
        ChangeAnimation(Constant.ANIM_IS_IDLE);
    }

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
                    character.forward = JoystickControl.direct;
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
        // if (!IsDead && !target.IsDead && currentTarget == null)
        // {
        // target.EnableTargetMark(true);
        // if (!isMoving && characterSkin.currentWeapon.CanAttack)
        // {
        //     OnAttack();
        // }
        // }
    }

    public override void RemoveTarget(CharacterObject target)
    {
        base.RemoveTarget(target);
        target.EnableTargetMark(false);
    }

    public void Setup()
    {
        TF.forward = Vector3.back;
        circleOutline.SetActive(GameManager.Ins.IsState(GameState.Gameplay) ? true : false);
        characterSkin.ChangeWeapon(LevelManager.Ins.weaponItemData.itemDataList[equippedWeaponData.id]);
        characterSkin.ChangeSkin(LevelManager.Ins.shopItemData.SkinItemDataList[(int)currentSkinData.shopCategory].itemDataList[currentSkinData.id]);
    }

    // ICharacter inherit
    public override void OnInit()
    {
        base.OnInit();
        circleOutline.SetActive(GameManager.Ins.IsState(GameState.Gameplay) ? true : false);
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
    }

    public override void OnAttack()
    {
        base.OnAttack();
        // if (currentTarget != null && characterSkin.currentWeapon.CanAttack)
        // {
        //     if (!currentTarget.IsDead)
        //     {
        //         ResetAnim();
        //         Attack();
        //     }
        //     else
        //     {
        //         RemoveTarget(currentTarget);
        //     }
        // }
    }

    public override void OnDeath()
    {
        base.OnDeath();
    }

}
