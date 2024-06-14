using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class PoolControler : MonoBehaviour
{
    [Header("---- POOL CONTROLER TO INIT POOL ----")]
    //[Header("Put object pool to list Pool or Resources/Pool")]
    //[Header("Preload: Init Poll")]
    //[Header("Spawn: Take object from pool")]
    //[Header("Despawn: return object to pool")]
    //[Header("Collect: return objects type to pool")]
    //[Header("CollectAll: return all objects to pool")]

    [Space]
    [Header("Pool")]
    public List<PoolAmount> Pool;

    [Header("Particle")]
    public ParticleAmount[] Particle;


    public void Awake()
    {
        for (int i = 0; i < Pool.Count; i++)
        {
            SimplePool.Preload(Pool[i].prefab, Pool[i].amount, Pool[i].root, Pool[i].collect);
        }

        for (int i = 0; i < Particle.Length; i++)
        {
            ParticlePool.Preload(Particle[i].prefab, Particle[i].amount, Particle[i].root);
            ParticlePool.Shortcut(Particle[i].particleType, Particle[i].prefab);
        }
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(PoolControler))]
public class PoolControlerEditor : Editor
{
    PoolControler pool;

    private void OnEnable()
    {
        pool = (PoolControler)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Create Quick Root"))
        {
            for (int i = 0; i < pool.Pool.Count; i++)
            {
                if (pool.Pool[i].root == null)
                {
                    Transform tf = new GameObject(pool.Pool[i].prefab.poolType.ToString()).transform;
                    tf.parent = pool.transform;
                    pool.Pool[i].root = tf;
                }
            }

            for (int i = 0; i < pool.Particle.Length; i++)
            {
                if (pool.Particle[i].root == null)
                {
                    Transform tf = new GameObject(pool.Particle[i].particleType.ToString()).transform;
                    tf.parent = pool.transform;
                    pool.Particle[i].root = tf;
                }
            }
        }

        if (GUILayout.Button("Get Prefab Resource"))
        {
            GameUnit[] resources = Resources.LoadAll<GameUnit>("Pool");

            for (int i = 0; i < resources.Length; i++)
            {
                bool isDuplicate = false;
                for (int j = 0; j < pool.Pool.Count; j++)
                {
                    if (resources[i].poolType == pool.Pool[j].prefab.poolType)
                    {
                        isDuplicate = true;
                        break;
                    }
                }

                if (!isDuplicate)
                {
                    Transform root = new GameObject(resources[i].name).transform;

                    PoolAmount newPool = new PoolAmount(root, resources[i], SimplePool.DEFAULT_POOL_SIZE, true);

                    pool.Pool.Add(newPool);
                }
            }
        }
    }
}

#endif

[System.Serializable]
public class PoolAmount
{
    [Header("-- Pool Amount --")]
    public Transform root;
    public GameUnit prefab;
    public int amount;
    public bool collect;

    public PoolAmount(Transform root, GameUnit prefab, int amount, bool collect)
    {
        this.root = root;
        this.prefab = prefab;
        this.amount = amount;
        this.collect = collect;
    }
}


[System.Serializable]
public class ParticleAmount
{
    public Transform root;
    public ParticleType particleType;
    public ParticleSystem prefab;
    public int amount;
}


public enum ParticleType
{
    Hit
}

public enum PoolType
{
    None = 0,

    Bot = 1,

    Weapon_Hammer = 2,
    Weapon_Lollipop = 3,
    Weapon_Knife = 4,
    Weapon_CandyCane = 5,
    Weapon_Boomerang = 6,
    Weapon_SwirlyPop = 7,
    Weapon_Axe = 8,
    Weapon_IceCreamCone = 9,
    Weapon_BattleAxe = 10,
    Weapon_Arrow = 11,
    Weapon_Uzi = 12,

    Bullet_Hammer = 13,
    Bullet_Lollipop = 14,
    Bullet_Knife = 15,
    Bullet_CandyCane = 16,
    Bullet_Boomerang = 17,
    Bullet_SwirlyPop = 18,
    Bullet_Axe = 19,
    Bullet_IceCreamCone = 20,
    Bullet_BattleAxe = 21,
    Bullet_Arrow = 22,
    Bullet_Uzi = 23,

    Head_Arrow = 24,
    Head_Cowboy = 25,
    Head_Crown = 26,
    Head_Ear = 27,
    Head_Hat = 28,
    Head_HatCap = 29,
    Head_HatYellow = 30,
    Head_HeadPhone = 31,
    Head_Horn = 32,
    Head_Beard = 33,
    Head_WitchHat = 34,
    Head_Angel = 35,
    Head_ThorHat = 36,

    Back_AngelWing = 37,
    Back_Blade = 38,
    Back_DevilWing = 39,

    LeftHand_1 = 40,
    LeftHand_2 = 41,
    LeftHand_Book = 42,
    LeftHand_Bow = 43,

    Tail_DevilTail = 44,

    Skin_Default = 45,
    Skin_Devil = 46,
    Skin_Angel = 47,
    Skin_Witch = 48,
    Skin_DeadPool = 49,
    Skin_Thor = 50,
}


