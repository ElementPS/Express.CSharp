using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Configuration;

namespace Express.CSharp
{
    public partial class ExpressCSharp : Form
    {
        public ExpressCSharp()
        {
            InitializeComponent();
        }

        private void btnClearData_Click(object sender, EventArgs e)
        {
            txtRequest.Text = string.Empty;
            txtResponse.Text = string.Empty;
        }

        private void btnSaleRequest_Click(object sender, EventArgs e)
        {
            var accountId = ConfigurationManager.AppSettings["AccountID"];
            var accountToken = ConfigurationManager.AppSettings["AccountToken"];
            var acceptorId = ConfigurationManager.AppSettings["AcceptorID"];
            var applicationId = ConfigurationManager.AppSettings["ApplicationID"];
            var applicationVersion = ConfigurationManager.AppSettings["ApplicationVersion"];
            var applicationName = ConfigurationManager.AppSettings["ApplicationName"];

            XNamespace express = "https://transaction.elementexpress.com";

            XDocument doc = new XDocument(new XElement(express + "CreditCardSale",
                                               new XElement(express + "Credentials",
                                                   new XElement(express + "AccountID", accountId),
                                                   new XElement(express + "AccountToken", accountToken),
                                                   new XElement(express + "AcceptorID", acceptorId)
                                                            ),
                                                new XElement(express + "Application",
                                                    new XElement(express + "ApplicationID", applicationId),
                                                    new XElement(express + "ApplicationVersion", applicationVersion),
                                                    new XElement(express + "ApplicationName", applicationName)
                                                            ),
                                                new XElement(express + "Terminal",
                                                    new XElement(express + "TerminalID", "01"),
                                                    new XElement(express + "CardholderPresentCode", "2"),
                                                    new XElement(express + "CardInputCode", "5"),
                                                    new XElement(express + "TerminalCapabilityCode", "3"),
                                                    new XElement(express + "TerminalEnvironmentCode", "2"),
                                                    new XElement(express + "CardPresentCode", "2"),
                                                    new XElement(express + "MotoECICode", "1"),
                                                    new XElement(express + "CVVPresenceCode", "1")
                                                            ),
                                                new XElement(express + "Transaction",
                                                    new XElement(express + "TransactionAmount", "6.55"),
                                                    new XElement(express + "MarketCode", "7")
                                                            ),
                                                new XElement(express + "Card",
                                                    new XElement(express + "CardNumber", "5499990123456781"),
                                                    new XElement(express + "ExpirationMonth", "09"),
                                                    new XElement(express + "ExpirationYear", "08")
                                                            )
                                                       )
                                         );
            txtRequest.Text = doc.ToString();
        }

        private void btnSendTransaction_Click(object sender, EventArgs e)
        {
            //https://certtransaction.elementexpress.com/express.asmx -- soap

            var request = (HttpWebRequest)WebRequest.Create("https://certtransaction.elementexpress.com/");

            var data = Encoding.ASCII.GetBytes(txtRequest.Text);

            request.Method = "POST";
            request.ContentType = "text/xml";
            request.ContentLength = txtRequest.Text.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            txtResponse.Text = responseString;
        }

        private void btnHealthCheck_Click(object sender, EventArgs e)
        {
            var accountId = ConfigurationManager.AppSettings["AccountID"];
            var accountToken = ConfigurationManager.AppSettings["AccountToken"];
            var acceptorId = ConfigurationManager.AppSettings["AcceptorID"];
            var applicationId = ConfigurationManager.AppSettings["ApplicationID"];
            var applicationVersion = ConfigurationManager.AppSettings["ApplicationVersion"];
            var applicationName = ConfigurationManager.AppSettings["ApplicationName"];

            XNamespace express = "https://transaction.elementexpress.com";

            XDocument doc = new XDocument(new XElement(express + "HealthCheck",
                                               new XElement(express + "Credentials",
                                                   new XElement(express + "AccountID", accountId),
                                                   new XElement(express + "AccountToken", accountToken),
                                                   new XElement(express + "AcceptorID", acceptorId)
                                                            ),
                                                new XElement(express + "Application",
                                                    new XElement(express + "ApplicationID", applicationId),
                                                    new XElement(express + "ApplicationVersion", applicationVersion),
                                                    new XElement(express + "ApplicationName", applicationName)
                                                            )
                                                       )
                                         );
            txtRequest.Text = doc.ToString();
        }
    }
}
