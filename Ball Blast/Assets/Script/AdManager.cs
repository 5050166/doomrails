//广告管理脚本
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.UI;
using DG.Tweening;

public class AdManager : MonoBehaviour
{
    private static AdManager instance;
    public static AdManager Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    public BannerView bannerView;
    public RewardBasedVideoAd rewardBasedVideo;
    public InterstitialAd interstitial;
    public Text AdTestText;

    public void Awake()
    {
        Instance = this;
    }

    void Start()
    {
#if UNITY_ANDROID
        string AppId = "ca-app-pub-1129560958513637~7247686129";
#endif

        //初始化
        MobileAds.Initialize(AppId);
        //横幅
        if (PlayerPrefs.GetInt("removead") == 0)
        {
            RequestBanner();
        }

        //视屏
        rewardBasedVideo = RewardBasedVideoAd.Instance;
        // 视屏广告载入成功时
        rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
        // 视频广告载入失败时
        rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        // 视屏广告打开
        rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
        // 视屏广告开始播放
        rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
        //视屏广告奖励
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        // 视屏广告关闭
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
        // 视频广告后台
        rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;

        //载入广告

        if (PlayerPrefs.GetInt("removead") == 0)
        {
            RequestInterstitial();

        }
        //图片
        RequestRewardBasedVideo();
    }


    //ca-app-pub-1129560958513637/9490706082

    private void RequestInterstitial()  //载入插屏广告
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-1129560958513637/9490706082";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-1129560958513637/9490706082";
#else
  
#endif

        interstitial = new InterstitialAd(adUnitId);

        // 插屏广告载入成功时执行
        interstitial.OnAdLoaded += HandleOnAdLoadedForScreen;
        // 插屏广告载入失败时
        interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoadForScreen;
        //打开广告时
        interstitial.OnAdOpening += HandleOnAdOpenedForScreen;
        // 关闭广告时
        interstitial.OnAdClosed += HandleOnAdClosedForScreen;

        interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplicationForScreen;

        AdRequest request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);
    }

    public void HandleOnAdLoadedForScreen(object sender, EventArgs args)
    {
        // 插屏广告载入成功时执行
    }

    public void HandleOnAdFailedToLoadForScreen(object sender, AdFailedToLoadEventArgs args)
    {
        // 插屏广告载入失败时
        RequestInterstitial();
    }

    public void HandleOnAdOpenedForScreen(object sender, EventArgs args)
    {
        //打开广告时
    }

    public void HandleOnAdClosedForScreen(object sender, EventArgs args)
    {
        // 关闭广告时
        interstitial.Destroy();  //销毁广告
        RequestInterstitial();
    }

    public void HandleOnAdLeavingApplicationForScreen(object sender, EventArgs args)
    {

    }



    private void RequestBanner()//载入横幅广告
    {

#if UNITY_ANDROID
        string AppId = "ca-app-pub-1129560958513637/3711217055"; //测试id
#endif

        bannerView = new BannerView(AppId, AdSize.Banner, AdPosition.Top);

        //广告点击事件注册

        //当广告载入成功（完成）时触发的事件
        bannerView.OnAdLoaded += HandleOnAdLoaded;

        //当广告载入失败时触发
        bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;

        //当广告被点击时触发
        bannerView.OnAdOpening += HandleOnAdOpened;

        //当广告关闭时触发
        bannerView.OnAdClosed += HandleOnAdClosed;

        //当游戏切到后台时触发
        bannerView.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        AdRequest adRequest = new AdRequest.Builder().Build();

        bannerView.LoadAd(adRequest);
    }
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        //  AdTestText.text = "广告载入成功";
    }
    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        //  AdTestText.text = "广告载入失败";
    }
    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        //  AdTestText.text = "点击广告时触发";
    }
    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        //  AdTestText.text = "广告关闭时触发";
    }
    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        //  AdTestText.text = "游戏切换到后台时触发";
    }


    private void RequestRewardBasedVideo()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-1129560958513637/9063330678";  //视屏广告
#endif
        //创建一个空的广告
        AdRequest request = new AdRequest.Builder().Build();
        //载入广告
        this.rewardBasedVideo.LoadAd(request, adUnitId);

    }


    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        //视屏广告已经载入成功

    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {


        //视屏广告载入失败后 失败信息 args.Message
        RequestRewardBasedVideo();
    }

    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        //视屏广告打开后执行   bgm暂停？
    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        //视屏广告开始后执行
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        //视屏广告关闭后执行
        this.RequestRewardBasedVideo();//


    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    { //通过一个type和amount的奖励实例来描述给用户的奖励
      //视频广告奖励逻辑位置。此处可添加视频奖励的对应逻辑。


        GameObject coin = Instantiate(BuffSystem.Instance.coin, GameMod.Instance.AdButton.transform.localPosition, Quaternion.identity);
        coin.transform.SetParent(GameMod.Instance.AdButton.transform.parent);
        coin.transform.localPosition = GameMod.Instance.AdButton.transform.localPosition;
        coin.transform.DOLocalMove(new Vector3(-490f, 1540f, 0f), 0.5f).SetEase(Ease.InQuad);
        coin.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        Destroy(coin, 1f);

        GameObject coin2 = Instantiate(BuffSystem.Instance.coin, GameMod.Instance.AdButton.transform.localPosition, Quaternion.identity);
        coin2.transform.SetParent(GameMod.Instance.AdButton.transform.parent);
        coin2.transform.localPosition = GameMod.Instance.AdButton.transform.localPosition;
        coin2.transform.DOLocalMove(new Vector3(-490f, 1540f, 0f), 0.5f).SetEase(Ease.InOutQuad);
        coin2.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        Destroy(coin2, 1f);

        GameObject coin3 = Instantiate(BuffSystem.Instance.coin, GameMod.Instance.AdButton.transform.localPosition, Quaternion.identity);
        coin3.transform.SetParent(GameMod.Instance.AdButton.transform.parent);
        coin3.transform.localPosition = GameMod.Instance.AdButton.transform.localPosition;
        coin3.transform.DOLocalMove(new Vector3(-490f, 1540f, 0f), 0.5f);
        coin3.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        Destroy(coin3, 1f);


        PlayerprefController.AddIntValue("coin", DataManager.Instance.getCurrentLevel() * 20 + 100);
        MainMenuUI.Instance.UpdateCurrentCoin();
        GameMod.Instance.AdButtonText.text = "+" + (DataManager.Instance.getCurrentLevel() * 20 + 100);

    }

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        //程序后台执行

    }

    public void GameOverForVideo()
    {
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show(); //展示视屏广告

        }
        AudioManager.Instance.source.PlayOneShot(AudioManager.Instance.ClickSound);
    }


    private void GameOverForScreen() //展示插屏广告
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
    }



}
