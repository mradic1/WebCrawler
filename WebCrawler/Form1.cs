using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using System.Runtime.InteropServices;


namespace WebCrawler
{
    public partial class Form1 : Form
    {
        #region Mouse move handler
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        #endregion

        #region Behaviour Methods
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        #endregion

        #region Misc
        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("WebCrawler by Miloš Radić \n m.radic225@gmail.com");
        }

        void init()
        {
            handler.Init();
            handler.DataLoaded += new EventHandler(dataLoaded);
            settingsPageContainer1.buttonClicked += new EventHandler(SettingsSaveButton);
            crawlerContainer1.StartButton += new EventHandler(StartButton);
            crawlerContainer1.MinimizedStart += new EventHandler(MinimizedButton);
            crawlerContainer1.Minimize += new EventHandler(MinimizeButton);
            crawlerContainer1.StopButton += new EventHandler(StopButton);

            Icon icon = new Icon("Icon.ico");
            this.Icon = icon;

            panel3.Top = button2.Top;
            panel5.Visible = true;
            crawlerContainer1.Visible = false;
            panel5.BringToFront();
        }

        void homeButton()
        {
            panel3.Top = button2.Top;
            panel5.Visible = true;
            crawlerContainer1.Visible = false;
            panel5.BringToFront();
            settingsPageContainer1.Visible = false;

        }

        void crawlButton()
        {
            panel3.Top = button3.Top;
            panel5.Visible = false;
            crawlerContainer1.BringToFront();
            crawlerContainer1.Visible = true;
            settingsPageContainer1.Visible = false;
        }

        void settingsButton()
        {
            panel3.Top = button4.Top;
            settingsPageContainer1.Visible = true;
            settingsPageContainer1.BringToFront();
            crawlerContainer1.Visible = false;
            panel5.Visible = false;
        }
        #endregion

        CrawlerHandler handler = new CrawlerHandler();
        LogicHandler logic = new LogicHandler();


        public Form1()
        {
            InitializeComponent();
            init();
        }

        void dataLoaded(object sender, EventArgs e)
        {
            //Yoo
        }

        

        private void button2_Click(object sender, EventArgs e) // HOME
        {
            homeButton();
        }

        private void button3_Click(object sender, EventArgs e)// CRAWL
        {
            crawlButton();
        }

        private void button4_Click(object sender, EventArgs e)// SETTINGS
        {
            settingsButton();

        }

        private void SettingsSaveButton(object o, EventArgs e)
        {

        }

        private void StartButton(object o, EventArgs e)
        {
            label1.Text = "Started";
            label1.ForeColor = Color.FromArgb(90,178,104);
            logic.startFirstLink("https://www.njuskalo.hr/ps3-konzole?locationIds=1160&price%5Bmax%5D=1000");
        }

        private void MinimizedButton(object o, EventArgs e)
        {

            logic.mailTo = "aaabbccc";   
        }

        private void MinimizeButton(object o, EventArgs e)
        {
            UserData data = new UserData();
            for (int i = 1; i <= 20; i++)
            {
                data.Data = i.ToString();
                data.pushValues(i);
            }

        }

        private void StopButton(object o, EventArgs e)
        {
            label1.Text = "Stopped";
            label1.ForeColor = Color.DarkRed;
        }


    }

}


