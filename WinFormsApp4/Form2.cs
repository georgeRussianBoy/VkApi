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
        VkApi _api_user;
        VkApi _api_group;
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
            int a = -1;
            try
            {
                if (textBox1.Text != "")
                {
                    a = Convert.ToInt32(textBox1.Text);
                }
            }
            catch
            {
                return;
            }
            try
            {
                var getFriends = _api_user.Friends.Get(new VkNet.Model.RequestParams.FriendsGetParams
                {
                    UserId = a,
                    Fields = VkNet.Enums.Filters.ProfileFields.All

                });
                string s = "";
                int i = 0;
                foreach (User user in getFriends)
                {
                    if (checkBox1.Checked)
                    {
                        s += "Пол:" + user.Sex + "\n";
                    }
                    if (checkBox2.Checked)
                    {
                        s += "Отношения:" + user.Relation + "\n";
                    }
                    if (checkBox3.Checked)
                    {
                        s += "Номер телефона:" + user.MobilePhone + "\n";
                    }
                    if (checkBox4.Checked)
                    {
                        s += "День рождения:" + user.BirthDate + "\n";
                    }
                    users.Add(new vkuser(user.FirstName + " " + user.LastName, s));
                    listBox1.Items.Add(users[i]._Name);
                    i++;
                    s = "";
                }
            }
            catch
            {
                MessageBox.Show("Такого пользователя не существует");
                return;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            users.Clear();
            try
            {
                var getFollowers = _api_user.Groups.GetMembers(new GroupsGetMembersParams()
                {
                    GroupId = textBox1.Text,
                    Fields = VkNet.Enums.Filters.UsersFields.FirstNameAbl
                });
                string s = "";
                int i = 0;
                foreach (User user in getFollowers)
                {
                    if (checkBox1.Checked)
                    {
                        s += "Пол:" + user.Sex + "\n";
                    }
                    if (checkBox2.Checked)
                    {
                        s += "Отношения:" + user.Relation + "\n";
                    }
                    if (checkBox3.Checked)
                    {
                        s += "Номер телефона:" + user.MobilePhone + "\n";
                    }
                    if (checkBox4.Checked)
                    {
                        s += "День рождения:" + user.BirthDate + "\n";
                    }
                    users.Add(new vkuser(user.FirstName + " " + user.LastName, s));
                    listBox1.Items.Add(users[i]._Name);
                    i++;
                    s = "";
                }
            }
            catch
            {
                MessageBox.Show("Такой группы не существует");
                return;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            users.Clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
