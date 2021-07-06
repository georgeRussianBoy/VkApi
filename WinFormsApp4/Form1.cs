using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using VkNet;

namespace WinFormsApp4
{
    public partial class Form1 : Form
    {
        string file_path = @"C:\Users\USER_NAME\Desktop\token.txt";
        public Form1()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = !textBox1.Enabled;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string userToken = null;
            string communityToken = null;
            if (checkBox1.Checked)
            {
                userToken = getTokenFromFile(false); // false - for user
                communityToken = getTokenFromFile(true);  // true - for community
            }
                
            else
                userToken = textBox1.Text;
                communityToken = textBox2.Text;
        }

        private string getTokenFromFile(bool forCommunity)
        {
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\')[1];
            string real_file_path = file_path.Replace("USER_NAME", userName);
            string token = "";  
            try
            {
                using (var sr = new StreamReader(real_file_path))
                {
                    if (forCommunity)
                        sr.ReadLine();
                    token = sr.ReadLine();
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("File " + real_file_path + " not exists");
            }
            return token;
        }
    }
}
