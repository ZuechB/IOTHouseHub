# IOTHouseHub
.net core 3 web api; .net core 3 raspberry pi with gpio commands; xamarin mobile app



## Install your API
Coming soon!


## Install your Android mobile app
Coming soon!


## Install on Raspberry Pi
On your raspberry pi device open chrome and go to the following url:

### Install .net core 3
1.) Go to https://dotnet.microsoft.com/download/dotnet-core/3.0
2.) Download ".NET Core Binaries" (ARM32) - Runtime 3.0.0 only
3.) sudo mkdir -p /opt/dotnet
4.) sudo tar zxf dotnet-runtime-3.0.0-linux-arm.tar.gz -C /opt/dotnet
5.) sudo ln -s /opt/dotnet/dotnet /usr/local/bin

last confirm that dotnet is installed by typing "dotnet --help" If you receive a response you are great to go!

### Install IOTDeviceListener

After you compile the IOT Device Listener to install it on your Raspberry Pi, you must first give it permission.
Go to command line and run the following:
1.) chmod u+x IOTDeviceListener
2.) now you can run the app by just typing: ./IOTDeviceListener
