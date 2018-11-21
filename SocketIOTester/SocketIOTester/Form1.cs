using Newtonsoft.Json.Linq;
using Quobject.SocketIoClientDotNet.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocketIOTester
{
    public partial class Form1 : Form
    {
        private Socket socket;

        public Form1()
        {
            InitializeComponent();
            id = new Random().Next(100);
            this.Text = "Channel Id: " + id.ToString();
        }
        int id;

        #region Page Load
        private void Form1_Load(object sender, EventArgs e)
        {
            
            this.socket = IO.Socket("http://localhost:5000");
            socket.On(Socket.EVENT_CONNECT, () =>
            {
                JObject obj = new JObject();
                obj["Time"] = JToken.FromObject(DateTime.Now);
                obj["id"] = JToken.FromObject(id.ToString());
                socket.Emit("register", obj);
            })
            .On("testMessage", (data) =>
            {
                var token = JToken.FromObject(data);
                MessageBox.Show(this.id.ToString());
            });
            

        }
        #endregion
        private void button1_Click(object sender, EventArgs e)
        {
           
            JObject obj = new JObject();
            obj["Name"] = JToken.FromObject("Shlomi");
            socket.Emit("register",obj);
        }

        
    }
}
