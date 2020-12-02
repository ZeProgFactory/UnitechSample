using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Device;
using Android.Device.Scanner.Configuration;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ZPF
{
   static class UnitechHelper
   {
      public static ScanManager mScanManager { get; private set; }

      public static void initScan()
      {
         if (Xamarin.Essentials.DeviceInfo.Manufacturer != "Unitech")
         {
            return;
         };

         mScanManager = new ScanManager();

         // Set the output mode of the barcode reader (either send output to text box or as Android intent).
         // Set to 0 if barcode output is to be sent as intent, barcode output is to be sent to the text box in focus
         mScanManager.SwitchOutputMode(0);

         mScanManager.TriggerMode = Android.Device.Scanner.Configuration.Triggering.Host;

         mScanManager.EnableAllSymbologies(true);

         // Turn off the power for the bar code reader. 
         mScanManager.CloseScanner();   // -> save power if not used
      }


      public static bool EnableAllSymbologies()
      {
         if (Xamarin.Essentials.DeviceInfo.Manufacturer != "Unitech")
         {
            return true;
         };

         mScanManager.EnableAllSymbologies(true);
         return true;
      }

      public static bool EnableEAN13Only()
      {
         if (Xamarin.Essentials.DeviceInfo.Manufacturer != "Unitech")
         {
            return true;
         };

         mScanManager.EnableAllSymbologies(false);
         mScanManager.EnableSymbology(Symbology.Ean13, true);

         return true;
      }

      public static bool EnableAllEANs()
      {
         if (Xamarin.Essentials.DeviceInfo.Manufacturer != "Unitech")
         {
            return true;
         };

         mScanManager.EnableAllSymbologies(false);
         mScanManager.EnableSymbology(Symbology.Ean13, true);
         mScanManager.EnableSymbology(Symbology.Ean8, true);
         mScanManager.EnableSymbology(Symbology.Code128, true);

         return true;
      }

      /// <summary>
      /// Turn on the power for the bar code reader. 
      /// </summary>
      /// <returns></returns>
      public static bool OpenScanner()
      {
         if (Xamarin.Essentials.DeviceInfo.Manufacturer != "Unitech")
         {
            return true;
         };

         // Turn on the power for the bar code reader. 
         return mScanManager.OpenScanner();
      }

      /// <summary>
      /// Turn off the power for the bar code reader. 
      /// </summary>
      /// <returns></returns>
      public static bool CloseScanner()
      {
         if (Xamarin.Essentials.DeviceInfo.Manufacturer != "Unitech")
         {
            return true;
         };

         // Turn off the power for the bar code reader. 
         return mScanManager.CloseScanner();
      }
   }
}
