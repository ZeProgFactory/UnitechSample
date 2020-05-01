using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using ZPF;

public class UnitechViewModel : BaseViewModel
{
   public enum Symbologies
   {
      Unknown,
      None,
      EAN13,
      EAN8,
      Code39,
      Code128,
      GS1_128,
      DATAMATRIX,
      QRCODE,
   }

   // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -

   static UnitechViewModel _Current = null;

   public static UnitechViewModel Current
   {
      get
      {
         if (_Current == null)
         {
            _Current = new UnitechViewModel();
         };

         return _Current;
      }

      set
      {
         _Current = value;
      }
   }

   // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -

   public UnitechViewModel()
   {
      _Current = this;

      // - - -  - - - 

      EA510Symbology = new Dictionary<int, Symbologies>();
      EA510Symbology.Add(-1, Symbologies.Unknown);
      EA510Symbology.Add(0, Symbologies.None);
      EA510Symbology.Add(99, Symbologies.EAN13);
      EA510Symbology.Add(100, Symbologies.EAN13);
      EA510Symbology.Add(68, Symbologies.EAN8);
      EA510Symbology.Add(98, Symbologies.Code39);
      EA510Symbology.Add(106, Symbologies.Code128);
      EA510Symbology.Add(73, Symbologies.GS1_128);
      EA510Symbology.Add(119, Symbologies.DATAMATRIX);
      EA510Symbology.Add(115, Symbologies.QRCODE);

      EA502Symbology = new Dictionary<int, Symbologies>();
      EA502Symbology.Add(-1, Symbologies.Unknown);
      EA502Symbology.Add(0, Symbologies.None);
      EA502Symbology.Add(11, Symbologies.EAN13);
      EA502Symbology.Add(12, Symbologies.EAN8);
      EA502Symbology.Add(1, Symbologies.Code39);
      EA502Symbology.Add(8, Symbologies.Code128);
      EA502Symbology.Add(48, Symbologies.GS1_128);
      EA502Symbology.Add(23, Symbologies.DATAMATRIX);
      EA502Symbology.Add(31, Symbologies.QRCODE);

      //  DISCRETE25 = 2, AZTEC = 32, CHINESE25 = 45, CODABAR = 5, CODE11 = 46, 
      //  CODE32 = 26, CODE93 = 7, COMPOSITE_CC_AB = 44, COMPOSITE_CC_C = 43, 
      //  COMPOSITE_TLC39 = 48, GS1_14 = 17, GS1_EXP = 19, GS1_LIMIT = 18, HANXIN = 49,
      //  INTERLEAVED25 = 4, MATRIX25 = 3, MAXICODE = 24, MICROPDF417 = 29, MSI = 14, 
      //  PDF417 = 22, POSTAL_4STATE = 36, POSTAL_AUSTRALIAN = 39, POSTAL_JAPAN = 41, 
      //  POSTAL_KIX = 40, POSTAL_PLANET = 34, POSTAL_POSTNET = 35, POSTAL_ROYALMAIL = 38, 
      //  POSTAL_UPUFICS = 37, RESERVED_13 = 13, RESERVED_15 = 15, RESERVED_16 = 16, 
      //  RESERVED_20 = 20, RESERVED_21 = 21, RESERVED_27 = 27, RESERVED_28 = 28, 
      //  RESERVED_30 = 30, RESERVED_33 = 33, RESERVED_6 = 6, TRIOPTIC = 25, UPCA = 9, 
      //  UPCE = 10, UPCE1 = 47

      if (_IsEA510)
      {
         EASymbology = EA510Symbology;
      }
      else
      {
         EASymbology = EA502Symbology;
      };
   }

   // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -

   public ObservableCollection<NameValue> LastScans { get; set; } = new ObservableCollection<NameValue>();

   // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -

   public enum USymbology { }

   public bool IsEA510
   {
      get => _IsEA510;
      set
      {
         if (SetField(ref _IsEA510, value))
         {
            if (_IsEA510)
            {
               EASymbology = EA510Symbology;
            }
            else
            {
               EASymbology = EA502Symbology;
            };
         };
      }
   }

#if DEBUG
   bool _IsEA510 = true;
#else
   bool _IsEA510 = false;
#endif

   // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -

   public Dictionary<int, Symbologies> EA502Symbology { get; }
   public Dictionary<int, Symbologies> EA510Symbology { get; }
   public Dictionary<int, Symbologies> EASymbology { get; set; }

   // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -

   public string Data { get => _Data; set => SetField(ref _Data, value); }
   string _Data = "";

   public int Length { get => _Length; set => SetField(ref _Length, value); }
   int _Length = -1;

   public Symbologies Symbology { get => _Symbology; set => SetField(ref _Symbology, value); }
   Symbologies _Symbology = Symbologies.None;

   public byte nSymbology { get => _nSymbology; set => SetField(ref _nSymbology, value); }
   byte _nSymbology = 0;

   public byte[] RAWData { get => _RAWData; set => _RAWData = value; }

   byte[] _RAWData = null;

   // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -

   /// <summary>
   /// Called by BroadcastReceiver ...
   /// </summary>
   /// <param name="data"></param>
   /// <param name="length"></param>
   /// <param name="symbology"></param>
   /// <param name="rawData"></param>
   public void NewBarcode(string data, int length, byte symbology, byte[] rawData)
   {
      Data = data;
      Length = length;

      // - - -  - - - 

      nSymbology = symbology;

      Symbologies _symbology = Symbologies.Unknown;
      if (EASymbology.TryGetValue(symbology, out _symbology))
      {
         Symbology = _symbology;
      }
      else
      {
         Symbology = Symbologies.Unknown;
      };

      // - - -  - - - 

      RAWData = rawData;

      if (Symbology == Symbologies.Unknown)
      {
         LastScans.Add(new NameValue { Name = Symbology.ToString() + $"({nSymbology})", Value = data });
      }
      else
      {
         LastScans.Add(new NameValue { Name = Symbology.ToString(), Value = data });
      };

      Xamarin.Essentials.Vibration.Vibrate(100);
   }

   // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -
}
