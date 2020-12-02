using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZPF.XF
{
   public interface IScanner
   {
      bool OpenScanner();
      bool CloseScanner();

      bool EnableAllSymbologies();
      bool EnableEAN13Only();
      bool EnableAllEANs();
   }
}
