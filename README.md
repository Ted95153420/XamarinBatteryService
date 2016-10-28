Android Battery Service
===================

Introduction
-----------------

Assuming you have a github account clone the repository.
```
git clone https://github.com/Ted95153420/XamarinBatteryService
```

If you do not have a github account, then download the source code [here](https://github.com/Ted95153420/XamarinBatteryService/archice/master.zip)

---------

Getting Setup
-------------

Open the BatteryService.sln file in Visual Studio 2015. Once you have done this, right click the solution in the solution explorer.

![enter image description here](https://raw.githubusercontent.com/Ted95153420/XamarinBatteryService/master/Screenshots/BatteryServiceSolution.JPG "Right Click solution")

Now left click on 'Restore Nuget Packages'

![enter image description here](https://raw.githubusercontent.com/Ted95153420/XamarinBatteryService/master/Screenshots/RestoreNuget.jpg)

Now restart Visual Studio. Once you have restarted, you should be able to deploy the application to an android emulator. I have been deploying to an XXHPDI android phone, but you can deploy to whatever you like. To deploy I click the following in VS 2015 :-

![enter image description here](https://raw.githubusercontent.com/Ted95153420/XamarinBatteryService/master/Screenshots/DeployToEmultor.JPG)

Now wait. Eventually the emulator will start up, the android OS will start and the app and service will be deployed to the android device. To understand how to use the app (its very easy, you just need to start the service and place a breakpoint in the 'OnRecieve' method of the BatteryBroadCastReciever class), then I suggest you [watch the video](https://www.youtube.com/watch?v=vBaMcZT196g&feature=youtu.be) 

Solution Structure
-------------

The actual service is contained within the  'AndroidBatteryService' project.

![enter image description here](https://raw.githubusercontent.com/Ted95153420/XamarinBatteryService/master/Screenshots/ServiceProject.JPG) 

BatterService is an app that is used to start and stop AndroidBatteryService. The code in AndroidBatteryService.cs is something like what is shown below. The OnStartCommand method is where the bulk of the work is done. Notice how we create a new BatteryBroadCastReciever, then register tis reciever with a call to RegisterReciever. Note how one of the parameters to RegisterReciever is an IntentFilter. Basically what we are saying here is this "Please register the service, but only listen out for broadcasts describing a change in battery level"

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