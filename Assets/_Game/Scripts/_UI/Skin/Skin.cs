using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin : UICanvas
{
    [SerializeField] private CameraFollow cameraFollow;

    private void OnEnable()
    {
        LevelManager.Ins.player.ChangeAnim(Constant.ANIM_DANCE);
        // cameraFollow.SetTarget(LevelManager.Ins.player.transform.position + Vector3.up * 2);
    }

    public void CloseButton()
    {
        UIManager.Ins.OpenUI<MainMenu>();
        Close(0);
    }
}
