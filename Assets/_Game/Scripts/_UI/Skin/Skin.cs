using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Skin : UICanvas
{
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private SkinItemList skinItemComponent;

    private int coinValue => DataManager.Ins.CoinValue;

    private void OnEnable()
    {
        LevelManager.Ins.player.ChangeAnimation(Constant.ANIM_IS_DANCE);
        CameraFollow.Ins.ChangeCameraType(CameraFollowType.Shop);
        UpdateCoinText();
    }

    public void CloseButton()
    {
        UIManager.Ins.OpenUI<MainMenu>();
        skinItemComponent.EquipCorrectSkin();
        Close(0);
    }

    public void UpdateCoinText()
    {
        coinText.text = coinValue.ToString();
    }
}
