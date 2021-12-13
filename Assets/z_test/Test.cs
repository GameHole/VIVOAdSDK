using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MiniGameSDK;
public class Test : MonoBehaviour
{
    IRewardAdAPI rewardAd;
    IInterstitialAdAPI interstitialAdAPI;
    IBannerAd bannerAd;
    IAnalyzeEvent analyze;
    ISplashAd splash;
    IDialogPageAd pageAd;
    public Button rwd;
    public Button inter;
    public Button banr;
    public Button banrc;
    public Button sp;
    public Button paged;
    public Button ana0;
    public Button ana1;
    private void Awake()
    {
        
    }
    void Start()
    {
        paged.onClick.AddListener(() =>
        {
            pageAd.Show();
        });
        sp.onClick.AddListener(()=>
        {
            splash.Show();
        });
        rwd.onClick.AddListener(() =>
        {
            rewardAd.AutoShow((v) =>
            {
                Debug.Log(v);
            });
        });
        inter.onClick.AddListener(() =>
        {
            interstitialAdAPI.Show();
        });
        banr.onClick.AddListener(() =>
        {
            bannerAd.Show();
        });
        banrc.onClick.AddListener(() =>
        {
            bannerAd.Hide();
        });
        //ana0.onClick.AddListener(() =>
        //{
        //    analyze.SetEvent("app_start");
        //});
        //ana1.onClick.AddListener(() =>
        //{
        //    analyze.SetEvent("level_end", new KVPair("20","0"));
        //});
    }
}
