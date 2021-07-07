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
                    MessageBox.Show(user.BirthDate);
                    var birth = DateTime.Parse(user.BirthDate);
                    int age = now.Year - birth.Year;
                    sum += age;
                }
            }
            textBox2.Text = (sum / k).ToString() + " лет\n";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var now = DateTime.Today;
            VkNet.Utils.VkCollection<User> getFollow;
            try
            {
                getFollow = apiUser.Groups.GetMembers(new GroupsGetMembersParams()
                {
                    GroupId = textBox1.Text,
                    Fields = VkNet.Enums.Filters.UsersFields.All
                });
            }
            catch (VkNet.Exception.VkApiException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            string s = "";
            bool greet = false;
            foreach (User user in getFollow)
            {
                if (user.BirthDate?.Length >= 3)
                {
                    var birth = DateTime.Parse(user.BirthDate);
                    if (now.Day == birth.Day && now.Month == birth.Month)
                    {
                        var post = apiUser.Wall.Post(new WallPostParams
                        {
                            OwnerId = -int.Parse(textBox1.Text),
                            FromGroup = true,
                            Message = $"С днем рождения, @id{user.Id} ({user.FirstName} {user.LastName})"
                        });
                        s += user.FirstName + " " + user.LastName + " Поздравлен(а)\n";
                        greet = true;
                    }
                }
            }
            if (!greet)
            {
                textBox2.Text = "Никто не поздравлен!";
            }
            else
            {
                textBox2.Text = s;
            }
        }


    }
}
