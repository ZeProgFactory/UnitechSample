using Android.App;
using Android.Content;
using Android.Net;
using System;
using System.Diagnostics;
using ZPF.XF;

[assembly: Xamarin.Forms.Dependency(typeof(Scanner))]
namespace ZPF.XF
{
   /// <summary>
   /// </summary>
   public class Scanner : IScanner
   {
      /// <summary>
      /// Turn on the power for the bar code reader. 
      /// </summary>
      /// <returns></returns>
      public  bool OpenScanner()
      {
         // Turn on the power for the bar code reader. 
         return UnitechHelper.OpenScanner();
      }

      /// <summary>
      /// Turn off the power for the bar code reader. 
      /// </summary>
      /// <returns></returns>
      public  bool CloseScanner()
      {
         // Turn on the power for the bar code reader. 
         return UnitechHelper.CloseScanner();
      }

      public bool EnableAllSymbologies()
      {
         return UnitechHelper.EnableAllSymbologies();
      }

      public bool EnableEAN13Only()
      {
         return UnitechHelper.EnableEAN13Only();
      }
      public bool EnableAllEANs()
      {
         return UnitechHelper.EnableAllEANs();
      }
   }
}
