using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.Drawing;

namespace WebCrawler
{
    public class LogicHandler
    {

        public static void Email(string htmlString, string email)
        {

            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress("crawlerMail8@gmail.com");
            message.To.Add(new MailAddress(email));
            message.Subject = "Found new item";
            message.IsBodyHtml = true; //to make message body as html  
            message.Body = htmlString;
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com"; //for gmail host  
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("crawlerMail8@gmail.com", "crawlerSender123");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);

        }
        #region Icon stuff

        NotifyIcon notificationIcon = new NotifyIcon();
        Icon icon = new Icon("Icon.ico");
        public void showIcon()
        {
            createIcon();
            notificationIcon.Visible = true;

        }
        public void hideIcon()
        {
            notificationIcon.Visible = false;
            notificationIcon.Dispose();
        }

        public void createIcon()
        {
            notificationIcon.Icon = icon;
            ContextMenu menu = new ContextMenu();
            MenuItem quitItem = new MenuItem("Quit");
            MenuItem showItem = new MenuItem("Show");
            menu.MenuItems.Add(showItem);
            menu.MenuItems.Add(quitItem);


            notificationIcon.ContextMenu = menu;

            quitItem.Click += QuitItem_Click;
            showItem.Click += ShowItem_Click;
        }

        private void ShowItem_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            if (!form.Visible)
            {
                form.Visible = true;
                form.ShowInTaskbar = true;
            }
            hideIcon();
        }

        private void QuitItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to exit the program?", "Quit WebCrawler", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                hideIcon();
                Application.Exit();
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }

        #endregion

        #region Timer stuff

        System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer timer2 = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer timer3 = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer timer4 = new System.Windows.Forms.Timer();

        void initTimers()
        {

            timer2.Tick += Timer2_Tick;
            timer3.Tick += Timer3_Tick;
            timer4.Tick += Timer4_Tick;
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            handler1.pullData(firstLink);
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {

        }

        private void Timer3_Tick(object sender, EventArgs e)
        {

        }

        private void Timer4_Tick(object sender, EventArgs e)
        {

        }

        public int timer1Interval
        {
            get { return timer1.Interval; }
            set { timer1.Interval = value; }
        }

        public int timer2Interval
        {
            get { return timer2.Interval; }
            set { timer2.Interval = value; }
        }

        public int timer3Interval
        {
            get { return timer3.Interval; }
            set { timer3.Interval = value; }
        }

        public int timer4Interval
        {
            get { return timer4.Interval; }
            set { timer4.Interval = value; }
        }

        #endregion

        #region Misc

        public void exitFunction()
        {
            notificationIcon.Visible = false;
            notificationIcon.Dispose();
            Application.Exit();
        }

        public int miliSecParse(ComboBox box)
        {
            int result = 0;
            string buffer = box.Text;
            if (buffer.Contains("min"))
            {
                buffer = buffer.Replace(" min", "");
                int.TryParse(buffer, out result);
                result = (result * 60 * 1000);
                return result;
            }
            else if (buffer.Contains("h"))
            {
                buffer = buffer.Replace(" h", "");
                int.TryParse(buffer, out result);
                result = (result * 60 * 60 * 1000);
                return result;
            }
            else
            {
                return 0;
            }

        }
        #endregion

        #region Main Logic

        CrawlerHandler handler1 = new CrawlerHandler();

        private string firstLink;
        public void startFirstLink(string url)
        {

            firstLink = url;
            handler1.DataLoaded += Handler1_DataLoaded;
            timer1Interval = 10000;
            timer1.Tick += Timer1_Tick;
            timer1.Start();


        }

        private void Handler1_DataLoaded(object sender, EventArgs e)
        {
            if (checkFirstLoad(handler1))
            {
                deleteCategory(1);
                fillDataBase(handler1);
            }
            else
            {
                int index = checkForMatch(handler1);
                if (index > -1)
                {
                    DataBase baza = new DataBase();
                    baza.NumId = handler1.NumId[index];
                    baza.Name = handler1.Name[index];
                    baza.Price = (int)handler1.Price[index];
                    baza.Link = "www.njuskalo.hr" + handler1.Link[index];
                    baza.Date = handler1.Date[index];
                    baza.ImageLink = "http://" + handler1.ImageLink[index];
                    baza.Category = 1;
                    baza.pushValues();
                    LogicHandler.Email("New item found", "");
                }
            }
        }

        int checkForMatch(CrawlerHandler handler)
        {
            DataBase data = new DataBase();
            int count = handler.NumId.Count;
            int basecount = data.MaxRowNumber();
            for (int i = 0; i < count; i++)
            {
                for (int j = 1; j <= basecount; j++)
                {
                    data.getValues(j);
                    if (data.Category == 1)
                    {
                        if (data.NumId == handler.NumId[i])
                        {
                            break;
                        }
                        else
                        {
                            return i;
                        }
                    }
                }
            }
            return -1;
        }

        public bool checkFirstLoad(CrawlerHandler handler)
        {
            DataBase data = new DataBase();
            int counter = 0;

            for (int i = 0; i < handler.NumId.Count; i++)
            {
                for (int j = 1; j <= data.MaxRowNumber(); j++)
                {
                    data.getValues(j);
                    if (data.Category == 1)
                    {
                        if (data.NumId == handler.NumId[i])
                        {
                            counter++;
                        }
                    }
                }
            }

            if (counter > 5)
                return false;
            else
                return true;

        }

        public void fillDataBase(CrawlerHandler handler)
        {
            DataBase data = new DataBase();

            for (int i = 0; i < handler.NumId.Count; i++)
            {
                data.NumId = handler.NumId[i];
                data.Name = handler.Name[i];
                data.Price = (int)handler.Price[i];
                data.Link = handler.Link[i];
                data.Date = handler.Date[i];
                data.ImageLink = handler.ImageLink[i];
                data.Category = 1;
            }

        }

        public void deleteCategory(int cat)
        {
            DataBase data = new DataBase();
            int num = data.MaxRowNumber();
            for (int i = 0; i < num; i++)
            {
                data.getValues(i);
                if (data.Category == 1)
                {
                    data.deleteValue(i);
                }
            }
        }

        #endregion

        #region UserDataFunctions

        public List<string> getRefreshRates()
        {
            List<string> list = new List<string>();
            UserData data = new UserData();
            data.getValues(1);
            list.Add(data.Data);

            data.getValues(2);
            list.Add(data.Data);

            data.getValues(3);
            list.Add(data.Data);

            data.getValues(4);
            list.Add(data.Data);

            return list;
        }

        public List<string> getLinks()
        {
            List<string> list = new List<string>();
            UserData data = new UserData();
            data.getValues(5);
            list.Add(data.Data);

            data.getValues(6);
            list.Add(data.Data);

            data.getValues(7);
            list.Add(data.Data);

            data.getValues(8);
            list.Add(data.Data);

            return list;

        }

        public List<string> getLinkNames()
        {
            List<string> list = new List<string>();
            UserData data = new UserData();
            data.getValues(9);
            list.Add(data.Data);

            data.getValues(10);
            list.Add(data.Data);

            data.getValues(11);
            list.Add(data.Data);

            data.getValues(12);
            list.Add(data.Data);

            return list;
        }

        public List<string> getCheckStatus()
        {
            List<string> list = new List<string>();
            UserData data = new UserData();
            data.getValues(13);
            list.Add(data.Data);

            data.getValues(14);
            list.Add(data.Data);

            data.getValues(15);
            list.Add(data.Data);

            data.getValues(16);
            list.Add(data.Data);

            return list;
        }

        public string mailTo
        {

            get
            {
                UserData data = new UserData();
                data.getValues(17);
                return data.Data;
            }
            set
            {
                UserData data = new UserData();
                data.Data = value;
                data.pushValues(17);
            }
        }

        #endregion

    }
}
