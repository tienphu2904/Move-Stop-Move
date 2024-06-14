using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Failure : UICanvas
{
    public void MainMenuButton()
    {
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI<MainMenu>();
        GameManager.Ins.ChangeState(GameState.MainMenu);
    }
}
