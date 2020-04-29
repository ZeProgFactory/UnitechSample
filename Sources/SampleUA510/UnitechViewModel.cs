using System;
using ZPF;

public class UnitechViewModel : BaseViewModel
{
   public enum USymbology
   {
      NONE = 0,
      EAN13_ = 99,    /* 11 */
      EAN13 = 100,    /* 11 */
      EAN8 = 68,      /* 12 */
      CODE39 = 98,    /*  1 */
      CODE128 = 106,  /*  8 */
      GS1_128 = 73,   /* 42 */
      DATAMATRIX = 119,/* 23 */
      QRCODE = 115,   /* 31 */
   }

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
   }

   // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -

   public string Data { get => _Data; set => SetField(ref _Data, value); }
   string _Data = "";

   public int Length { get => _Length; set => SetField(ref _Length, value); }
   int _Length = -1;

   public USymbology Symbology { get => _Symbology; set => SetField(ref _Symbology, value); }
   USymbology _Symbology = USymbology.NONE;

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
      Symbology = (USymbology)(symbology);
      RAWData = rawData;

      Xamarin.Essentials.Vibration.Vibrate(100);
   }

   // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -
}
