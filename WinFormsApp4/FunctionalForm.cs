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
using System.Threading;

namespace WinFormsApp4
{
    public partial class FunctionalForm : Form
    {
        int visibleChars = 8;
        VkApi _apiUser;
        VkApi _apiCommunity;
        public FunctionalForm(VkApi apiUser, VkApi apiCommunity)
        {
            InitializeComponent();

/*            apiUser = new VkApi();
            apiCommunity = new VkApi();*/
            if (apiUser.Token.Length > visibleChars)
                label2.Text = apiUser.Token.Substring(0, visibleChars) + new string('*', apiUser.Token.Length - visibleChars);
            if (apiCommunity.Token.Length > visibleChars)
                label4.Text = apiCommunity.Token.Substring(0, visibleChars) + new string('*', apiCommunity.Token.Length - visibleChars);
            this._apiUser = apiUser;
            this._apiCommunity = apiCommunity;
/*            for (int i = 0; i < 100; ++i)
            {
                apiUser.Wall.Post(
                new VkNet.Model.RequestParams.WallPostParams
                {
                    OwnerId = -205655768,
                    Message = "тимур че делаеш"
                });
            }*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Formforgetmembers f = new Formforgetmembers(_apiUser, _apiCommunity);
            f.ShowDialog();
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DeletePostsForm f = new DeletePostsForm(_apiUser, _apiCommunity);
            f.ShowDialog();
        }
    }

}
