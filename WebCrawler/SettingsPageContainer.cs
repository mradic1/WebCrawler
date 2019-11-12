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
    public partial class SettingsPageContainer : UserControl
    {
        public SettingsPageContainer()
        {
            InitializeComponent();
        }

        public string Link1CategoryText
        {
            get
            {
                return textBox2.Text;
            }
            set
            {
                textBox2.Text = value;
            }
        }

        public string Link2CategoryText
        {
            get
            {
                return textBox3.Text;
            }
            set
            {
                textBox3.Text = value;
            }
        }
        public string Link3CategoryText
        {
            get
            {
                return textBox4.Text;
            }
            set
            {
                textBox4.Text = value;
            }
        }

        public string Link4CategoryText
        {
            get
            {
                return textBox5.Text;
            }
            set
            {
                textBox5.Text = value;
            }
        }

        public string MailTextboxText
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        public string HomePageLink
        {
            get { return textBox6.Text; }
            set { textBox6.Text = value; }
        }

        public RichTextBox RichBox()
        {
            return richTextBox1;
        }

        public void button1_Click(object sender, EventArgs e)
        {
            ButtonClicked(EventArgs.Empty);
        }

        protected virtual void ButtonClicked(EventArgs e)
        {
            EventHandler handler = buttonClicked;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler buttonClicked;
    }
}
