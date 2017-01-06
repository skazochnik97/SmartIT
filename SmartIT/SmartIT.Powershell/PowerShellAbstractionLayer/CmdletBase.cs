using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace SmartIT.Module.PowerShellAbstractionLayer
{
    public abstract class CmdletBase : PSCmdlet, IDynamicParameters
    {
        private bool isServerConnectionRequired;
      //  private ServerConnection serverConnectionInternal;

        protected CmdletBase()
            : this(true)
        {
        }

        protected CmdletBase(bool isServerConnectionRequired)
        {
            this.isServerConnectionRequired = isServerConnectionRequired;
        }

    /*    protected ServerConnection serverConnection
        {
            get
            {
                if (this.serverConnectionInternal != null && !this.serverConnectionInternal.IsConnected)
                {
                    ServerConnection equivalentConnection = (ServerConnection)null;
                    if (ServerConnection.TryGetExistingServerConnection(this.serverConnectionInternal, out equivalentConnection))
                        this.serverConnectionInternal = equivalentConnection;
                }
                return this.serverConnectionInternal;
            }
            set
            {
                this.serverConnectionInternal = value;
            }
        }
        */
        protected override void ProcessRecord()
        {

           // if (!this.InitializeServerConnection())
            //    return;

            try
            {
                this.ValidateParameters();
                this.Process();
            }
            catch (Exception e)//(ValidationException e)
            {
                this.WriteObject(e.Message);
            }
            /*catch (Exception ex)
            {
                throw;
            }
            */
        }


        public object GetDynamicParameters()
        {
            return (object)null;
        }

       /* private bool InitializeServerConnection()
        {
            bool flag = true;
            if (this.isServerConnectionRequired)
            {
                if (this.serverConnection == null)
                {
                    try
                    {
                        this.serverConnection = ServerConnection.ActiveConnection;
                    }
                    catch (Exception ex)
                    {
                        WriteError(new ErrorRecord(ex, "", ErrorCategory.NotSpecified, null));

                        flag = false;
                        // throw;
                        //    this.HandleCarmineException((CarmineException)ex);
                    }
                }
            }
            else
                this.serverConnection = (ServerConnection)null;
            return flag;
        }

    */

        protected abstract void Process();

        protected virtual void ValidateParameters()
        {
        }
    }

}
