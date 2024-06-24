using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : UICanvas
{
    [SerializeField] private WeaponItemData weaponItemData;
    [SerializeField] private TextMeshProUGUI weaponName, weaponPrice, coinText;
    [SerializeField] private Image weaponImage;
    [SerializeField] private GameObject buyButton, selectButton, equippedButton;

    private WeaponItem currentWeapon;
    private WeaponShopItem currentWeaponData => (DataManager.Ins.UserData.Dict[Constant.DATA_WEAPONDATA] as List<WeaponShopItem>).Find(i => i.id == currentWeapon.id);
    private WeaponShopItem equippedWeaponData => (DataManager.Ins.UserData.Dict[Constant.DATA_WEAPONDATA] as List<WeaponShopItem>).Find(i => i.statusType == StatusType.Equipped);

    private int coinValue => DataManager.Ins.CoinValue;

    private void OnEnable()
    {
        LevelManager.Ins.player.ChangeAnimation(Constant.ANIM_IS_DANCE);
        CameraFollow.Ins.ChangeCameraType(CameraFollowType.Shop);
        OnTryWeapon(weaponItemData.itemDataList[equippedWeaponData.id]);
        UpdateCoinText();
    }

    //Button
    public void NextWeaponButton()
    {
        int index = currentWeapon.id == (weaponItemData.itemDataList.Count - 1) ? currentWeapon.id : currentWeapon.id + 1;
        OnTryWeapon(weaponItemData.itemDataList[index]);
    }

    public void PrevWeaponButton()
    {
        int index = currentWeapon.id == 0 ? currentWeapon.id : currentWeapon.id - 1;
        OnTryWeapon(weaponItemData.itemDataList[index]);
    }

    public void CloseButton()
    {
        UIManager.Ins.OpenUI<MainMenu>();
        OnTryWeapon(weaponItemData.itemDataList.Find(i => i.id == equippedWeaponData.id));
        Close(0);
    }

    public void BuyWeaponButton()
    {
        if (currentWeapon.cost < coinValue)
        {
            OnBuyWeapon();
            OnEquipWeapon();
            UpdateCoinText();
            SetStatusButton(equippedButtonStatus: true);
        }
    }

    public void SelectWeaponButton()
    {
        OnEquipWeapon();
        SetStatusButton(equippedButtonStatus: true);
    }

    private void SetStatusButton(bool buyButtonStatus = false,
                                    bool selectButtonStatus = false,
                                    bool equippedButtonStatus = false)
    {
        buyButton.SetActive(buyButtonStatus);
        selectButton.SetActive(selectButtonStatus);
        equippedButton.SetActive(equippedButtonStatus);
    }

    //Logic
    private void OnTryWeapon(WeaponItem item)
    {
        currentWeapon = item;
        weaponName.text = currentWeapon.name;
        weaponImage.sprite = currentWeapon.prbItemSprite;
        weaponPrice.text = currentWeapon.cost.ToString();
        LevelManager.Ins.player.ChangeWeapon(item);

        switch (currentWeaponData.statusType)
        {
            case StatusType.Lock:
                SetStatusButton(buyButtonStatus: true);
                break;
            case StatusType.Available:
                SetStatusButton(selectButtonStatus: true);
                break;
            case StatusType.Equipped:
                SetStatusButton(equippedButtonStatus: true);
                break;
        }
    }

    private void OnEquipWeapon()
    {
        UserData userData = DataManager.Ins.UserData;

        List<WeaponShopItem> weaponShopItem = userData.Dict[Constant.DATA_WEAPONDATA] as List<WeaponShopItem>;
        for (int i = 0; i < weaponShopItem.Count; i++)
        {
            weaponShopItem[i].statusType = weaponShopItem[i].statusType == StatusType.Equipped ? StatusType.Available : weaponShopItem[i].statusType;
            weaponShopItem[i].statusType = weaponShopItem[i].id == currentWeaponData.id ? StatusType.Equipped : weaponShopItem[i].statusType;
        }
        userData.SetData(Constant.DATA_WEAPONDATA, weaponShopItem);
        DataManager.Ins.UserData = userData;
    }

    private void OnBuyWeapon()
    {
        DataManager.Ins.UpdatePlayerData(coinAmount: -currentWeapon.cost);
    }

    private void UpdateCoinText()
    {
        coinText.text = coinValue.ToString();
    }
}
