package com.ad.nativeview;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;

public class EmptyActivity extends Activity
{
    public interface IOnActivityCreated
    {
       void onCreate(Activity activity);
    }
    static IOnActivityCreated call;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        if(call!=null)
        {
            call.onCreate(this);
        }
    }
    public static void New(Activity owener, IOnActivityCreated onCreate){
        call=onCreate;
        owener.startActivity(new Intent(owener,EmptyActivity.class));
    }
}
