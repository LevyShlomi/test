using SocketIO.ServiceModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Example
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ServiceFactory serviceFactory;
        SocketIO.ServiceModel.ServiceHost host;
        private ServiceEndpoint serviceEndpoint;

        private void Form1_Load(object sender, EventArgs e)
        {
            serviceFactory = new ServiceFactory();
            host = new SocketIO.ServiceModel.ServiceHost(serviceFactory);
            var service = new Service();
            this.serviceEndpoint = host.AddServiceEndpoint<IService>(new Uri("http://localhost:5000"), service);
            host.Open();
        }

        class Service : IService
        {
            public ProcessInfo Get(int i)
            {
                var p = Process.GetProcessById(i);
                return new ProcessInfo(p);

            }

            public List<ProcessInfo> ListProcesses()
            {
                return Process.GetProcesses().Select(p => new ProcessInfo(p)).ToList();
            }
        }
    }
    [ServiceContract(Name ="test-service")]
    public interface IService
    {
        [OperationContract(Name = "list")]
        List<ProcessInfo> ListProcesses();
        [OperationContract(Name ="get-processes")]
        ProcessInfo Get(int i);
    }

    public class ProcessInfo
    {
        public ProcessInfo(Process p)
        {
            Name = p.ProcessName;
            Pid = p.Id;
        }

        public string Name { get; set; }
        public int Pid { get; set; }
    }


}
