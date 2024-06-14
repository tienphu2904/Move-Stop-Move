using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private SkinItemData bodySkinData;
    public WeaponItemData weaponItemData;
    public ShopItemData shopItemData;
    public Player player;
    public Map currentMap;
    public List<Bot> botList = new List<Bot>();

    private float totalBot, maxBot;

    public void OnInit()
    {
        // SimplePool.CollectAll();
        // botList.Clear();
        totalBot = currentMap.TotalBot;
        maxBot = currentMap.MaxBot;
        player.Setup();
        player.ChangeAnimation(Constant.ANIM_IS_IDLE);
    }

    private void Update()
    {
        if (totalBot > 0)
        {
            for (int i = botList.Count; i < maxBot; i++)
            {
                SpawmNewBot();
                totalBot--;
            }
        }
    }

    public void OnPlay()
    {
        foreach (Bot bot in botList)
        {
            bot.ChangeState(new PatrolState());
        }
    }

    public void OnWin()
    {

    }

    public void OnLose()
    {

    }

    public void OnBotDeath()
    {

    }

    public Vector3 GetRandomPointToSpawnBot()
    {
        float minDistanceSpawnBot = 7f;
        List<Transform> validSpawnPoints = new List<Transform>();

        foreach (Transform spawnPoint in currentMap.spawnBotTransformList)
        {
            bool isValid = true;
            foreach (Bot bot in botList)
            {
                if (Vector3.Distance(spawnPoint.position, bot.transform.position) < minDistanceSpawnBot)
                {
                    isValid = false;
                    break;
                }
            }
            if (isValid)
            {
                validSpawnPoints.Add(spawnPoint);
            }
        }

        if (validSpawnPoints.Count == 0)
        {
            return Vector3.zero;
        }

        int randomIndex = Random.Range(0, validSpawnPoints.Count);
        return validSpawnPoints[randomIndex].position;
    }

    public void SpawmNewBot()
    {
        if (totalBot > 0)
        {
            Bot bot = SimplePool.Spawn<Bot>(PoolType.Bot, GetRandomPointToSpawnBot(), Quaternion.identity);
            bot.currentMap = currentMap;

            SkinItem bodySkin = bodySkinData.itemDataList[Random.Range(0, bodySkinData.itemDataList.Count - 1)];
            bot.ChangeSkin(bodySkin);
            botList.Add(bot);
            totalBot--;
        }
    }
}
