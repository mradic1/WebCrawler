using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using System.IO;
using HtmlAgilityPack;
using System.Net.Http;
using System.Data.SqlClient;
using Gecko;
using System.Runtime.InteropServices;
using System.Threading;

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

    public class DataBase:Form1
    {
        private int id;
        private string numId;
        private string name;
        private int price;
        private string link;
        private string date;
        private string imageLink;
        private int category;

        string connetionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\mradi\Source\Repos\WebCrawler\WebCrawler\Database1.mdf;Integrated Security=True";

        #region Data Types
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public string NumId
        {
            get
            {
                return numId;
            }
            set
            {
                numId = value;
            }
        }
        public string ItemName
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public int Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
            }
        }

        public string Link
        {
            get
            {
                return link;
            }
            set
            {
                link = value;
            }
        }

        public string Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
            }
        }

        public string ImageLink
        {
            get
            {
                return imageLink;
            }
            set
            {
                imageLink = value;
            }
        }

        public int Category
        {
            get
            {
                return category;
            }
            set
            {
                category = value;
            }
        }

        #endregion
        public void pushValues()
        {

            SqlConnection cnn;
            SqlCommand cmd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            String sql = "";
            sql = "INSERT INTO Items VALUES ('" + numId + "', '" + name + "', '" + price + "', '" + link + "','" + date + "','" + imageLink + "','" + category + "'); ";
            cnn = new SqlConnection(connetionString);
            cmd = new SqlCommand(sql, cnn);
            cnn.Open();
            adapter.InsertCommand = new SqlCommand(sql, cnn);
            adapter.InsertCommand.ExecuteNonQuery();
            cnn.Close();
        }

        public void deleteValue(int rowId)
        {

            SqlConnection cnn;
            SqlCommand cmd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            String sql = "";
            sql = "DELETE FROM Items WHERE Id='" + rowId + "';";
            cnn = new SqlConnection(connetionString);
            cmd = new SqlCommand(sql, cnn);
            cnn.Open();
            adapter.InsertCommand = new SqlCommand(sql, cnn);
            adapter.InsertCommand.ExecuteNonQuery();
            cnn.Close();
        }

        public void getValues(int rowNum)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\mradi\Source\Repos\WebCrawler\WebCrawler\Database1.mdf;Integrated Security=True"))
            using (SqlCommand command = new SqlCommand("select * from Items where Id = " + rowNum + "", connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int.TryParse(reader["Id"].ToString(), out id);
                        numId = reader["numId"].ToString();
                        name = reader["name"].ToString();
                        int.TryParse(reader["price"].ToString(), out price);
                        link = reader["link"].ToString();
                        date = reader["date"].ToString();
                        imageLink = reader["imageLink"].ToString();
                        int.TryParse(reader["category"].ToString(), out category);
                    }
                }
                connection.Close();
            }
        }

        public int MaxRowNumber()
        {
            SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\mradi\Source\Repos\WebCrawler\WebCrawler\Database1.mdf; Integrated Security = True");
            con.Open();
            SqlCommand comm = new SqlCommand("SELECT COUNT(*) FROM Items", con);
            Int32 count = (Int32)comm.ExecuteScalar();
            con.Close();
            return count;
        }

        public void Reseed()
        {
            SqlConnection cnn;
            SqlCommand cmd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            String sql = "";
            sql = "DBCC CHECKIDENT(Items, RESEED, 0)";
            cnn = new SqlConnection(connetionString);
            cmd = new SqlCommand(sql, cnn);
            cnn.Open();
            adapter.InsertCommand = new SqlCommand(sql, cnn);
            adapter.InsertCommand.ExecuteNonQuery();
            cnn.Close();
        }

    }

    public class UserData
    {
        private int id;
        private string data;


        string connetionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\mradi\Source\Repos\WebCrawler\WebCrawler\Database1.mdf;Integrated Security=True";
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public string Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }
        public void pushValues(int id)
        {

            SqlConnection cnn;
            SqlCommand cmd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            String sql = "";
            sql = "INSERT INTO Data VALUES ('"+id+"','" + data + "'); ";
            cnn = new SqlConnection(connetionString);
            cmd = new SqlCommand(sql, cnn);
            cnn.Open();
            adapter.InsertCommand = new SqlCommand(sql, cnn);
            adapter.InsertCommand.ExecuteNonQuery();
            cnn.Close();
        }

        public void deleteValue(int rowId)
        {

            SqlConnection cnn;
            SqlCommand cmd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            String sql = "";
            sql = "DELETE FROM Data WHERE Id='" + rowId + "';";
            cnn = new SqlConnection(connetionString);
            cmd = new SqlCommand(sql, cnn);
            cnn.Open();
            adapter.InsertCommand = new SqlCommand(sql, cnn);
            adapter.InsertCommand.ExecuteNonQuery();
            cnn.Close();
        }

        public void getValues(int rowNum)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\mradi\Source\Repos\WebCrawler\WebCrawler\Database1.mdf;Integrated Security=True"))
            using (SqlCommand command = new SqlCommand("select * from Data where Id = " + rowNum + "", connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int.TryParse(reader["Id"].ToString(), out id);
                        data = reader["data"].ToString();
                        
                    }
                }
                connection.Close();
            }
        }

        public void Reseed()
        {
            SqlConnection cnn;
            SqlCommand cmd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            String sql = "";
            sql = "DBCC CHECKIDENT(Items, RESEED, 0)";
            cnn = new SqlConnection(connetionString);
            cmd = new SqlCommand(sql, cnn);
            cnn.Open();
            adapter.InsertCommand = new SqlCommand(sql, cnn);
            adapter.InsertCommand.ExecuteNonQuery();
            cnn.Close();
        }

    }
    
    public class CrawlerHandler
    {
        public  GeckoWebBrowser browser = new GeckoWebBrowser();  //MORAT CES SA ARRAYEM
        int numOfEx = 0;
     
        #region //Data methods

        string url;
        List<string> numId = new List<string>();
        List<string> name = new List<string>();
        List<float> price = new List<float>();
        List<string> link = new List<string>();
        List<string> date = new List<string>();
        List<string> imageLink = new List<string>();
        public string Url
        {   
            get { return url; }
            set { url = value; }
        }
        public List<string> NumId
        {
            get { return numId; }
        }
        public List<string> Name
        {
            get { return name; }
        }
        public List<float> Price
        {
            get { return price; }
        }
        public List<string> Link
        {
            get { return link; }
        }
        public List<string> Date
        {
            get { return date; }
        }

        public List<string> ImageLink
        {
            get { return imageLink; }
        }
        #endregion

        public void pullData(string _url)
        {
            url = _url;
            pullData();
        }
        public void pullData()
        {
            browser.Navigate(url);
        }

        private void storeInDatabase()
        {

        }
        private void fetchData()
        {
            string innerHtml = "";
            GeckoHtmlElement element = null;
            var geckoDomElement = browser.Document.DocumentElement;
            if (geckoDomElement is GeckoHtmlElement)
            {
                element = (GeckoHtmlElement)geckoDomElement;
                innerHtml = element.OuterHtml;
            }

            var htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(innerHtml);

            try
            {
                var productList = htmlDoc.DocumentNode.Descendants("ul").Where(node => node.GetAttributeValue("class", "").Equals("EntityList-items")).ToList();
                var Products = productList[1].Descendants("li").Where(node => node.GetAttributeValue("data-href", "").Contains("oglas")).ToList();

                foreach (var Item in Products) // Pazi kako ces odraditi jer ga vise puta poziva (probaj mozda sa array)
                {
                    int prices;
                    string priceStr;

                    numId.Add(Item.Descendants("a").FirstOrDefault().GetAttributeValue("name", ""));
                    name.Add(Item.Descendants("a").Where(node => node.GetAttributeValue("class", "").Equals("link")).FirstOrDefault().InnerText);

                    priceStr = (Item.Descendants("strong").Where(node => node.GetAttributeValue("class", "").Equals("price price--hrk")).FirstOrDefault().InnerText.Trim('\r', '\n', '\t', '.', ' ').Replace("&nbsp;kn", ""));
                    priceStr = priceStr.Replace(".","");
                    int.TryParse(priceStr, out prices);
                    price.Add((float)prices);

 
                    link.Add(Item.GetAttributeValue("data-href", ""));
                    date.Add(Item.Descendants("time").Where(node => node.GetAttributeValue("pubdate", "").Equals("pubdate")).FirstOrDefault().InnerText);
                    imageLink.Add(Item.Descendants("img").FirstOrDefault().GetAttributeValue("data-src","NECE").TrimStart('/'));
                }
            }
            catch (Exception)
            {
                numOfEx++;
                if (numOfEx > 1)
                {
                    MessageBox.Show("HTML Exception");
                    numOfEx = 0;

                }
            }
            OnDataLoaded(EventArgs.Empty);
        }
        private void browser_Loaded(object sender, EventArgs e)
        {
           fetchData();    
        }
        public void Init()
        {
            Xpcom.Initialize("Firefox");
            browser.DocumentCompleted += new EventHandler<Gecko.Events.GeckoDocumentCompletedEventArgs>(browser_Loaded);
        }

        protected virtual void OnDataLoaded(EventArgs e)
        {
            EventHandler handler = DataLoaded;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler DataLoaded;
    }

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

            for(int i = 0; i < handler.NumId.Count;i++)
            {
                data.NumId = handler.NumId[i];
                data.Name = handler.Name[i];
                data.Price=(int)handler.Price[i];
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
            for(int i = 0;i< num; i++)
            {
                data.getValues(i);
                if(data.Category == 1)
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


