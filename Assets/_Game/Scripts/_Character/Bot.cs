using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Bot : CharacterObject
{
    [Header("Bot")]
    [SerializeField] private SkinItemData bodySkinData;
    [SerializeField] private NameData nameData;
    private NavMeshAgent agent;
    private Vector3 destionation;
    public bool IsDestination => Vector3.Distance(destionation, Vector3.right * TF.position.x + Vector3.forward * TF.position.z) < 0.1f;
    IState<Bot> currentState;

    public Map currentMap;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
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
        //random body mat
        SkinItem bodySkin = bodySkinData.itemDataList[Random.Range(0, bodySkinData.itemDataList.Count - 1)];
        ChangeSkin(bodySkin);
        //equip weapon
        List<WeaponItem> weaponDataList = LevelManager.Ins.weaponItemData.itemDataList;
        characterSkin.ChangeWeapon(weaponDataList[Random.Range(0, weaponDataList.Count)]);
        //equip skin
        List<SkinItem> skinDataList = LevelManager.Ins.shopItemData.SkinItemDataList[Random.Range(0, 4)].itemDataList;
        characterSkin.ChangeSkin(skinDataList[Random.Range(0, skinDataList.Count)]);
        //random name
        GetRandomName();

        ChangeState(new IdleState());
    }

    public override void OnPlay()
    {
        nameTag.OnInit(characterName, characterSkin.CurrentBodyMat.color);
        ChangeState(new PatrolState());
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        characterSkin.OnDespawn();
        SimplePool.Despawn(this);
    }

    public override void OnAttack()
    {
        base.OnAttack();
    }

    public override void OnDeath(CharacterObject attacker)
    {
        base.OnDeath(attacker);
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

    private void GetRandomName()
    {
        characterName = nameData.characterNames[Random.Range(0, nameData.characterNames.Count)];
    }
}
