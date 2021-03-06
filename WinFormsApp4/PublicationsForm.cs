using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using VkNet;
using VkNet.Model;
 
namespace WinFormsApp4
{
    public partial class PublicationsForm : Form
    {
        List<User> users = new List<User>();
        VkApi _apiUser;
        VkApi _apiCommunity;
        List<string> posts = new List<string>();
        List<int> index = new List<int>();
        
        public PublicationsForm(VkApi apiUser,VkApi apiCommunity)
        {
            _apiUser = apiUser;
            _apiCommunity = apiCommunity;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            users.Clear();
            var getFriends = _apiUser.Friends.Get(new VkNet.Model.RequestParams.FriendsGetParams
            {
                Fields = VkNet.Enums.Filters.ProfileFields.All
            });
            string s = "";
            int i = 0;
            foreach (User user in getFriends)
            {
                users.Add(user);
                listBox1.Items.Add(users[i].FirstName + ' '  + users[i].LastName);
                i++;
                 
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            if (listBox1.SelectedIndex != -1)
            {
                int i = listBox1.SelectedIndex;
                var post = _apiUser.Wall.Get(new VkNet.Model.RequestParams.WallGetParams
                {
                    OwnerId = users[i].Id,
                     
                }) ;
                if (listBox2.SelectedItems.Count!=0)
                    MessageBox.Show(Reposting(ref post,users[i]));
                else
                    MessageBox.Show("Неверно введены данные");     
            }
            else
            {
                MessageBox.Show("Выберите пользователя");
            }
        }
        private string Reposting(ref WallGetObject post,User user)
        {
            int id = 0;
            if(!int.TryParse(textBox1.Text, out id))
            {
                return "incorrect group ID";
            }

            foreach(int index in listBox2.SelectedIndices)
            { 
                try
                {
                    var repost = _apiUser.Wall.Repost(("wall" + user.Id.ToString() + "_" + post.WallPosts[index].Id.ToString()), message: "",
                groupId: id, markAsAds: false);
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                
            }
            return "Complete!";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex != -1)
            {
                 
                int i = listBox1.SelectedIndex;
                WallGetObject post;
                try
                {
                    post = _apiUser.Wall.Get(new VkNet.Model.RequestParams.WallGetParams
                    {
                        OwnerId = users[i].Id,

                    });
                }
                catch (VkNet.Exception.UserDeletedOrBannedException ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                
                label1.ForeColor = Color.Red;
                label1.Text = $"Posts:{post.TotalCount}";
                listBox2.Items.Clear();
                for (int k=0;k<(int)post.TotalCount;k++)
                {
                    listBox2.Items.Add($"пост {k}");
                }
            }
            else
            {
                label1.Text = "";
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
             

        }
    }
}
