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

Change Log - 1st November 2016
-------------
Please note: this section will not make sense to you unless you have followed the 'Getting Setup' section first. Once you are setup and running the aplication on a device / emulator, then you will be presented with a simple UI to start and stop service.
Since then there have been 2 further requirements.
1. To automatically start the service when the device / emulator boots up.
2. To restart the service if the system shuts it down (E.g in the case that the device is running low on memory)

##### Point 1 - Starting the service on bootup
I could talk about this at length. In a nutshell, be wary of what Android version you are running. After version 3.1 the flag **FLAG_EXCLUDE_STOPPED_PACKAGES** is added to ALL broadcast intents. If this flag is set, then the service will not start on bootup. What you need to do is run the appllication, start the service via the UI, then stop it again. You need to do this once and once only.
For further explaination and to see this in action, [watch the latest video](https://www.youtube.com/watch?v=TdHfNIfYFHc&feature=youtu.be)

The application that starts the service needs the **correct permissions** To see where these are set, right click the 'BatteryService.Droid' project and left click properties. Look in the 'Android Manifest' tab. Scroll down to 'Required Permissions'. Notice how WAKE_LOCK and RECEIVE_BOOT_COMPLETED are ticked.

![enter image description here](https://raw.githubusercontent.com/Ted95153420/XamarinBatteryService/master/Screenshots/ProjectSettings.jpg)


It is StartupBroadCastReciever.cs that does the business of starting the service once the device is restarted.
```
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
```
Notice the use of PowerManager.WakeLock. You dont HAVE to use this. What this does is allow you to acquire a Wakelock (wakeLock.cquire()) - once this has been acquired then the device cannot sleep. Origionally I thought this was the reason why the service wasnt being started on bootup. This is not the case, but the code wont hurt. NOTE : a wakelock hogs battery. Be sure to call wakeLock.Release() once you are done starting the service.

##### Point 2 - Making sure the service restarts if the system shuts it down
Take a look where the service actually starts (AndroidBatteryService.cs). Here it is the return value thats important. STICKY means that if the system does shut the service down, then it will be restarted again as soon as possible.

```
public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            Toast.MakeText(this, "The Battery Service was just started", ToastLength.Long).Show();
            //you are interested in recieving the battery status of the device.
            //To do this, instantiate BatteryBroadCastReciever
            _broadCastReciever = new BatteryBroadCastReciever();
            RegisterReceiver(_broadCastReciever, new IntentFilter(Intent.ActionBatteryChanged));
            base.OnStartCommand(intent, flags, startId);
            return StartCommandResult.Sticky;
        }
```




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