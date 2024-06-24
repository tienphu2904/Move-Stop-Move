
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Unity.VisualScripting;
using System.Linq;

public class DataManager : Singleton<DataManager>
{
    [SerializeField] private ShopItemData shopItemData;
    [SerializeField] private WeaponItemData weaponItemData;

    // private UserData _userData;
    private string filePath;
    private static readonly List<string> _mandatoryKeyList = new List<string> { Constant.DATA_SHOPDATA };
    public static readonly ReadOnlyCollection<string> MandatoryKeyList = _mandatoryKeyList.AsReadOnly();
    public int CoinValue => ((PlayerData)UserData.Dict[Constant.DATA_PLAYERDATA]).coin;
    public List<ShopItem> shopItemList => UserData.Dict[Constant.DATA_SHOPDATA] as List<ShopItem>;
    public List<WeaponShopItem> weaponItemList => UserData.Dict[Constant.DATA_WEAPONDATA] as List<WeaponShopItem>;
    public UserData UserData { get => LoadData(); set => SaveData(value); }


    private void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, "savefile.dat");
    }

    public void SaveData(UserData userData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        formatter.Serialize(stream, userData);
    }

    public UserData LoadData()
    {
        UserData userData;
        BinaryFormatter formatter = new BinaryFormatter();
        try
        {
            using FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            userData = (UserData)formatter.Deserialize(stream);
        }
        catch
        {
            userData = GetDefaultData();
        }
        return userData;
    }

    public void UpdatePlayerData(int coinAmount, ShopItem shopItem = null, WeaponShopItem weaponShopItem = null)
    {
        UserData userData = UserData;
        PlayerData playerData = userData.GetData(Constant.DATA_PLAYERDATA, new PlayerData());
        playerData.coin += coinAmount;
        if (shopItem != null)
        {
            playerData.currentSkinItem = shopItem;
        }
        if (weaponShopItem != null)
        {
            playerData.currentWeaponItem = weaponShopItem;
        }

        userData.SetData(Constant.DATA_PLAYERDATA, playerData);
        UserData = userData;
    }

    private UserData GetDefaultData()
    {
        UserData userData = new();

        List<ShopItem> shopItemLists = GenerateDefaultShopData();
        List<WeaponShopItem> weaponShopItemLists = GenerateDefaultWeaponData();
        PlayerData playerData = GenerateDefaultPlayerData(weaponShopItemLists);

        userData.SetData(Constant.DATA_SHOPDATA, shopItemLists);
        userData.SetData(Constant.DATA_WEAPONDATA, weaponShopItemLists);
        userData.SetData(Constant.DATA_PLAYERDATA, playerData);

        return userData;
    }

    private List<ShopItem> GenerateDefaultShopData()
    {
        List<ShopItem> shopItemLists = new List<ShopItem>();
        for (int i = 0; i < shopItemData.SkinItemDataList.Count; i++)
        {
            List<SkinItem> itemDataList = shopItemData.SkinItemDataList[i].itemDataList;
            for (int j = 0; j < itemDataList.Count; j++)
            {
                ShopItem shopItem = new()
                {
                    shopCategory = (ShopCategory)i,
                    id = itemDataList[j].id,
                    statusType = i == 0 && j == 0 ? StatusType.Equipped : StatusType.Lock,
                };
                shopItemLists.Add(shopItem);
            }
        }
        return shopItemLists;
    }

    private List<WeaponShopItem> GenerateDefaultWeaponData()
    {
        List<WeaponShopItem> weaponItemLists = new List<WeaponShopItem>();

        for (int i = 0; i < weaponItemData.itemDataList.Count; i++)
        {
            WeaponShopItem weaponItem = new()
            {
                id = weaponItemData.itemDataList[i].id,
                statusType = i == 0 ? StatusType.Equipped : StatusType.Lock,
            };
            weaponItemLists.Add(weaponItem);
        }
        return weaponItemLists;
    }

    private PlayerData GenerateDefaultPlayerData(List<WeaponShopItem> weaponShopItemLists)
    {

        PlayerData playerData = new()
        {
            coin = 1000,
            currentWeaponItem = weaponShopItemLists.Find(i => i.statusType == StatusType.Equipped),
            currentSkinItem = new ShopItem()
        };
        return playerData;
    }
}
