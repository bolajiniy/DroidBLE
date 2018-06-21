using Android.App;
using Android.Widget;
using Android.OS;
using Android.Bluetooth;
using Android.Content;
using Android.Runtime;
using Plugin.Permissions;
using System.Collections.Generic;
using Android.Bluetooth.LE;

namespace DroidBLE
{
    [Activity(Label = "DroidBLE", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        protected BluetoothAdapter _adapter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);
            button.Text = "Start Scanning";

            StartActivity(new Intent(BluetoothAdapter.ActionRequestEnable));

            _adapter = ((BluetoothManager)GetSystemService("bluetooth")).Adapter;

            var newCallback = new BLEScanCallback();
            var oldCallBack = new BLELeScanCallBack();

            var intent = new Intent(this, typeof(BleReceiver));

            var pendingIntent =  PendingIntent.GetBroadcast(this, 111, intent, PendingIntentFlags.UpdateCurrent);


            var _isScanning = false;
            button.Click += delegate
            {
                if (_isScanning)
                {
                    _isScanning = false;
                    button.Text = "Start Scanning";
                    if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
                    {
                        _adapter.BluetoothLeScanner.StopScan(pendingIntent);
                    }
                     else if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                    {
                        _adapter.BluetoothLeScanner.StopScan(newCallback);
                    }
                    else
                    {

#pragma warning disable CS0618 // Type or member is obsolete
                        _adapter.StopLeScan(oldCallBack);
#pragma warning restore CS0618 // Type or member is obsolete
                    }
                }
                else
                {

                    if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
                    {
                        var filters = new List<ScanFilter>();

                        //todo : set filter uuid
                        //var filter = new ScanFilter.Builder()
                        //                            .SetServiceUuid(ParcelUuid.FromString(""))
                        //                            .Build();
                        //filters.Add(filter);

                        var settings = new ScanSettings.Builder()
                                                       .SetScanMode(Android.Bluetooth.LE.ScanMode.Balanced)
                                                       .Build();



                        _adapter.BluetoothLeScanner.StartScan(filters, settings, pendingIntent);


                    }
                    else
                    if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                    {

                        _adapter.BluetoothLeScanner.FlushPendingScanResults(newCallback);

                        _adapter.BluetoothLeScanner.StartScan(newCallback);

                    }
                    else
                    {

#pragma warning disable CS0618 // Type or member is obsolete
                        _adapter.StartLeScan(oldCallBack);
#pragma warning restore CS0618 // Type or member is obsolete
                    }

                    _isScanning = true;
                    button.Text = "Stop Scanning";
                }

            };
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);

            System.Diagnostics.Debugger.Log(11, "BLE-INTENT", intent.Action);
            
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            System.Diagnostics.Debugger.Log(11, "BLE-OnActivityResult", data.Action);
        }

    }
}

