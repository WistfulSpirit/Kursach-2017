using System;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;

namespace Client_Kurs
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            textBox1.Text = Properties.Settings.Default.ServersIPStatic;
            textBox2.Text = Properties.Settings.Default.ServersPortStatic.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ServersIPStatic = textBox1.Text;
            Properties.Settings.Default.ServersPortStatic = Convert.ToInt32(textBox2.Text);
            Properties.Settings.Default.Save();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox tbx = sender as TextBox;
            if (String.IsNullOrWhiteSpace(tbx.Text))
            {
                button1.Enabled = false;
            }
            else
            {
                Regex regex = new Regex(@"((1\d\d|2([0-4]\d|5[0-5])|\D\d\d?)\.?){4}$");
                if (regex.IsMatch(tbx.Text))
                {
                    textBox1.ForeColor = System.Drawing.Color.Black;
                    button1.Enabled = true;
                }
                else
                {
                    button1.Enabled = false;
                    textBox1.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
        }
    }
}
