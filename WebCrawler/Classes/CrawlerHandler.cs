using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gecko;

namespace WebCrawler
{
    public class CrawlerHandler:Form1
    {
        public GeckoWebBrowser browser = new GeckoWebBrowser();  //MORAT CES SA ARRAYEM
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
                    priceStr = priceStr.Replace(".", "");
                    int.TryParse(priceStr, out prices);
                    price.Add((float)prices);


                    link.Add(Item.GetAttributeValue("data-href", ""));
                    date.Add(Item.Descendants("time").Where(node => node.GetAttributeValue("pubdate", "").Equals("pubdate")).FirstOrDefault().InnerText);
                    imageLink.Add(Item.Descendants("img").FirstOrDefault().GetAttributeValue("data-src", "NECE").TrimStart('/'));
                }
            }
            catch (Exception)
            {
                numOfEx++;
                if (numOfEx > 1)
                {
                    System.Windows.Forms.MessageBox.Show("HTML Exception");
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
}
