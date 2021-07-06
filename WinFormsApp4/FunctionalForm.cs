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
        string token = "";
        int visibleChars = 5;
        VkApi api;
        public FunctionalForm(string token)
        {
            InitializeComponent();

            VkApi api = new VkApi();
            if (token.Length > visibleChars)
                label2.Text = token.Substring(0, visibleChars) + new string('*', token.Length - visibleChars);
        }
    }

}
