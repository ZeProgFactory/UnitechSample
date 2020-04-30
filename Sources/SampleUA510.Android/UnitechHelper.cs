using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Device;
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
         mScanManager = new ScanManager();

         // Turn on the power for the bar code reader. 
         mScanManager.OpenScanner();

         // Set the output mode of the barcode reader (either send output to text box or as Android intent).
         // Set to 0 if barcode output is to be sent as intent, barcode output is to be sent to the text box in focus
         mScanManager.SwitchOutputMode(0);

         mScanManager.TriggerMode = Android.Device.Scanner.Configuration.Triggering.Host;

         mScanManager.EnableAllSymbologies(true);

         // Call this method to start decoding. 
         // mScanManager.StartDecode();
      }

      public static bool CloseScanner()
      {
         // Turn off the power for the bar code reader. 
         return mScanManager.CloseScanner();
      }
   }
}
