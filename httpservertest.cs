using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkHttp
{
    public partial class httpservertest : Form
    {
        public Label lbcontent { get { return label1; } }
        private Http.HttpServer hs;
        private myHandler mh;
        public httpservertest()
        {
            InitializeComponent();

        }

        private void httpservertest_Load(object sender, EventArgs e)
        {
           hs = new Http.HttpServer(new Http.Server(1432));
           hs.Handlers.Clear();
           mh = new myHandler(this);
           hs.Handlers.Add(mh);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            hs.Handlers.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            hs.Handlers.Add(mh);
        }
    }
}
