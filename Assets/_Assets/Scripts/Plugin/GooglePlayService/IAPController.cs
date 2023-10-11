using UnityEngine;
using UnityEngine.Purchasing;

public class IAPController : MonoBehaviour
{
    [SerializeField] DataUser _dataUser;
    [SerializeField] CoinsUI _coinsUI;
    private void Start()
    {
        _coinsUI = FindObjectOfType<CoinsUI>();
    }
    public void BuyProduct(Product product)
    {
        if (product.definition.id == "bigfeatured")
        {
            _dataUser.RewardProduct(250, TypeProduct.Coins);
            _dataUser.RewardProduct(0, TypeProduct.RemoveAds);
            _dataUser.RewardProduct(3, TypeProduct.SwapObj);
            _dataUser.RewardProduct(2, TypeProduct.MoreQueue);
            _dataUser.RewardProduct(1, TypeProduct.RevertObj);
        }
        else if (product.definition.id == "noads")
        {
            _dataUser.RewardProduct(0, TypeProduct.RemoveAds);
        }
        else if (product.definition.id == "coinsproduct1")
        {
            _dataUser.RewardProduct(150, TypeProduct.Coins);
        }
        _coinsUI.ShowCoins();
    }
    public void Buyfail(Product product, PurchaseFailureReason purchaseFailureReason)
    {
        Debug.Log("fail");
    }
    public void BuyCoins()
    {
        _dataUser.BuyCoins(500);
        _coinsUI.ShowCoins();
    }
    public void Pay100CoinsToPlayOn(int value)
    {
        DataUser.MinusCoins(value);
    }
}
public class DataUser
{
    public static int Coins;
    public static bool RemoveAds;
    public static int ItemRevertObj;
    public static int ItemMoreQueue;
    public static int ItemSwapObjPosition;
    public void RewardProduct(int value, TypeProduct typeProduct)
    {
        switch (typeProduct)
        {
            case TypeProduct.Coins:
                Coins += value;
                break;
            case TypeProduct.RemoveAds:
                RemoveAds = true;
                break;
            case TypeProduct.MoreQueue:
                ItemMoreQueue += value;
                break;
            case TypeProduct.RevertObj:
                ItemRevertObj += value;
                break;
            case TypeProduct.SwapObj:
                ItemSwapObjPosition += value;
                break;
        }
    }
    public static void MinusCoins(int value)
    {
        Coins -= value;
    }
    public void BuyCoins(int value)
    {
        Coins += value;
    }
    public void SaveData()
    {
        //QuickSaveWriter writer = QuickSaveWriter.Create("test");
        //writer.Write("key1", LifeUser);
        //writer.Write("key2", time);
        //writer.Write("key3", maxLife);
        //writer.Write("key4", minLife);
        //writer.Commit();
    }
}
public enum TypeProduct
{
    Coins,
    RemoveAds,
    RevertObj,
    MoreQueue,
    SwapObj
}