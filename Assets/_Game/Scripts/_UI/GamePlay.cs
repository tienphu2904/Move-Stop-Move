using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePlay : UICanvas
{
    [SerializeField] private TextMeshProUGUI playerAmount;

    private void OnEnable()
    {
        GameManager.Ins.ChangeState(GameState.Gameplay);
        CameraFollow.Ins.ChangeCameraType(CameraFollowType.GamePlay);
    }

    public void pauseButton()
    {
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI<MainMenu>();
        GameManager.Ins.ChangeState(GameState.MainMenu);
    }

    public void UpdatePlayerAmount(string text)
    {
        playerAmount.text = text;
    }
}
