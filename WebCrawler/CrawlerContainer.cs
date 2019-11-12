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
    public partial class CrawlerContainer : UserControl
    {
        public CrawlerContainer()
        {
            InitializeComponent();
            checkBox1.Checked = true;
            checkBox2.Checked = true;
            checkBox3.Checked = true;
            checkBox4.Checked = true;

        }

        public ComboBox combo1()
        {
            return comboBox1;
        }

        public ComboBox combo2()
        {
            return comboBox2;
        }

        public ComboBox combo3()
        {
            return comboBox3;
        }

        public ComboBox combo4()
        {
            return comboBox4;
        }

        public CheckBox check1()
        {
            return checkBox1;
        }

        public CheckBox check2()
        {
            return checkBox2;
        }

        public CheckBox check3()
        {
            return checkBox3;
        }

        public CheckBox check4()
        {
            return checkBox4;
        }

        public TextBox box1()
        {
            return textBox1;
        }

        public TextBox box2()
        {
            return textBox2;
        }

        public TextBox box3()
        {
            return textBox3;
        }

        public TextBox box4()
        {
            return textBox4;
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Visible = checkBox1.Checked;
            comboBox1.Enabled = checkBox1.Checked;
            
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Visible = checkBox2.Checked;
            comboBox2.Enabled = checkBox2.Checked;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            textBox3.Visible = checkBox3.Checked;
            comboBox3.Enabled = checkBox3.Checked;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            textBox4.Visible = checkBox4.Checked;
            comboBox4.Enabled = checkBox4.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            startButtonEvent(EventArgs.Empty);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            minimizedButtonEvent(EventArgs.Empty);
        }

        protected virtual void startButtonEvent(EventArgs e)
        {
            EventHandler handler = StartButton;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler StartButton;

        protected virtual void minimizedButtonEvent(EventArgs e)
        {
            EventHandler handler = MinimizedStart;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler MinimizedStart;

        private void button4_Click(object sender, EventArgs e)
        {
            minimizeButtonEvent(EventArgs.Empty);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StopButtonEvent(EventArgs.Empty);
        }


        protected virtual void StopButtonEvent(EventArgs e)
        {
            EventHandler handler = StopButton;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler StopButton;

        protected virtual void minimizeButtonEvent(EventArgs e)
        {
            EventHandler handler = Minimize;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        public event EventHandler Minimize;
    }
}
