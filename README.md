# Xamarin Android Bluetooth BLE
Simple Xamarin Android BLE scanning

Scanning for BLE devices is a very common task/request these days, expecialy with the advent and proliferation of IoT devices.

Also, google has made some profiles and security changes. Some permissions are required for your application to have Bluetooth BLE priviledge.

Below are the required permissions:<br/>
              android.permission.BLUETOOTH<br/> 
              android.permission.BLUETOOTH_ADMIN<br/>
              android.permission.ACCESS_COARSE_LOCATION<br/>
              android.permission.ACCESS_FINE_LOCATION<br/>


The last two permissions are the new additions, for more info ; https://developer.android.com/reference/android/bluetooth/le/BluetoothLeScanner#startScan(java.util.List%3Candroid.bluetooth.le.ScanFilter%3E,%20android.bluetooth.le.ScanSettings,%20android.app.PendingIntent)


# Location and Bluetooth Services 

Location and Bluetooth servics are required;


            bleAdapter = ((BluetoothManager)GetSystemService(BluetoothService)).Adapter;
            locManager = (LocationManager)GetSystemService(LocationService);
            

# Permissions at runtime

Despite adding above permissions in the AndroidManifest.xml file, applications are required to check again at the rumtime if those previledges are still available to them.
           
           ActivityCompat.RequestPermissions(this, requiredPermissions, 10101);
           
           
           private readonly string[] requiredPermissions = new string[] {
                                        Manifest.Permission.AccessFineLocation,
                                        Manifest.Permission.Bluetooth,
                                        Manifest.Permission.BluetoothAdmin,
                                        Manifest.Permission.AccessCoarseLocation,
                                    };
                                    
