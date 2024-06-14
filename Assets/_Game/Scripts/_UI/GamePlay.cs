using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : UICanvas
{
    private void OnEnable()
    {
        GameManager.Ins.ChangeState(GameState.Gameplay);
        CameraFollow.Ins.ChangeCameraType(CameraFollowType.GamePlay);
        LevelManager.Ins.player.Setup();
    }

    public void pauseButton()
    {
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI<MainMenu>();
        GameManager.Ins.ChangeState(GameState.MainMenu);
    }
}
