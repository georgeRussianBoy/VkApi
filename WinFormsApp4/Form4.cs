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
        VkApi vkapi;
        string forUsers;
        public Form4(string forUsers)
        {
            InitializeComponent();
            VkApi vkapi = new VkApi();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            vkapi.Authorize(new ApiAuthParams
            {
                AccessToken = forUsers
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Определить возраст пользователя, как среднее арифметическое для возраста его друзей.
            
            int sum = 0;
            int k = 0;
            var friends = vkapi.Friends.Get(new VkNet.Model.RequestParams.FriendsGetParams
            {
                UserId = Convert.ToInt64(textBox1.Text),
                Fields = VkNet.Enums.Filters.ProfileFields.All
            }) ;
            var now = DateTime.Today;
            foreach (User user in friends)
            {
                k++;
                var birth = DateTime.Parse(user.BirthDate);
                int age = now.Year - birth.Year;
                sum += age;
            }
            listBox1.Text += sum / k + "лет";
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

       
    }
}
