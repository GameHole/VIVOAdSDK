package com.ad.vivo;

import android.app.Activity;
import android.app.Application;
import android.content.res.Configuration;
import android.os.Bundle;
import android.util.Log;

import com.ad.nativeview.EmptyActivity;
import com.api.unityactivityinterface.IOnCreate;
import com.vivo.ad.model.AdError;
import com.vivo.ad.splash.SplashAdListener;
import com.vivo.mobilead.splash.SplashAdParams;
import com.vivo.mobilead.splash.VivoSplashAd;

public class VivoSplashShower implements IOnCreate {
    String id ="ce2e5586b4b3476ca68bf38cb8d54940";
    boolean active=true;
    public void onCreate(Activity activity, Bundle savedInstanceState) {
        if(!active)return;
        EmptyActivity.New(activity, new EmptyActivity.IOnActivityCreated() {
            @Override
            public void onCreate(Activity activity) {
                currentActivity=activity;
                ShowSplash(activity);
            }
        });
    }
    Activity currentActivity;
    void finish(){
        if(currentActivity!=null)
            currentActivity.finish();
    }
    void ShowSplash(Activity activity) {
        SplashAdParams.Builder builder = new SplashAdParams.Builder(id);
        builder.setAppTitle("广告联盟");
        builder.setFetchTimeout(3000);
        builder.setSplashOrientation(activity.getResources().getConfiguration().orientation);
        VivoSplashAd ad = new VivoSplashAd(activity,listener , builder.build());
        ad.loadAd();
    }


    SplashAdListener listener = new SplashAdListener() {
        @Override
        public void onADDismissed() {
            Log.v("unity","splash onADDismissed");
            finish();
        }

        @Override
        public void onNoAD(AdError adError) {
            Log.v("unity","splash onNoAD "+adError.toString());
            finish();
        }

        @Override
        public void onADPresent() {
            Log.v("unity","splash onADPresent");
        }

        @Override
        public void onADClicked() {
            Log.v("unity","splash onADClicked");
        }
    };


}
