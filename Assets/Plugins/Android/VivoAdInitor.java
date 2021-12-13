package com.ad.vivo;

import android.app.Application;



import com.api.unityactivityinterface.IOnAppCreate;
import com.vivo.mobilead.manager.VInitCallback;
import com.vivo.mobilead.manager.VivoAdManager;
import com.vivo.mobilead.unified.base.VivoAdError;
import com.vivo.mobilead.util.VOpenLog;

public class VivoAdInitor implements IOnAppCreate {
    String mediaID="03ad601d82b64f8e9b4939259b47c26e";
    boolean isDebug=true;
    @Override
    public void onCreate(Application activity) {
        VOpenLog.setEnableLog(isDebug);
        VivoAdManager.getInstance().init(activity, mediaID,
                new VInitCallback() {
                    @Override
                    public void suceess() {
                        VOpenLog.d("Unity", "vivo sdk init success");
                    }
                    @Override
                    public void failed(VivoAdError adError) {
                        VOpenLog.e("Unity", "vivo sdk init failed: " + adError.toString());
                    }
                });
    }
}
