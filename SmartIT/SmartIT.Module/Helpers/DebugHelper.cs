using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartIT.Module.Helpers
{
   public static class DebugHelper
    {
        public static string WriteDebug(string message)
        {
            return $"{DateTime.UtcNow.ToString()}: {message} \r\n";
        }
    }
}
