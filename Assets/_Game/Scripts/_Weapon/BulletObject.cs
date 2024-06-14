using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class BulletObject : GameUnit
{
    [SerializeField] protected float moveSpeed = 8f;
    protected CharacterObject character;
    protected Action<CharacterObject, CharacterObject> onHit;
    private float liveTime;
    private float bulletSize;

    private void OnEnable()
    {
        bulletSize = 1f;
        liveTime = 1f;
    }

    protected virtual void Update()
    {
        liveTime -= Time.deltaTime;
        if (liveTime < 0f)
        {
            OnDespawn();
        }
    }

    public virtual void OnInit(CharacterObject character, Vector3 target, float size)
    {
        this.character = character;
        TF.forward = (target - TF.position).normalized;
        bulletSize = size;
        liveTime *= bulletSize;
    }

    public virtual void OnInit(CharacterObject attacker, Action<CharacterObject, CharacterObject> onHit)
    {
        this.character = attacker;
        this.onHit = onHit;
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
            if (target != character)
            {
                OnDespawn();
                target.OnDeath();
            }
        }
    }
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag(Constant.TAG_CHARACTER))
    //     {
    //         CharacterObject victim = Cache.GetCharacter(other);
    //         onHit?.Invoke(character, victim);
    //     }
    // }
}