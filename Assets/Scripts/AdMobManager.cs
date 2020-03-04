using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdMobManager : MonoBehaviour
{
    private BannerView bannerView;
    private InterstitialAd interstitial;
    private RewardBasedVideoAd rewardBasedVideo;

    public string AppId;
    public string BannerId;
    public string InterstitialId;
    public string EraseRewardId;
    public string UpgradeRewardId;

    public string TestDeviceId;

    private string rewardType;

    public GameObject noAdsButton;
    public IAPMgr iAPMgr;

    // Use this for initialization
    void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
#if UNITY_ANDROID
        string appId = AppId;
#elif UNITY_IPHONE
            string appId = "ca-app-pub-3940256099942544~1458002511";
#else
            string appId = "unexpected_platform";
#endif
        MobileAds.Initialize(appId);
        MonoBehaviour.print("MobileAds Initialize");

        // Initialize the Google Mobile Ads SDK.
        RequestBanner();
        RequestInterstitial();

        // Get singleton reward based video ad reference.
        rewardBasedVideo = RewardBasedVideoAd.Instance;

        // Called when the user should be rewarded for watching a video.
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        // Called when the ad is closed.
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;

        // Called when an ad is shown.
        interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        interstitial.OnAdClosed += HandleOnAdClosed;

        RequestRewardBasedVideo();
    }

    private void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = BannerId;
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);
        MonoBehaviour.print("New BannerView");

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().AddTestDevice(TestDeviceId).Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);

        //StartCoroutine(CheckPurchasingNoAds());
    }

    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = InterstitialId;
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().AddTestDevice(TestDeviceId).Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);
    }

    private void RequestRewardBasedVideo()
    {
        string adUnitId;
        AdRequest request = new AdRequest.Builder().AddTestDevice(TestDeviceId).Build();

        adUnitId = EraseRewardId;
        rewardBasedVideo.LoadAd(request, adUnitId);
    }

    public void PurchasingNoAds()
    {
        bannerView.Hide();
        noAdsButton.SetActive(false);
    }

    public void GameOver()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        ItemManager itemManager = GetComponent<ItemManager>();
        string itemName;

        if (rewardType.Equals("Erase"))
        {
            itemManager.eraseItemAmount += 3;
            itemName = "[지우개]";
            PlayerPrefs.SetInt("EraseAmount", itemManager.eraseItemAmount);
        }
        else
        {
            itemManager.upgradeItemAmount += 3;
            itemName = "[업그레이드]";
            PlayerPrefs.SetInt("UpgradeAmount", itemManager.upgradeItemAmount);
        }

        itemManager.ItemAmountSetting();
        itemManager.itemNameTextinitemEarnPanel.text = itemName;
        itemManager.itemEarnPanel.SetActive(true);

        MonoBehaviour.print(
            "HandleRewardBasedVideoRewarded event received for "
                        + 3 + " " + rewardType);
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        this.RequestRewardBasedVideo();
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        GetComponent<AudioSource>().Pause();
        GetComponent<ItemManager>().gameManager.PauseGame();
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void ShowRewardAd(string type)
    {
        rewardType = type;

        if (rewardBasedVideo.IsLoaded())
            rewardBasedVideo.Show();
    }

    IEnumerator CheckPurchasingNoAds()
    {
        while (!iAPMgr.IsInitalized())
        {
            MonoBehaviour.print(iAPMgr.IsInitalized());
            yield return null;          
        }

        if (iAPMgr.HasRecipt("no_ads")) PurchasingNoAds();
    }
}
