using Android.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidBatteryService
{
    public class BatteryBroadCastReciever : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            //Here is where you do "Something" when the battery level changes.
            //As an example I assigned the battery level to an Integer. You can put code in here to do anything
            //(i.e - report battery level to your Web App.) If you would like help with this, let me know...
            int level = intent.GetIntExtra("level", 0);
        }
    }
}
