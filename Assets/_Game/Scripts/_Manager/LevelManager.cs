using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public WeaponItemData weaponItemData;
    public ShopItemData shopItemData;
    public Player player;
    public Map currentMap;
    public List<Bot> botList = new List<Bot>();

    public float totalCharacter, remainingBot, maxBot;

    public void OnInit()
    {
        OnReset();
        totalCharacter = currentMap.TotalBot;
        maxBot = currentMap.MaxBot;
        remainingBot = totalCharacter - 1;

        player.OnInit();
        player.TF.position = currentMap.transform.position;
        player.ChangeAnimation(Constant.ANIM_IS_IDLE);

        for (int i = botList.Count; i < maxBot; i++)
        {
            SpawmNewBot();
        }
    }

    public void OnPlay()
    {
        UIManager.Ins.GetUI<GamePlay>().UpdatePlayerAmount(totalCharacter.ToString());
        player.OnPlay();
        foreach (Bot bot in botList)
        {
            bot.OnPlay();
        }
    }

    public void OnReset()
    {
        foreach (Bot bot in botList)
        {
            bot.OnDespawn();
        }
        SimplePool.ReleaseAll();
        botList.Clear();
    }

    public void OnWin()
    {
        int coinAmount = player.killNumber * 5;
        UIManager.Ins.OpenUI<Victory>().UpdateCoin(coinAmount);
        GameManager.Ins.ChangeState(GameState.Lose);
        UpdateCoin(coinAmount);
        Debug.Log("Win");
    }

    public IEnumerator OnLose(CharacterObject attacker)
    {
        int coinAmount = player.killNumber * 5;
        yield return new WaitForSeconds(1f);
        UIManager.Ins.OpenUI<Failure>().SetupData((totalCharacter - 1).ToString(), attacker.characterName, coinAmount);
        GameManager.Ins.ChangeState(GameState.Lose);
        UpdateCoin(coinAmount);
        Debug.Log("Lose");
    }

    public void OnCharacterDeath(CharacterObject attacker, CharacterObject deadCharacter)
    {
        UIManager.Ins.GetUI<GamePlay>().UpdatePlayerAmount(totalCharacter--.ToString());
        if (deadCharacter is Player)
        {
            StartCoroutine(OnLose(attacker));
        }
        else if (deadCharacter is Bot)
        {
            botList.Remove(deadCharacter as Bot);
            if (remainingBot > 0)
            {
                SpawmNewBot();
            }

            if (totalCharacter == 1 && GameManager.Ins.IsState(GameState.Gameplay))
            {
                Invoke(nameof(OnWin), 1f);
            }
        }
    }

    public Vector3 GetRandomPointToSpawnBot()
    {
        float minDistanceSpawnBot = 10f;
        List<Vector3> spawnPoints = new List<Vector3>();
        List<Vector3> validSpawnPoints = new List<Vector3>();

        for (int i = 0; i < 50; i++)
        {
            spawnPoints.Add(currentMap.GetRandomPointOnNavMesh(currentMap.transform.position));
        }

        foreach (Vector3 spawnPoint in spawnPoints)
        {
            bool isValid = true;
            foreach (Bot bot in botList)
            {
                if (Vector3.Distance(spawnPoint, bot.TF.position) < minDistanceSpawnBot)
                {
                    isValid = false;
                    break;
                }
                if (Vector3.Distance(spawnPoint, player.TF.position) < minDistanceSpawnBot)
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
        return validSpawnPoints[randomIndex];
    }

    public void SpawmNewBot()
    {
        Bot bot = SimplePool.Spawn<Bot>(PoolType.Bot, GetRandomPointToSpawnBot(), Quaternion.identity);
        bot.OnInit();
        bot.currentMap = currentMap;
        botList.Add(bot);
        remainingBot--;
        if (GameManager.Ins.IsState(GameState.Gameplay))
        {
            bot.OnPlay();
        }
    }

    private void UpdateCoin(int coin)
    {
        DataManager.Ins.UpdatePlayerData(coinAmount: coin);
    }
}
