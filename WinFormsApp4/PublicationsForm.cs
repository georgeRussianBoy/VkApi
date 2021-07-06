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
            for (int i = 0; i < users.Count; i++)
            {
                var post = _apiUser.Wall.Get(new VkNet.Model.RequestParams.WallGetParams
                {

                    OwnerId = users[i].Id,
                    Count = 1,
                });

                var repost = _apiUser.Wall.Repost(("wall" + users[i].Id.ToString() + "_" + post.WallPosts[0].Id.ToString()),message:"",
                    groupId: 205655768,markAsAds:false);  


        }
        }
    }
}
