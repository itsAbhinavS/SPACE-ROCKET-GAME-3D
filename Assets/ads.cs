using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;



public class ads : MonoBehaviour
{

    private void Start()
    {
        RequestBanner();
        RequestInterstitial();
        RequestRewardedAd();
    }

    BannerView bannerView;

    string bannerID = "ca-app-pub-3940256099942544/6300978111";

    InterstitialAd interstitialAd;

    string interstitialID = "ca-app-pub-3940256099942544/1033173712";


    RewardedAd rewardedAd;

    string rewardedAdID = "ca-app-pub-3940256099942544/5224354917";



    void RequestBanner()
    {
        bannerView = new BannerView(bannerID, AdSize.Banner, AdPosition.Top);
        AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);

    }

    void RequestInterstitial()
    {
        interstitialAd = new InterstitialAd(interstitialID);
        AdRequest request = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(request);
    }

    public void ShowInterstitialAds()
    {
        interstitialAd.Show();
        RequestInterstitial();

    }

    void RequestRewardedAd()
    {
        rewardedAd = new RewardedAd(rewardedAdID);
        AdRequest request = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(request);
    }

    public void ShowRewardedAd()
    {
        rewardedAd.Show();
        RequestRewardedAd();

    }
}


