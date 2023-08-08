using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace serviceRestart
{
    internal class Program
    {
        public static void restartService(string serviceName, int timeOutMS)
        {
            ServiceController service = new ServiceController(serviceName);

            TimeSpan timeout = TimeSpan.FromMilliseconds(timeOutMS);

            try
            {
                //If service is stopped...
                if (service.Status == ServiceControllerStatus.Stopped)
                {
                    service.Start();

                    service.WaitForStatus(ServiceControllerStatus.Running, timeout);

                    MessageBox.Show($"The service '{serviceName}', has been successfully started!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                //If service is Running...
                else if (service.Status == ServiceControllerStatus.Running)
                {
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);

                    service.Start();
                    service.WaitForStatus(ServiceControllerStatus.Running, timeout);

                    MessageBox.Show($"The service '{serviceName}', has been successfully restarted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        static void Main(string[] args)
        {
            try
            {
                restartService("SERVICE_NAME", 30000);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
