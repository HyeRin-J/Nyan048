using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPMgr : MonoBehaviour, IStoreListener
{
    private static IStoreController storeController;
    private static IExtensionProvider extensionProvider;

    public static string[] EraseID = { "erase_5", "erase_10", "erase_30", "erase_50", "erase_100" };
    public static string[] UpgradeID = { "upgrade_5", "upgrade_10", "upgrade_30", "upgrade_50", "upgrade_100" };
    public static string[] SkinID = { "first_cat_thief_skin", "first_cat_astro_skin" };
    public static string NoAdsID = "no_ads";

    public ItemManager itemManager;
    public SkinPanelManager skinPanelManager;

    public UnityEngine.UI.Button skinButton;

    private void Awake()
    {
        if (storeController == null)
            InitializePurchasing();
    }

    public void InitializePurchasing()
    {
        if (IsInitalized()) return;

        //상품 등록 부분
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        
        for (int i = 0; i < EraseID.Length; i++)
            builder.AddProduct(EraseID[i], ProductType.Consumable, new IDs() { { EraseID[i], GooglePlay.Name } });
        for (int i = 0; i < UpgradeID.Length; i++)
            builder.AddProduct(UpgradeID[i], ProductType.Consumable, new IDs() { { UpgradeID[i], GooglePlay.Name } });
        for (int i = 0; i < SkinID.Length; i++)
            builder.AddProduct(SkinID[i], ProductType.NonConsumable, new IDs() { { SkinID[i], GooglePlay.Name } });

        builder.AddProduct(NoAdsID, ProductType.NonConsumable, new IDs() { { NoAdsID, GooglePlay.Name } });

        UnityPurchasing.Initialize(this, builder);

        Debug.Log("Initialize Complete");
    }

    public bool HasRecipt(string id)
    {
        MonoBehaviour.print("HasRecipt no_ads");

        foreach(Product product in storeController.products.all)
        {
            if (product.definition.id.Equals(id) && product.hasReceipt)
            {
                MonoBehaviour.print("HasRecipt True");
                return true;
            }
        }
        MonoBehaviour.print("HasRecipt False");
        return false;
    }

    public bool IsInitalized()
    {
        return storeController != null && extensionProvider != null;
    }

    public void BuyErase(int num)
    {
        BuyProductId("erase_" + num);
    }

    public void BuyUpgrade(int num)
    {
        BuyProductId("upgrade_" + num);
    }

    public void BuyNoAds()
    {
        BuyProductId("no_ads");
    }

    public void BuySkin(UnityEngine.UI.Button skinButton)
    {
        this.skinButton = skinButton;
    }

    public void BuySkin(string skinName)
    {
        BuyProductId(skinName);
    }

    void BuyProductId(string productID)
    {
        if (IsInitalized())
        {
            Product product = storeController.products.WithID(productID);

            if (product != null && product.availableToPurchase)
                storeController.InitiatePurchase(product);
            else
                Debug.Log("BuyProductID : FAILED, Not purchasing product. is not found or not available for purchase.");
        }
        else
            Debug.Log("BuyProductID : FAILED, Not initialized.");
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        storeController = controller;
        extensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log(string.Format("OnInitialized FAIL Reason : {0}", error));
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        switch (args.purchasedProduct.definition.id)
        {
            case "erase_5":
                Debug.Log("구매 성공");
                itemManager.eraseItemAmount += 5;
                break;
            case "erase_10":
                Debug.Log("구매 성공");
                itemManager.eraseItemAmount += 10;
                break;
            case "erase_30":
                Debug.Log("구매 성공");
                itemManager.eraseItemAmount += 30;
                break;
            case "erase_50":
                Debug.Log("구매 성공");
                itemManager.eraseItemAmount += 50;
                break;
            case "erase_100":
                Debug.Log("구매 성공");
                itemManager.eraseItemAmount += 100;
                break;
            case "upgrade_5":
                Debug.Log("구매 성공");
                itemManager.upgradeItemAmount += 5;
                break;
            case "upgrade_10":
                Debug.Log("구매 성공");
                itemManager.upgradeItemAmount += 10;
                break;
            case "upgrade_30":
                Debug.Log("구매 성공");
                itemManager.upgradeItemAmount += 30;
                break;
            case "upgrade_50":
                Debug.Log("구매 성공");
                itemManager.upgradeItemAmount += 50;
                break;
            case "upgrade_100":
                Debug.Log("구매 성공");
                itemManager.upgradeItemAmount += 100;
                break;
            case "no_ads":
                itemManager.ads.PurchasingNoAds();
                break;
            case "first_cat_thief_skin":
            case "first_cat_astro_skin":
                skinPanelManager.PurchasingSkin(args.purchasedProduct.definition.id);
                skinButton.interactable = false;
                skinButton.GetComponentsInChildren<UnityEngine.UI.Text>()[1].text = "이미 구매";
                break;
            default:
                Debug.Log(string.Format("Unrecognized product : {0}", args.purchasedProduct.definition.id));
                break;
        }
        PlayerPrefs.SetInt("EraseAmount", itemManager.eraseItemAmount);
        PlayerPrefs.SetInt("UpgradeAmount", itemManager.upgradeItemAmount);
        
        itemManager.ItemAmountSetting();
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        Debug.Log(string.Format("Purchase FAILED : {0}, REASON : {1}", product.definition.id, reason));
    }

}
