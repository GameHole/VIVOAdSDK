using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MiniGameSDK;
using VivoAdSdk;
public class Test : MonoBehaviour
{
    IRewardAdAPI rewardAd;
    IInterstitialAdAPI interstitialAdAPI;
    IBannerAd bannerAd;
    IAnalyzeEvent analyze;
    ISplashAd splash;
    IDialogPageAd pageAd;
    IFloatIconAd icon;
    public Button rwd;
    public Button inter;
    public Button banr;
    public Button banrc;
    public Button sp;
    public Button paged;
    public Button iconn;
    public Button iconh;
    public Button ana0;
    public Button ana1;
    private void Awake()
    {
        
    }
    void Start()
    {
        Debug.Log("banner ad ="+bannerAd);
        paged.onClick.AddListener(() =>
        {
            pageAd.Show();
        });
        sp.onClick.AddListener(()=>
        {
            splash.Show();
        });
        iconn.onClick.AddListener(() =>
        {
            icon.Show();
        });
        iconh.onClick.AddListener(() =>
        {
            icon.Hide();
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
