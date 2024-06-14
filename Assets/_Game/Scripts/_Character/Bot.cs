using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : CharacterObject
{
    private NavMeshAgent agent;
    private Vector3 destionation;
    public bool IsDestination => Vector3.Distance(destionation, Vector3.right * TF.position.x + Vector3.forward * TF.position.z) < 0.1f;
    IState<Bot> currentState;

    public Map currentMap;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        characterSkin.ChangeWeapon(LevelManager.Ins.weaponItemData.itemDataList[0]);
        ChangeState(new IdleState());
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.OnExcute(this);
        }
    }

    public void SetDestination(Vector3 position)
    {
        agent.enabled = true;
        destionation = position;
        destionation.y = 0;
        agent.SetDestination(position);
    }

    public void ChangeState(IState<Bot> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public override void OnInit()
    {
        base.OnInit();
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        SimplePool.Despawn(this);
    }

    public override void OnAttack()
    {
        base.OnAttack();
    }

    public override void OnDeath()
    {
        base.OnDeath();
        agent.enabled = false;
        ChangeState(null);
        EnableTargetMark(false);
        Invoke(nameof(OnDespawn), 2f);
    }

    internal void OnStop()
    {
        agent.enabled = false;
        ChangeAnimation(Constant.ANIM_IS_IDLE);
    }

    public override void AddTarget(CharacterObject target)
    {
        base.AddTarget(target);
    }
}
