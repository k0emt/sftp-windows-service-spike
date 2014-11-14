using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace TransferService
{
    public partial class TransferService : ServiceBase
    {
        Boolean transferringAllowed = false;
        EventLog eventLog1 = new EventLog();

        public TransferService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // event log starting up

            // load up configuration settings
            var appSettings = ConfigurationManager.AppSettings;
            string sftpUser = appSettings["user"];

            // event log 
            if (!System.Diagnostics.EventLog.SourceExists("MyTransferService"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "MyTransferService", "MyLog");
            }
            // configure the event log instance to use this source name
            eventLog1.Source = "MyTransferService";
            eventLog1.WriteEntry("In OnStart.");
            eventLog1.WriteEntry("user: " + sftpUser);

            // spin up a timer
            // add an event to the timer -- just have it event log for now
        }

        protected override void OnStop()
        {
            // event log stopping
            eventLog1.WriteEntry("In OnStop.");
        }

        protected override void OnPause()
        {
            // event log pausing
            eventLog1.WriteEntry("In OnPause.");
        }

        protected override void OnContinue()
        {
            // event log continue
            eventLog1.WriteEntry("In OnContinue.");
        }
    }
}
