package com.api.unityactivityinterface;
import com.ad.vivo.VivoAdInitor;
import android.app.Application;
import java.util.ArrayList;
public class CustomUnityApplication extends Application{
     ArrayList<IOnAppCreate> _ionappcreate = new ArrayList<IOnAppCreate>();
     ArrayList<IOnAppTerminate> _ionappterminate = new ArrayList<IOnAppTerminate>();
     public CustomUnityApplication(){
          VivoAdInitor _com_ad_vivo_vivoadinitor = new VivoAdInitor();
          _ionappcreate.add(_com_ad_vivo_vivoadinitor);
     }
     @Override
     public void onCreate(){
          super.onCreate();
          for (IOnAppCreate item: _ionappcreate){
               item.onCreate(this);
          }
     }
     @Override
     public void onTerminate(){
          super.onTerminate();
          for (IOnAppTerminate item: _ionappterminate){
               item.onTerminate(this);
          }
     }
}
