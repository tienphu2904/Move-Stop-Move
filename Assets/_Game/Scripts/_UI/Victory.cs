using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Victory : UICanvas
{
    [SerializeField] private TextMeshProUGUI coinText;
    public void MainMenuButton()
    {
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI<MainMenu>();
        GameManager.Ins.ChangeState(GameState.MainMenu);
    }

    public void UpdateCoin(int coinAmount)
    {
        coinText.text = coinAmount.ToString();
    }
}
