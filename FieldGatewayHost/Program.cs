using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace FieldGatewayHost
{
    static class Program
    {
        private static LogManager logger = new LogManager();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            System.IO.Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

            try
            {

                //Task.Run(() =>
                //{
                //    StringBuilder sb = new StringBuilder();
                //    sb.AppendLine($"System.Environment.CurrentDirectory - {System.Environment.CurrentDirectory}");
                //    sb.AppendLine($"System.AppDomain.CurrentDomain.BaseDirectory - {System.AppDomain.CurrentDomain.BaseDirectory}");

                //    var items = System.Environment.GetEnvironmentVariables();
                //    foreach (var i in items)
                //    {
                //        sb.AppendLine($"{((System.Collections.DictionaryEntry)i).Key} {((System.Collections.DictionaryEntry)i).Value}");
                //    }

                //    logger.Debug(sb.ToString());
                //});

                var service = new SampleService(logger);

                var args = Environment.GetCommandLineArgs();
                if (args.Any(x => x.Equals("/DebugInConsole", StringComparison.OrdinalIgnoreCase)))
                {
                    service.StartService();
                    Console.ReadLine();
                    service.StopService();
                }
                else
                {
                    ServiceBase[] servicesToRun;
                    servicesToRun = new ServiceBase[]
                    {
                        service
                    };
                    ServiceBase.Run(servicesToRun);
                }
            }
            catch (Exception ex)
            {
                logger.Fatal("Unable to run FieldGatewayHost", ex);
            }
        }


        /// <summary>
        /// Handles the UnobservedTaskException event of the TaskScheduler control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="UnobservedTaskExceptionEventArgs"/> instance containing the event data.</param>
        private static void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            if (e.Exception != null)
            {
                logger.Fatal("Unhandled exception in async code in FieldGatewayHost", e.Exception);
            }
        }

        /// <summary>
        /// Handles the UnhandledException event of the CurrentDomain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="UnhandledExceptionEventArgs"/> instance containing the event data.</param>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject != null)
            {
                logger.Fatal($"Unhandled exception in FieldGatewayHost, err: {e.ExceptionObject}");
            }
        }
    }
}
