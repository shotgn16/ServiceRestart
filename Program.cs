using System.ServiceProcess;

namespace ServiceRestart_Net_7
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using (var ServiceClass = new CustomServiceManager())
            {
                var Controller = await ServiceClass.BuildController("MyQ_Sharp", 30);

                if (await ServiceClass.restartService(Controller))
                    Console.WriteLine($"Service.{Controller.ServiceName} restarted successfully!");
            }
        }
    }

    internal class CustomServiceManager : IDisposable
    {
        private ServiceController _serviceController;
        private TimeSpan _timeout;

        internal async Task<bool> IsValid(object input, bool returnValue = false)
        {
            if (input.GetType() == typeof(string))
            {
                if (!string.IsNullOrEmpty(Convert.ToString(input)))
                    returnValue = true;
            }

            else if (input.GetType() == typeof(Int32))
            {
                if (Convert.ToInt32(input) != 0)
                    returnValue = true;
            }

            return returnValue;
        }

        internal async Task<ServiceController> BuildController(string serviceName, int timeoutMS, ServiceController returnValue = null)
        {
            if (await IsValid(serviceName) && await IsValid(timeoutMS))
            {
                _serviceController = new ServiceController(serviceName);
                    _timeout = TimeSpan.FromSeconds(timeoutMS);
                returnValue = _serviceController;
            }

            return returnValue;
        }

        internal async Task<bool> restartService(ServiceController _ServiceController, bool returnValue = false)
        {
            try
            {
                if (_ServiceController.Status == ServiceControllerStatus.Stopped)
                {
                    _ServiceController.Start();
                        _ServiceController.WaitForStatus(ServiceControllerStatus.Running, _timeout);
                    returnValue = true;
                }

                else if (_ServiceController.Status == ServiceControllerStatus.Running)
                {
                    _ServiceController.Stop();
                        _ServiceController.WaitForStatus(ServiceControllerStatus.Stopped, _timeout);

                    _ServiceController.Start();
                        _ServiceController.WaitForStatus(ServiceControllerStatus.Running, _timeout);

                    returnValue = true;
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message + ex.StackTrace);
            }

            return returnValue;
        }

        public void Dispose()
        {
            GC.Collect();
        }
    }
}
