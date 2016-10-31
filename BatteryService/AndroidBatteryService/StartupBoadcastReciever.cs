using Android.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidBatteryService
{
    //The sole purpose of this class is to start the Android Battery Service
    //when the phone starts.
    public class StartupBoadcastReciever : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            Intent serviceStrtIntent = new Intent(Android.App.Application.Context, typeof(AndroidBatteryService));
            serviceStrtIntent.SetFlags(ActivityFlags.NewTask);
            Android.App.Application.Context.StartService(serviceStrtIntent);
        }
    }
}
