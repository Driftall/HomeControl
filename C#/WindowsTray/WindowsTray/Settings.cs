﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WindowsTray
{
    class Settings
    {
        public void saveSetting(string attribute, string data)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            XmlWriter writer = XmlWriter.Create("settings.xml", settings);
            writer.WriteStartDocument();
            writer.WriteComment("This file is generated by the program.");
            writer.WriteStartElement("Settings");
            writer.WriteElementString(attribute, data);
            writer.WriteEndElement();
            writer.WriteEndDocument();

            writer.Flush();
            writer.Close();
        }
    }
}
