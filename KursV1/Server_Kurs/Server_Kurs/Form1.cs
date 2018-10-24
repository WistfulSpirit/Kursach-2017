using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;


namespace Server_Kurs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        delegate void mssg(RichTextBox rtb, string text);

        mssg AcceptDel = (RichTextBox rtb, string text) =>
        {
            rtb.Text += text + "\n";
        };

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Server s = new Server(Convert.ToInt32(textBox1.Text));
                s.StartListen(richTextBox1);
                button1.Enabled = false;
                richTextBox1.Text += "Server started\n";
                s.CheckList(richTextBox1);
            }
            catch (Exception)
            {
                MessageBox.Show(e.ToString());
            }

        }

    }
}

