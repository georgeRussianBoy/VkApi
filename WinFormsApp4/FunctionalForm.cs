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
        int visibleChars = 5;
        VkApi _apiUser;
        VkApi _apiCommunity;
        public FunctionalForm(ref VkApi apiUser, ref VkApi apiCommunity)
        {
            InitializeComponent();

/*            apiUser = new VkApi();
            apiCommunity = new VkApi();*/
            if (apiUser.Token.Length > visibleChars)
                label2.Text = apiUser.Token.Substring(0, visibleChars) + new string('*', apiUser.Token.Length - visibleChars);

            this._apiUser = apiUser;
            this._apiCommunity = apiCommunity;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Formforgetmembers f = new Formforgetmembers(_apiUser, _apiCommunity);
            f.ShowDialog();
           
        }

        private void PubMakeForm_Click(object sender, EventArgs e)
        {
            PublicationsForm f = new PublicationsForm(_apiUser,_apiCommunity);
            f.Show();
        }
    }

}
