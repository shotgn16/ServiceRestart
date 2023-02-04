using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceProcess;

namespace serviceRestart
{
    public partial class Form1 : Form
    {
        private static ServiceController[] serviceList = ServiceController.GetServices();

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            bool isAdmin = await Other.isAdministrator();

            if (!isAdmin) { MessageBox.Show("Please run the application with administrator rights to continue...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); Application.Exit(); }

            foreach (ServiceController service in serviceList)
            {
                listBox1.Items.Add(service.DisplayName);
            }
        }

        //Start
        private async void button1_Click(object sender, EventArgs e)
        {
            ServiceController targetService = new ServiceController(listBox1.SelectedItem.ToString());
            label3.Text = await sController.startService(targetService);
        }

        //Stop
        private async void button2_Click(object sender, EventArgs e)
        {
            ServiceController targetService = new ServiceController(listBox1.SelectedItem.ToString());
            label3.Text = await sController.stopService(targetService);
        }

        //Restart
        private async void button3_Click(object sender, EventArgs e)
        {
            ServiceController targetService = new ServiceController(listBox1.SelectedItem.ToString());
            label3.Text = await sController.restartService(targetService);
        }
    }
}
