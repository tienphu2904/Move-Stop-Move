using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : UICanvas
{
    private void OnEnable()
    {
        LevelManager.Ins.player.ChangeAnim(Constant.ANIM_IDLE);
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
    }
}
