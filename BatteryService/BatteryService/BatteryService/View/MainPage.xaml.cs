using BatteryService.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BatteryService.View
{
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel _mainPageVM;
        public MainPage()
        {
            InitializeComponent();
            _mainPageVM = new MainPageViewModel();
            BindingContext = _mainPageVM;
        }


        public void OnStart(Object sender, EventArgs eArgs)
        {
            _mainPageVM.StartBatteryService();
        }

        public void OnStop(Object sender, EventArgs eArgs)
        {
            _mainPageVM.StopBatteryService();
        }
    }
}
