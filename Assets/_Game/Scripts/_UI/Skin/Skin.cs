using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Skin : UICanvas
{
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private SkinItemList skinItemComponent;

    private int coinValue => ((PlayerData)DataManager.Ins.UserData.Dict["PlayerData"]).coin;

    private void OnEnable()
    {
        LevelManager.Ins.player.ChangeAnimation(Constant.ANIM_IS_DANCE);
        CameraFollow.Ins.ChangeCameraType(CameraFollowType.Shop);
        UpdateCoinText();
    }

    public void CloseButton()
    {
        UIManager.Ins.OpenUI<MainMenu>();
        skinItemComponent.OnTrySkinItem(skinItemComponent.EquippedSkin());
        Close(0);
    }


    private void UpdateCoinText()
    {
        coinText.text = coinValue.ToString();
    }
}
