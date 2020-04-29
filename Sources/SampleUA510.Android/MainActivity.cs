using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Android.Device.Scanner;
using Android.Content;
using Android.Device;
using ZPF;

namespace SampleUA510.Droid
{
   [Activity(Label = "SampleUA510", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
   public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
   {
      private ScanResultReceiver receiver;

      protected override void OnCreate(Bundle savedInstanceState)
      {
         TabLayoutResource = Resource.Layout.Tabbar;
         ToolbarResource = Resource.Layout.Toolbar;

         base.OnCreate(savedInstanceState);

         Xamarin.Essentials.Platform.Init(this, savedInstanceState);
         global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

         UnitechHelper.initScan();
         receiver = new ScanResultReceiver();

         LoadApplication(new App());
      }

      protected override void OnResume()
      {
         base.OnResume();

         RegisterReceiver(receiver, new IntentFilter(ScanManager.ActionDecode));
      }

      protected override void OnPause()
      {
         UnregisterReceiver(receiver);

         base.OnPause();
      }

      public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
      {
         Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

         base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
      }

      protected override void OnStart()
      {
         base.OnStart();
      }

      protected override void OnDestroy()
      {
         //if (AnalyticsHelper.DeviceInfo.DM.Contains("CASIO"))
         {
            // Turn off the power for the bar code reader. 
            // System.Diagnostics.Debug.WriteLine($"CloseScanner {mScanManager.CloseScanner()}");
         };

         base.OnDestroy();
      }


      // https://docs.microsoft.com/en-us/xamarin/android/app-fundamentals/broadcast-receivers

      [BroadcastReceiver(Name = "com.ute.eu.ScanResultReceiver", Enabled = true)]
      // [BroadcastReceiver(Enabled = true)]
      [IntentFilter(new[] { ScanManager.ActionDecode })]
      public class ScanResultReceiver : BroadcastReceiver
      {
         public override void OnReceive(Context context, Intent intent)
         {
            //MainActivity inst = Instance();

            //if (inst != null)
            //   inst.SetViewText("");

            byte[] rawData = intent.GetByteArrayExtra(ScanManager.DecodeDataTag);
            int Length = intent.GetIntExtra(ScanManager.BarcodeLengthTag, 0);
            byte Symbology = (byte)intent.GetByteExtra(ScanManager.BarcodeTypeTag, (sbyte)0);
            string Data = System.Text.Encoding.UTF8.GetString(rawData);

            UnitechViewModel.Current.NewBarcode(Data, Length, Symbology, rawData);

            //if (inst != null)
            //   inst.SetViewText(result);
         }
      };
   }
}
