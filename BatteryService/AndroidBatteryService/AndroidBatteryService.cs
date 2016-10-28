using Android.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;

namespace AndroidBatteryService
{
    [Service]
    public class AndroidBatteryService : Service
    {

        private BatteryBroadCastReciever _broadCastReciever;

        public AndroidBatteryService()
        {
           
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            Toast.MakeText(this, "The Battery Service was just started", ToastLength.Long).Show();
            //you are interested in recieving the battery status of the device.
            //To do this, instantiate BatteryBroadCastReciever
            _broadCastReciever = new BatteryBroadCastReciever();
            RegisterReceiver(_broadCastReciever, new IntentFilter(Intent.ActionBatteryChanged));
            return base.OnStartCommand(intent, flags, startId);
        }

        public override void OnCreate()
        {
            base.OnCreate();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            Toast.MakeText(this, "The Battery Service has been stopped", ToastLength.Long).Show();
        }

        public override IBinder OnBind(Intent intent)
        {
            //started service, NOT a binded service - return null...
            return null;
        }
    }
}
