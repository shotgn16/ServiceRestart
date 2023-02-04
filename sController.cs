using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace serviceRestart
{
    internal class sController
    {
        ServiceController service = new ServiceController();

        public static async Task<string> startService(ServiceController service, string returnValue = null)
        {
            try
            {
                if (service.Status == ServiceControllerStatus.Running)
                {
                    returnValue = "Service already running";
                }

                else if (service.Status == ServiceControllerStatus.Stopped)
                {
                    service.Start();

                    returnValue = "Started Successfully!";
                }
            }

            catch (Exception ex) 
            {
                MessageBox.Show("Error - " + ex.Message + " /n/n " + ex.StackTrace , "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return returnValue;
        }

        public static async Task<string> stopService(ServiceController service, string returnValue = null)
        {
            try
            {
                if (service.Status == ServiceControllerStatus.Stopped) 
                {
                    returnValue = "Service is not running";
                }

                else if (service.Status == ServiceControllerStatus.Running)
                {
                    service.Stop();

                    returnValue = "Stopped Successfully";
                }
            }

            catch (Exception ex) 
            {
                MessageBox.Show("Error - " + ex.Message + " /n/n " + ex.StackTrace, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return returnValue;
        }

       public static async Task<string> restartService(ServiceController service, string returnValue = null)
        {
            try
            {
                if (service.Status == ServiceControllerStatus.Running)
                {
                    service.Stop();
                    service.Start();

                    returnValue = "Restarted Successfully";
                }

                else if (service.Status == ServiceControllerStatus.Stopped)
                {
                    service.Start();

                    return "Started Successfully";
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error - " + ex.Message + " /n/n " + ex.StackTrace, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return returnValue;
        }
    }
}