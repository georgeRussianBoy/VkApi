using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using VkNet;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace WinFormsApp4
{
    public partial class Formforgetmembers : Form
    {
        string _tokenuser;
        string _tokengroup;
        VkApi _api_user = new VkApi();
        VkApi _api_group = new VkApi();
        List<vkuser> users = new List<vkuser>();
        public Formforgetmembers(VkApi api_user, VkApi api_group)
        {
            InitializeComponent();
            _api_user = api_user;
            _api_group = api_group;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                label1.Text = users[listBox1.SelectedIndex]._Information;
            }
            else
            {
                label1.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            users.Clear();
            // обработать исключения!


            var getFriends = _api_user.Friends.Get(new VkNet.Model.RequestParams.FriendsGetParams
            {
                Fields = VkNet.Enums.Filters.ProfileFields.All
            });
            string s = "";
            int i = 0;
            foreach (User user in getFriends)
            {
                s += user.Sex + "\n";
                s += user.Relation + "\n";
                s += user.HasMobile + "\n";
                s += user.BirthDate + "\n";
                users.Add(new vkuser(user.FirstName + " " + user.LastName, s));
                listBox1.Items.Add(users[i]._Name);
                i++;
                s = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
