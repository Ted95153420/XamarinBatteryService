using Android.App;
using Android.Content;
using Android.OS;
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
        public override void OnReceive(Context context, Intent intent)
        {
            PowerManager pm  = (PowerManager)context.GetSystemService(Context.PowerService);
            PowerManager.WakeLock wakeLock = pm.NewWakeLock(WakeLockFlags.ScreenDim , "service start tag");
            wakeLock.Acquire();
            Intent serviceStrtIntent = new Intent(Android.App.Application.Context, typeof(AndroidBatteryService));
            serviceStrtIntent.SetFlags(ActivityFlags.NewTask);
            Android.App.Application.Context.StartService(serviceStrtIntent);
            wakeLock.Release();
        }
    }
}
