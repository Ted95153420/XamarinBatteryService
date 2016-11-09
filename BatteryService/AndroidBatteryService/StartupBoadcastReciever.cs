using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidBatteryService
{
    //The sole purpose of this class is to start the Android Battery Service
    //when the device starts.
    //[BroadcastReceiver(Enabled = true, Exported = true, Permission = "RECEIVE_BOOT_COMPLETED")]
    //[IntentFilter(new string[] { "android.intent.action.BOOT_COMPLETED" })]
    [BroadcastReceiver(Enabled = true, Exported = true, Permission = "RECEIVE_BOOT_COMPLETED")]
    [IntentFilter(new String[] { Intent.ActionBootCompleted}, Priority = (int)IntentFilterPriority.HighPriority)]
    public class StartupBoadcastReciever : BroadcastReceiver
    {
        private const string BOOTUP_TAG = "BOOT";
        public override void OnReceive(Context context, Intent intent)
        {
            try
            {
                Log.Info(BOOTUP_TAG, "POINT ONE Made it to the broadcast reciever");
                PowerManager pm = (PowerManager)context.GetSystemService(Context.PowerService);
                Log.Info(BOOTUP_TAG, "POINT TWO");
                PowerManager.WakeLock wakeLock = pm.NewWakeLock(WakeLockFlags.ScreenDim, "service start tag");
                Log.Info(BOOTUP_TAG, "POINT THREE");
                wakeLock.Acquire();
                Log.Info(BOOTUP_TAG, "POINT FOUR");
                Intent serviceStrtIntent = new Intent(Android.App.Application.Context, typeof(AndroidBatteryService));
                Log.Info(BOOTUP_TAG, "POINT FIVE");
                serviceStrtIntent.SetFlags(ActivityFlags.NewTask);
                Log.Info(BOOTUP_TAG, "POINT SIX");
                Android.App.Application.Context.StartService(serviceStrtIntent);
                Log.Info(BOOTUP_TAG, "POINT SEVEN");
                wakeLock.Release();
                Log.Info(BOOTUP_TAG, "POINT EIGHT");
            }
            catch(Exception bootException)
            {
                Log.Error(BOOTUP_TAG, "ERROR IN BOOTUP : " + bootException.Message);
            }
        }
    }
}
