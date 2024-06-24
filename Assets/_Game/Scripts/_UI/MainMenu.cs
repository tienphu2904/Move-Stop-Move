using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenu : UICanvas
{
    [SerializeField] private TextMeshProUGUI coinText;

    private void OnEnable()
    {
        LevelManager.Ins.OnInit();
        CameraFollow.Ins.ChangeCameraType(CameraFollowType.MainMenu);
        UpdateCoinValue();
    }

    public void UpdateCoinValue()
    {
        coinText.text = DataManager.Ins.CoinValue.ToString();
    }

    public void OpenShopButton()
    {
        Debug.Log("OpenShopButton");
    }

    public void AdsButton()
    {
        Debug.Log("AdsButton");
    }

    public void VibrateButton()
    {
        Debug.Log("VibrateButton");
    }

    public void SoundButton()
    {
        Debug.Log("SoundButton");
    }

    public void WeaponButton()
    {
        Debug.Log("WeaponButton");
        UIManager.Ins.OpenUI<Weapon>();
        Close(0);
    }

    public void SkinButton()
    {
        Debug.Log("SkinButton");
        UIManager.Ins.OpenUI<Skin>();
        Close(0);
    }

    public void PlayButton()
    {
        Debug.Log("PlayButton");
        UIManager.Ins.OpenUI<GamePlay>();
        LevelManager.Ins.OnPlay();
        Close(0);
    }
}
