
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
    private static readonly List<string> _mandatoryKeyList = new List<string> { "ShopData" };
    public static readonly ReadOnlyCollection<string> MandatoryKeyList = _mandatoryKeyList.AsReadOnly();
    public UserData UserData { get => LoadData(); set => SaveData(value); }

    private void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, "savefile.dat");
        // LoadData();
        // GetDefaultData();
        // SaveData();
        // LoadData();
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

    // public void GetDataFromFile()
    // {
    //     try
    //     {
    //         var fs = File.Open(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
    //         BinaryFormatter bf = new BinaryFormatter();
    //         _userData = (UserData)bf.Deserialize(fs);
    //         fs.Close();
    //     }
    //     catch
    //     {
    //         _userData = new UserData();
    //     }
    // }

    // public void WriteAllDataToFile()
    // {
    //     var fs = File.Open(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
    //     BinaryFormatter bf = new BinaryFormatter();
    //     bf.Serialize(fs, _userData);
    //     fs.Close();
    // }

    public UserData GetDefaultData()
    {
        UserData userData = new();

        List<ShopItem> shopItemLists = GenerateDefaultShopData();
        List<WeaponShopItem> weaponShopItemLists = GenerateDefaultWeaponData();
        PlayerData playerData = GenerateDefaultPlayerData(weaponShopItemLists);

        userData.SetData("ShopData", shopItemLists);
        userData.SetData("WeaponShopData", weaponShopItemLists);
        userData.SetData("PlayerData", playerData);

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
