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
using System.Xml;

namespace Express.CSharp
{
    public partial class ExpressCSharp : Form
    {
        bool isXML = true;
        ConfigurationData configurationData = null;
        string soapAction = string.Empty;

        public ExpressCSharp()
        {
            InitializeComponent();
            configurationData = new ConfigurationData();
        }

        private void btnClearData_Click(object sender, EventArgs e)
        {
            txtRequest.Text = string.Empty;
            txtResponse.Text = string.Empty;
        }

        private void btnSaleRequest_Click(object sender, EventArgs e)
        {
            isXML = true;
            soapAction = string.Empty;

            XNamespace express = "https://transaction.elementexpress.com";

            XDocument doc = new XDocument(new XElement(express + "CreditCardSale",
                                               new XElement(express + "Credentials",
                                                   new XElement(express + "AccountID", configurationData.AccountId),
                                                   new XElement(express + "AccountToken", configurationData.AccountToken),
                                                   new XElement(express + "AcceptorID", configurationData.AcceptorId)
                                                            ),
                                                new XElement(express + "Application",
                                                    new XElement(express + "ApplicationID", configurationData.ApplicationId),
                                                    new XElement(express + "ApplicationVersion", configurationData.ApplicationVersion),
                                                    new XElement(express + "ApplicationName", configurationData.ApplicationName)
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
                                                    new XElement(express + "ExpirationMonth", "12"),
                                                    new XElement(express + "ExpirationYear", "19")
                                                            )
                                                       )
                                         );
            txtRequest.Text = doc.ToString();
        }

        private void btnSendTransaction_Click(object sender, EventArgs e)
        {
            var httpSender = new HttpSender();
            var response = string.Empty;

            if (isXML)
            {
                response = httpSender.Send(txtRequest.Text, configurationData.ExpressXMLEndpoint, string.Empty);
            }
            else
            {
                response = httpSender.Send(txtRequest.Text, configurationData.ExpressSOAPEndpoint, soapAction);
            }

            //load into XmlDocument for parsing
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(response);

            //also allows printing in a better format
            var settings = new XmlWriterSettings(); 
            settings.Indent = true; 
            settings.IndentChars = " "; 
            settings.Encoding = Encoding.UTF8; 
           
            using (var stringWriter = new StringWriter())
            using (var xmlTextWriter = XmlWriter.Create(stringWriter, settings))
            {
                xmlDoc.WriteTo(xmlTextWriter);
                xmlTextWriter.Flush();
                response = stringWriter.GetStringBuilder().ToString();
            }

            txtResponse.Text = response;
        }

        private void btnHealthCheck_Click(object sender, EventArgs e)
        {
            isXML = true;
            soapAction = string.Empty;

            XNamespace express = "https://transaction.elementexpress.com";

            XDocument doc = new XDocument(new XElement(express + "HealthCheck",
                                               new XElement(express + "Credentials",
                                                   new XElement(express + "AccountID", configurationData.AccountId),
                                                   new XElement(express + "AccountToken", configurationData.AccountToken),
                                                   new XElement(express + "AcceptorID", configurationData.AcceptorId)
                                                            ),
                                                new XElement(express + "Application",
                                                    new XElement(express + "ApplicationID", configurationData.ApplicationId),
                                                    new XElement(express + "ApplicationVersion", configurationData.ApplicationVersion),
                                                    new XElement(express + "ApplicationName", configurationData.ApplicationName)
                                                            )
                                                       )
                                         );
            txtRequest.Text = doc.ToString();
        }

        private void btnHealthCheckSOAP_Click(object sender, EventArgs e)
        {
            isXML = false;
            soapAction = "\"https://transaction.elementexpress.com/HealthCheck\"";

            XNamespace xsi = XNamespace.Get("http://www.w3.org/2001/XMLSchema-instance");
            XNamespace xsd = XNamespace.Get("http://www.w3.org/2001/XMLSchema");
            XNamespace soap = XNamespace.Get("http://schemas.xmlsoap.org/soap/envelope/");
            XNamespace express = XNamespace.Get("https://transaction.elementexpress.com");


            XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", null), new XElement(soap + "Envelope", new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                    new XAttribute(XNamespace.Xmlns + "xsd", xsd), new XAttribute(XNamespace.Xmlns + "soap", soap),
                                            new XElement(soap + "Body",
                                                new XElement(express + "HealthCheck",
                                                    new XElement(express + "credentials",
                                                        new XElement(express + "AccountID", configurationData.AccountId),
                                                        new XElement(express + "AccountToken", configurationData.AccountToken),
                                                        new XElement(express + "AcceptorID", configurationData.AcceptorId)
                                                        ),
                                                    new XElement(express + "application",
                                                        new XElement(express + "ApplicationID", configurationData.ApplicationId),
                                                        new XElement(express + "ApplicationVersion", configurationData.ApplicationVersion),
                                                        new XElement(express + "ApplicationName", configurationData.ApplicationName)
                                                        )
                                                    )
                                              )
                                            )
                                            );

            txtRequest.Text = doc.Declaration.ToString() + Environment.NewLine + doc.ToString();
        }

        private void btnSaleRequestSOAP_Click(object sender, EventArgs e)
        {
            isXML = false;
            soapAction = "\"https://transaction.elementexpress.com/CreditCardSale\"";

            XNamespace xsi = XNamespace.Get("http://www.w3.org/2001/XMLSchema-instance");
            XNamespace xsd = XNamespace.Get("http://www.w3.org/2001/XMLSchema");
            XNamespace soap = XNamespace.Get("http://schemas.xmlsoap.org/soap/envelope/");
            XNamespace express = XNamespace.Get("https://transaction.elementexpress.com");


            XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", null), new XElement(soap + "Envelope", new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                    new XAttribute(XNamespace.Xmlns + "xsd", xsd), new XAttribute(XNamespace.Xmlns + "soap", soap),
                                            new XElement(soap + "Body",
                                                new XElement(express + "CreditCardSale",
                                                    new XElement(express + "credentials",
                                                        new XElement(express + "AccountID", configurationData.AccountId),
                                                        new XElement(express + "AccountToken", configurationData.AccountToken),
                                                        new XElement(express + "AcceptorID", configurationData.AcceptorId)
                                                        ),
                                                    new XElement(express + "application",
                                                        new XElement(express + "ApplicationID", configurationData.ApplicationId),
                                                        new XElement(express + "ApplicationVersion", configurationData.ApplicationVersion),
                                                        new XElement(express + "ApplicationName", configurationData.ApplicationName)
                                                        ),
                                                new XElement(express + "terminal",
                                                    new XElement(express + "TerminalID", "01"),
                                                    new XElement(express + "CardholderPresentCode", "Present"),
                                                    new XElement(express + "CardInputCode", "ManualKeyed"),
                                                    new XElement(express + "TerminalCapabilityCode", "MagstripeReader"),
                                                    new XElement(express + "TerminalEnvironmentCode", "LocalAttended"),
                                                    new XElement(express + "CardPresentCode", "Present"),
                                                    new XElement(express + "MotoECICode", "NotUsed"),
                                                    new XElement(express + "CVVPresenceCode", "NotProvided")
                                                            ),
                                                new XElement(express + "card",
                                                    new XElement(express + "CardNumber", "5499990123456781"),
                                                    new XElement(express + "ExpirationMonth", "12"),
                                                    new XElement(express + "ExpirationYear", "19")
                                                            ),
                                                new XElement(express + "transaction",
                                                    new XElement(express + "TransactionAmount", "6.55"),
                                                    new XElement(express + "MarketCode", "Retail")
                                                            )
                                                    )
                                              )
                                            )
                                            );

            txtRequest.Text = doc.Declaration.ToString() + Environment.NewLine + doc.ToString();
        }
    }
}
