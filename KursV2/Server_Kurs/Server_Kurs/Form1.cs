using System;
using System.Windows.Forms;
using System.IO;
namespace Server_Kurs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            FileStream file = new FileStream("info.dat", FileMode.OpenOrCreate);
            file.Close();
            textBox1.Text = Properties.Settings.Default.ThisPort.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ThisPort = Convert.ToInt32(textBox1.Text);
            Properties.Settings.Default.Save();
            Server s = new Server(Convert.ToInt32(textBox1.Text));
            s.StartListen(richTextBox1,button1);
            button1.Enabled = false;
            if (s!=null && !s.error)
            {
                richTextBox1.Text += "Server started\n";
                button1.Enabled = false;
            }
            else
                button1.Enabled = true;
            s.CheckList(richTextBox1);
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (String.IsNullOrWhiteSpace((sender as TextBox).Text))
                button1.Enabled = false;
            else
                button1.Enabled = true;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }
    }
}

