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
            int level = intent.GetIntExtra("level", 0);
        }
    }
}
