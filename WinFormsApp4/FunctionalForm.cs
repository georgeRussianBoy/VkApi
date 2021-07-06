using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;

namespace WinFormsApp4
{
    public partial class FunctionalForm : Form
    {
        string token = "";
        int visibleChars = 5;
        VkApi apiUser;
        VkApi apiCommunity;
        public FunctionalForm(VkApi apiUser, VkApi apiCommunity)
        {
            InitializeComponent();

            apiUser = new VkApi();
            apiCommunity = new VkApi();
            if (token.Length > visibleChars)
                label2.Text = token.Substring(0, visibleChars) + new string('*', token.Length - visibleChars);
            this.apiCommunity = apiCommunity;
            this.apiUser = apiUser;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Formforgetmembers f = new Formforgetmembers(apiUser, apiCommunity);
            f.ShowDialog();
           
        }
    }

}
