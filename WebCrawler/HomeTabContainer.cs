using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebCrawler
{
    public partial class HomeTabContainer : UserControl
    {
        private string link;
        public HomeTabContainer()
        {
            InitializeComponent();
        }

        public string NameTagText
        {
           set
            {
                label3.Text = value;
            }
        }

        public string DateTagText
        {
            get
            {
                return label1.Text;
            }

            set
            {
                label1.Text = value;
            }
        }

        public string PriceTagText
        {
            get
            {
                return label2.Text;
            }

            set
            {
                label2.Text = value;
            }
        }

        public string URI
        {
            get { return link; }
            set { link = value;}
        }
        public void LoadImageFromUrl(string url)
        {
            pictureBox1.Load(url);
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
        }

        private void HomeTabContainer_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(106, 106, 114);
            if (link != null)
            {
                System.Diagnostics.Process.Start(URI);
            }
        }

        private void HomeTabContainer_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(85, 85, 96);
        }

        private void HomeTabContainer_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(55, 55, 65);
        }
    }
}
