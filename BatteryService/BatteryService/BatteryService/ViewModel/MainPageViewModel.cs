using Android.Content;
using AndroidBatteryService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatteryService.ViewModel
{
    public class MainPageViewModel
    {
        public void StartBatteryService()
        {
            Intent intent = new Intent(Android.App.Application.Context, typeof(AndroidBatteryService.AndroidBatteryService));
            intent.SetFlags(ActivityFlags.NewTask);
            Android.App.Application.Context.StartService(intent);
        }

        public void StopBatteryService()
        {
            Intent intent = new Intent(Android.App.Application.Context, typeof(AndroidBatteryService.AndroidBatteryService));
            Android.App.Application.Context.StopService(intent);
        }
    }
}
