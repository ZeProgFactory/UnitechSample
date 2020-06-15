
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Device;
using Android.OS;
using Android.Runtime;
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

         // - - -  - - - 

         if (Xamarin.Forms.Device.Idiom == Xamarin.Forms.TargetIdiom.Phone || Xamarin.Forms.Device.Idiom == Xamarin.Forms.TargetIdiom.Unsupported)
         {
            // layout views vertically
            this.RequestedOrientation = ScreenOrientation.Portrait;
         }
         else
         {
            // layout views horizontally
            this.RequestedOrientation = ScreenOrientation.Sensor;
         };

         // - - -  - - - 

         Xamarin.Essentials.Platform.Init(this, savedInstanceState);
         global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

         // - - -  - - - 

         if (Xamarin.Essentials.DeviceInfo.Manufacturer == "Unitech")
         {
            UnitechHelper.initScan();
            receiver = new ScanResultReceiver();
         };

         // - - -  - - - 

         LoadApplication(new App());
      }

      protected override void OnResume()
      {
         base.OnResume();

         if (Xamarin.Essentials.DeviceInfo.Manufacturer == "Unitech")
         {
            RegisterReceiver(receiver, new IntentFilter(ScanManager.ActionDecode));
         };
      }

      protected override void OnPause()
      {
         if (Xamarin.Essentials.DeviceInfo.Manufacturer == "Unitech")
         {
            UnregisterReceiver(receiver);
         };

         base.OnPause();
      }

      public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
      {
         Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

         base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
      }

      // https://docs.microsoft.com/en-us/xamarin/android/app-fundamentals/broadcast-receivers

      [BroadcastReceiver(Name = "com.ute.eu.ScanResultReceiver", Enabled = true)]
      [IntentFilter(new[] { ScanManager.ActionDecode })]
      public class ScanResultReceiver : BroadcastReceiver
      {
         public override void OnReceive(Context context, Intent intent)
         {
            byte[] rawData = intent.GetByteArrayExtra(ScanManager.DecodeDataTag);
            int Length = intent.GetIntExtra(ScanManager.BarcodeLengthTag, 0);
            byte Symbology = (byte)intent.GetByteExtra(ScanManager.BarcodeTypeTag, (sbyte)0);
            string Data = System.Text.Encoding.UTF8.GetString(rawData);

            UnitechViewModel.Current.NewBarcode(Data, Length, Symbology, rawData);
         }
      };
   }
}
