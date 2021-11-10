using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;
using ZPF;
using ZPF.XF;

namespace SampleUA510
{
   // Learn more about making custom code visible in the Xamarin.Forms previewer
   // by visiting https://aka.ms/xamarinforms-previewer
   [DesignTimeVisible(false)]
   public partial class MainPage : ContentPage
   {

      public MainPage()
      {
         BindingContext = UnitechViewModel.Current;

         InitializeComponent();

         lManufacturer.Text = $"'{Xamarin.Essentials.DeviceInfo.Manufacturer}'";

         DependencyService.Get<IScanner>().OpenScanner();
         // DependencyService.Get<IScanner>().EnableAllEANs();
      }

      protected override void OnDisappearing()
      {
         DependencyService.Get<IScanner>().CloseScanner();

         base.OnDisappearing();
      }

      private void itemAppearing(object sender, ItemVisibilityEventArgs e)
      {
         var listView = (sender as ListView);

         var lastItem = UnitechViewModel.Current.LastScans.LastOrDefault();
         listView.ScrollTo(lastItem, ScrollToPosition.MakeVisible, true);
      }
   }
}
