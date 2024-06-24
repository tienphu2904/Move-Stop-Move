using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Failure : UICanvas
{
    [SerializeField] private TextMeshProUGUI rankText, nameText, coinText;

    public void SetupData(string rank, string name, int coinAmount)
    {
        UpdateRankText(rank);
        UpdateNameText(name);
        UpdateCoin(coinAmount);
    }

    public void MainMenuButton()
    {
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI<MainMenu>();
        GameManager.Ins.ChangeState(GameState.MainMenu);
    }

    private void UpdateRankText(string text)
    {
        rankText.text = $"#{text}";
    }

    private void UpdateNameText(string text)
    {
        nameText.text = text;
    }

    private void UpdateCoin(int coinAmount)
    {
        coinText.text = coinAmount.ToString();
    }
}
