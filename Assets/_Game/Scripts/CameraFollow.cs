using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraFollowType
{
    MainMenu,
    GamePlay,
    Shop
}

public class CameraFollow : Singleton<CameraFollow>
{
    public Transform tf;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform offsetGameplay, offsetMainMenu, offsetShop;
    private Vector3 offset;
    private Quaternion targetRotation;

    private void LateUpdate()
    {
        tf.position = Vector3.Lerp(tf.position, playerTransform.position + offset, Time.deltaTime * 100f);
        tf.rotation = Quaternion.Lerp(tf.rotation, targetRotation, Time.deltaTime * 100f);
        // tf.LookAt(target);
    }

    public void ChangeCameraType(CameraFollowType cameraType)
    {
        switch (cameraType)
        {
            case CameraFollowType.MainMenu:
                offset = offsetMainMenu.localPosition;
                targetRotation = offsetMainMenu.localRotation;
                break;
            case CameraFollowType.GamePlay:
                offset = offsetGameplay.localPosition;
                targetRotation = offsetGameplay.localRotation;
                break;
            case CameraFollowType.Shop:
                offset = offsetShop.localPosition;
                targetRotation = offsetShop.localRotation;
                break;
        }
    }
}
