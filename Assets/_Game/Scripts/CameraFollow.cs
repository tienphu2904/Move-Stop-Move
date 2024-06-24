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
    private Vector3 originalOffset;
    private Quaternion targetRotation;
    private CameraFollowType cameraType;

    private void LateUpdate()
    {
        tf.position = Vector3.Lerp(tf.position, playerTransform.position + offset, Time.deltaTime * 100f);
        tf.rotation = Quaternion.Lerp(tf.rotation, targetRotation, Time.deltaTime * 100f);
        tf.LookAt(cameraType == CameraFollowType.GamePlay ? playerTransform : null);
    }

    public void ChangeCameraType(CameraFollowType cameraType)
    {
        this.cameraType = cameraType;
        switch (cameraType)
        {
            case CameraFollowType.MainMenu:
                originalOffset = offsetMainMenu.localPosition;
                targetRotation = offsetMainMenu.localRotation;
                break;
            case CameraFollowType.GamePlay:
                originalOffset = offsetGameplay.localPosition;
                targetRotation = offsetGameplay.localRotation;
                break;
            case CameraFollowType.Shop:
                originalOffset = offsetShop.localPosition;
                targetRotation = offsetShop.localRotation;
                break;
        }
        offset = originalOffset;
    }

    public void UpdateWithPlayerSize(float size)
    {
        offset = originalOffset * size;
    }
}
