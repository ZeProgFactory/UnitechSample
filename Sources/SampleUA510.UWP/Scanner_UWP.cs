using ZPF.XF;

[assembly: Xamarin.Forms.Dependency(typeof(Scanner))]
namespace ZPF.XF
{
   /// <summary>
   /// </summary>
   public class Scanner : IScanner
   {
      public bool CloseScanner()
      {
         return true;
      }

      public bool EnableAllSymbologies()
      {
         return true;
      }

      public bool EnableEAN13Only()
      {
         return true;
      }

      public bool OpenScanner()
      {
         return true;
      }
   }
}
