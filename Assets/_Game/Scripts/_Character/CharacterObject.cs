using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public abstract class ICharacter : GameUnit
{
    public abstract void OnInit();
    public abstract void OnDespawn();
    public abstract void OnDeath();
    public abstract void OnAttack();
}

public class CharacterObject : ICharacter
{
    private const float CHARACTER_COLLIDER_RADIUS = .5f;

    [SerializeField] protected Transform character;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] protected Animator anim;
    [SerializeField] GameObject targetMask;
    [SerializeField] public SkinObject characterSkin;

    public bool isDead;
    protected bool isMoving;
    protected string currentAnim;
    protected List<GameUnit> skinItemList = new List<GameUnit>();
    public List<CharacterObject> targetList = new List<CharacterObject>();
    public CharacterObject currentTarget;
    protected Vector3 targetPoint;
    protected float size = 1f;

    public bool IsDead { get => isDead; set => isDead = value; }
    public bool CanAttack => targetList.Count > 0;

    // ICharacter inherit
    public override void OnInit()
    {

    }

    public override void OnDespawn()
    {

    }

    public override void OnAttack()
    {
        FindTarget();
        if (currentTarget != null && !currentTarget.IsDead)
        {
            targetPoint = currentTarget.TF.position;
            TF.forward = targetPoint - character.position;
            if (currentTarget != null && characterSkin.currentWeapon.CanAttack)
            {
                if (!currentTarget.IsDead)
                {
                    Attack();
                }
                else
                {
                    RemoveTarget(currentTarget);
                }
            }
            ResetAnim();
            ChangeAnimation(Constant.ANIM_IS_ATTACK);
        }
    }

    public override void OnDeath()
    {
        IsDead = true;
        ChangeAnimation(Constant.ANIM_IS_DEAD);
    }

    //Moving control
    public Vector3 CheckGround(Vector3 nextPoint)
    {
        RaycastHit hit;
        if (Physics.Raycast(nextPoint + Vector3.up, Vector3.down, out hit, 5f, groundLayer))
        {
            return hit.point + Vector3.up * .01f;
        }

        return TF.position;
    }

    public void Attack()
    {
        StartCoroutine(characterSkin.currentWeapon.Shoot(character: this, target: targetPoint, size: 1f));
    }

    // Skin Control
    public void ChangeSkin(SkinItem item)
    {
        characterSkin.ChangeSkin(item);
    }

    public void ClearSkin()
    {
        characterSkin.ClearSkin();
    }

    //Weapon Control
    public void ChangeWeapon(WeaponItem item)
    {
        characterSkin.ChangeWeapon(item);
    }

    public void RemoveWeapon()
    {
        characterSkin.RemoveWeapon();
    }

    //target control
    public virtual void AddTarget(CharacterObject target)
    {
        targetList.Add(target);
    }

    public virtual void RemoveTarget(CharacterObject target)
    {
        currentTarget = currentTarget == target ? null : currentTarget;
        targetList.Remove(target);
    }

    public virtual void EnableTargetMark(Boolean isEnable)
    {
        targetMask.SetActive(isEnable);
    }

    public void FindTarget()
    {
        if (currentTarget == null || currentTarget.IsDead)
        {
            float minDistance = float.PositiveInfinity;
            foreach (CharacterObject character in targetList)
            {
                float maxSightRange = Constant.CHARACTER_SIGHT_RADIUS * size + CHARACTER_COLLIDER_RADIUS * character.size;
                float dis = Vector3.Distance(TF.position, character.TF.position);
                if (character != null
                    && character != this
                    && !character.IsDead
                    && Vector3.Distance(TF.position, character.TF.position) <= maxSightRange)
                {
                    if (dis < minDistance)
                    {
                        minDistance = dis;
                        currentTarget = character;
                    }
                }
            }
        }
    }

    //Animation
    public void ChangeAnimation(string animName)
    {
        if (currentAnim != animName)
        {
            anim.ResetTrigger(currentAnim);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }

    public void ResetAnim()
    {
        anim.ResetTrigger(currentAnim);
        currentAnim = "";
    }

}
