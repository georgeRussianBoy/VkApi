﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using VkNet;
using VkNet.Model;
using VkNet.Model.RequestParams;
using System.IO;

namespace WinFormsApp4
{
    public partial class Form4 : Form
    {
        VkApi apiUser;
        public Form4(VkApi apiUser)
        {
            InitializeComponent();
            this.apiUser = apiUser;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Определить возраст пользователя, как среднее арифметическое для возраста его друзей.
            //VkNet.Exception.VkApiMethodInvokeException: "This profile is private"
            int sum = 0;
            int k = 0;
            VkNet.Utils.VkCollection<User> friends;
            try
            {
                friends = apiUser.Friends.Get(new VkNet.Model.RequestParams.FriendsGetParams
                {
                    UserId = Convert.ToInt64(textBox1.Text),
                    Fields = VkNet.Enums.Filters.ProfileFields.All
                });
            }
            catch (VkNet.Exception.VkApiMethodInvokeException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            catch (System.FormatException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            
            var now = DateTime.Today;
            foreach (User user in friends)
            {
                if (user.BirthDate?.Length >= 8)
                {
                    k++;
                    var birth = DateTime.Parse(user.BirthDate);
                    int age = now.Year - birth.Year;
                    sum += age;
                }
            }
            textBox2.Text += (sum / k).ToString() + " лет\n";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var now = DateTime.Today;
            VkNet.Utils.VkCollection<User> getFollow;
            try
            {
                getFollow = vkapi.Groups.GetMembers(new GroupsGetMembersParams()
                {
                    GroupId = textBox1.Text,
                    Fields = VkNet.Enums.Filters.UsersFields.FirstNameAbl
                });
            }
            catch (VkNet.Exception.VkApiException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            foreach (User user in getFollow)
            {
                var birth = DateTime.Parse(user.BirthDate);
                if (now.Day == birth.Day && now.Month == birth.Month)
                {
                    var post = vkapi.Wall.Post(new WallPostParams
                    {
                        Message = "С днем рождения" + "," + user.FirstName + user.LastName
                    });
                }
            }
        }


    }
}