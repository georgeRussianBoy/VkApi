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
    public partial class DeletePostsForm : Form
    {
        VkApi apiUser, communityApi;

        public DeletePostsForm(VkApi apiUser, VkApi communityApi)
        {
            InitializeComponent();
            this.apiUser = apiUser;
            this.communityApi = communityApi;
            progressBar1.Visible = false;
            progressBar1.Minimum = 0;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Enabled = !textBox2.Enabled;
            button1.Text = button1.Text == "Delete" ? "Delete ALL posts" : "Delete";
            label2.Enabled = !label2.Enabled;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            int ownerId = 0;
            if (!int.TryParse(textBox1.Text, out ownerId) && ownerId < 1) 
                return;
            ownerId = communityRadioButton.Checked ? ownerId * -1 : ownerId;

            if (checkBox1.Checked)
            {
                DialogResult dr = MessageBox.Show("This will DELETE ALL posts on your wall",
                "Are you sure?", MessageBoxButtons.YesNo);
                if (dr == DialogResult.No)
                    return;
                while (delete_last(ownerId, 100) == 100) ;
            }
            else
            {
                int count = int.Parse(textBox2.Text);

                for (int i = 0; i < count / 100; ++i)
                {
                    if (delete_last(ownerId, 100) < 100)
                        break;
                }
                delete_last(ownerId, count % 100);
            }
            progressBar1.Visible = false;
        }

        ulong delete_last(long ownerId, int count)
        {
            progressBar1.Visible = true;
            var collectionForDelete = apiUser.Wall.Get(new VkNet.Model.RequestParams.WallGetParams
            {
                OwnerId = ownerId,
                Count = (ulong)count
            });

            progressBar1.Maximum = (int)collectionForDelete.TotalCount;


            foreach (var post in collectionForDelete.WallPosts)
            {
                apiUser.Wall.Delete(ownerId, post.Id);
                System.Threading.Thread.Sleep(200);
                progressBar1.Value++;
            }
            progressBar1.Value = 0;
            return collectionForDelete.TotalCount;
        }

    }
}
