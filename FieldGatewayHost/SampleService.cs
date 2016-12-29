using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.IoT.Gateway;

namespace FieldGatewayHost
{
    public partial class SampleService : ServiceBase
    {
        private readonly ILog log;
        private IntPtr fieldGatewayHandle = IntPtr.Zero;
        private const string ModulesPath = "fgmodules.json";

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldGatewayService.FieldGatewayServiceHost"/> class.
        /// </summary>
        /// <param name="log">The log.</param>
        /// <param name="fieldGateway">The field gateway logic. monitor.</param>
        public SampleService(ILog log)
        {
            this.InitializeComponent();
            this.log = log;
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void StartService()
        {
            this.OnStart(null);
        }

        /// <summary>
        /// Stops the executing service.
        /// </summary>
        public void StopService()
        {
            this.OnStop();
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Start command is sent to the service by the Service Control Manager (SCM) or when the operating system starts (for a service that starts automatically). Specifies actions to take when the service starts.
        /// </summary>
        /// <param name="args">Data passed by the start command.</param>
        protected override void OnStart(string[] args)
        {
            this.log.Debug("Starts FieldGatewayService");
            //Task.Run(() =>
            //{
                try
                {
                    this.fieldGatewayHandle = NativeDotNetHostWrapper.GatewayCreateFromJson(ModulesPath);
                    if (this.fieldGatewayHandle == IntPtr.Zero)
                    {
                        throw new Exception("Unable to create field gateway handle. Check configuration");
                    }
                    this.log.Debug("FG created successfully");
                }
                catch (Exception ex)
                {
                    this.log.Fatal("Something went wrong", ex);
                    this.Stop();
                }
            //});
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Stop command is sent to the service by the Service Control Manager (SCM). Specifies actions to take when a service stops running.
        /// </summary>
        protected override void OnStop()
        {
            this.log.Debug("Stops FieldGatewayHost");

            try
            {
                if (this.fieldGatewayHandle != IntPtr.Zero)
                {
                    NativeDotNetHostWrapper.GatewayDestroy(this.fieldGatewayHandle);
                }
            }
            catch (Exception ex)
            {
                this.log.Error("Failed to gracefully close FieldGatewayHost", ex);
            }

            this.log.Debug("FieldGatewayHost was stopped");
        }
    }
}
