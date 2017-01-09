using SmartIT.Module.Cmdlets;
using SmartIT.Module.Model;
using SmartIT.Module.PowerShellAbstractionLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SmartIT.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();

            using (PowerShell PowerShellInstance = PowerShell.Create())
            {
                // this script has a sleep in it to simulate a long running script import-module SmartIT.Powershell.dll;
                PowerShellInstance.AddScript(""+
                                             "get-sitcompany -all");

                PSDataCollection<PSObject> outputCollection = new PSDataCollection<PSObject>();

                // begin invoke execution on the pipeline
                // use this overload to specify an output stream buffer
                // invoke execution on the pipeline (collecting output)
                Collection<PSObject> PSOutput = PowerShellInstance.Invoke();

                // loop through each output object item
                foreach (PSObject outputItem in PSOutput)
                {
                    if (outputItem != null)
                    {
                        Console.WriteLine(outputItem.BaseObject.GetType().FullName);

                    }
                }
            }
            Console.ReadLine();

        }
    }
}
