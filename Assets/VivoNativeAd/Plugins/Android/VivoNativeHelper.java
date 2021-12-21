package com.ad.vivo;

import android.app.Activity;
import android.text.TextUtils;
import android.util.Log;
import android.view.View;
import android.view.ViewGroup;
import android.view.ViewParent;
import android.widget.FrameLayout;

import com.ad.nativeview.NativeHelper;
import com.ad.nativeview.NativeImageView;
import com.ad.nativeview.NativeInfo;
import com.ad.nativeview.NetTexture;
import com.vivo.ad.nativead.NativeResponse;
import com.vivo.mobilead.unified.base.view.VivoNativeAdContainer;

public class VivoNativeHelper
{
    static Activity activity;
    public  static  void Init(Activity act){
        activity=act;
        NativeHelper.Init(act);
    }
    public  static  View Showbanner(NativeResponse response, View.OnClickListener onClose){
        try {
            NativeInfo info=createInfo(response);
            info.showedImg = new NetTexture(activity,response.getIconUrl(),null);
            NativeImageView v =  NativeHelper.ShowView(0,info,onClose);
            return regist(response,v);
        }catch (Exception e){
            e.printStackTrace();
            return null;
        }
    }
    public  static  void  SetBannerBtnScale(float scale)
    {
        View btn = NativeHelper.GetView(0).basic.closeBtn.button;
        btn.setScaleX(scale);
        btn.setScaleY(scale);
    }
    public  static  void  SetInterClickRangeScale(float scale)
    {
        View t = NativeHelper.GetView(1).touchRange;
        t.setScaleX(scale);
        t.setScaleY(scale);
    }
    public  static  View ShowInter(NativeResponse response,View.OnClickListener onClose) {
        try {
            NativeInfo info = createInfo(response);
            info.showedImg = new NetTexture(activity, response.getImgUrl().get(0), null);
            NativeImageView v = NativeHelper.ShowView(1, info, onClose);
            return regist(response, v);
        } catch (Exception e) {
            e.printStackTrace();
            return null;
        }
    }
    static boolean isRegisted( ViewParent iparent){
        return iparent.getClass()==VivoNativeAdContainer.class;
    }
    static  View regist(NativeResponse response, NativeImageView view){

        ViewParent iparent = view.touchRange.getParent();
        if(!isRegisted(iparent)){
            ViewGroup parnet =(ViewGroup)iparent ;
            int idx=parnet.indexOfChild(view.touchRange);
            parnet.removeView(view.touchRange);
            VivoNativeAdContainer nativeAdContainer = new VivoNativeAdContainer(activity);
            nativeAdContainer.setLayoutParams(view.touchRange.getLayoutParams());
            nativeAdContainer.addView(view.touchRange);
            parnet.addView(nativeAdContainer,idx);
            iparent=nativeAdContainer;
        }
        VivoNativeAdContainer touch= (VivoNativeAdContainer)iparent;
        touch.setScaleX(view.touchRange.getScaleX());
        touch.setScaleY(view.touchRange.getScaleY());
        response.registerView(touch,null,null);
        return view.root;
    }
    static NativeInfo createInfo( NativeResponse response){
        NativeInfo info=new NativeInfo();
        info.description=response.getDesc();
        info.title=response.getTitle();
        if (response.getAdLogo() != null||!TextUtils.isEmpty(response.getAdMarkUrl())) {
            info.logoImg = new NetTexture(activity, response.getAdMarkUrl(), response.getAdLogo());
        }else {
            info.logoTxt=GetLogoTxt(response);
        }
        return info;
    }
    static  String GetLogoTxt(NativeResponse response) {
        if (!TextUtils.isEmpty(response.getAdMarkText()))
            return response.getAdMarkText();
        if (!TextUtils.isEmpty(response.getAdTag()))
            return response.getAdTag();
        return "广告";
    }
}
