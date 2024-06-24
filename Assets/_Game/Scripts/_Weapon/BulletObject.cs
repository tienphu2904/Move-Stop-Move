using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class BulletObject : GameUnit
{
    [SerializeField] protected float moveSpeed = 12f;
    protected CharacterObject character;
    protected float liveRange;
    protected Vector3 startPoint;
    private float bulletSize;

    protected virtual void OnEnable()
    {
        bulletSize = 1f;
        liveRange = Constant.CHARACTER_SIGHT_RADIUS;
    }

    protected virtual void Update()
    {
        if (Vector3.Distance(startPoint, TF.position) > liveRange)
        {
            OnDespawn();
        }
    }

    public virtual void OnInit(CharacterObject character, Vector3 target, Vector3 startPoint, float size)
    {
        this.character = character;
        this.startPoint = startPoint;
        TF.forward = target - TF.position;
        bulletSize = size;
        liveRange *= bulletSize;
    }

    public void OnDespawn()
    {
        SimplePool.Despawn(this);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_CHARACTER))
        {
            CharacterObject target = Cache.GetCharacter(other);
            if (target != character && !target.IsDead)
            {
                OnDespawn();
                target.OnDeath(character);
                character.RemoveTarget(target);
                character.OnKill();
            }
        }
    }
}