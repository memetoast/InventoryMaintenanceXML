using InventoryMaintenance.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace InventoryMaintenance
{
    public static class InvItemDB
    {
        private const string Path = @"..\..\InventoryItems.xml";

        public static List<InvItem> GetItems()
        {
            // create the list
            List<InvItem> items = new List<InvItem>();
            //creates an XmlReaderSettings object that ignores white space and comments
            //create an XmlReader object for the InventoryItems.xml file that’s included in the project
            // Add code here to read the XML file into the List<InvItem> object.
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;
            XmlReader xmlIn = XmlReader.Create(Path, settings);
            if (xmlIn.ReadToDescendant("InvItem"))
            {
                do
                {
                    InvItem product = new InvItem();
                    product.ItemNo = xmlIn.ReadElementContentAsInt();
                    product.Description = xmlIn.ReadElementContentAsString();
                    product.Price = xmlIn.ReadElementContentAsDecimal();
                    items.Add(product);
                }
                while (xmlIn.ReadToNextSibling("InvItem"));
            }
            xmlIn.Close();
            return items;
        }

        public static void SaveItems(List<InvItem> items)
        {
            // Add code here to write the List<InvItems> object to an XML file.
            //creates an XmlWriterSettings object that indents each element four spaces
            //create an XmlWriter object for the InventoryItems.xml file that uses the writer settings
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = ("    ");

            //writes an XML declaration line and a start tag for the Items element
            XmlWriter xmlOut = XmlWriter.Create(Path, settings);
            xmlOut.WriteStartDocument();
            xmlOut.WriteStartElement("InvItem");
            foreach(InvItem product in items)
            {
                xmlOut.WriteStartElement("InvItem");
                xmlOut.WriteAttributeString("ItemNo", Convert.ToString(product.ItemNo));
                xmlOut.WriteElementString("Description", product.Description);
                xmlOut.WriteElementString("Price", Convert.ToString(product.Price));
            }
            xmlOut.WriteEndElement();
            xmlOut.Close();

        }
    }
}
