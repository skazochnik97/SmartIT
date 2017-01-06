using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace SmartIT.Module.PowerShellAbstractionLayer
{


    public abstract class ConnectionCmdlet : SmartIT.Module.PowerShellAbstractionLayer.CmdletBase
    {
      /*  [ValidateNotNull]
        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public ServerConnection SmartITServer
        {
            get
            {
                return this.serverConnection;
            }
            set
            {
                if (value == null)
                    return;
                this.serverConnection = value;
            }
        }
        */
        protected ConnectionCmdlet()
          : base()
        {
        }

    }
}
