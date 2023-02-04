using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace serviceRestart
{
    public class Other
    {
        public static async Task<bool> isAdministrator(bool isAdmin = false, string returnValue = null)
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal prnciple = new WindowsPrincipal(identity);
            isAdmin = prnciple.IsInRole(WindowsBuiltInRole.Administrator);

            return isAdmin;
        }
    }
}
