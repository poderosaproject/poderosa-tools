/*
 * Copyright 2011 The Poderosa Project.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 *
 * $Id: SettingsForm.cs,v 1.2 2011/05/03 17:35:29 kzmi Exp $
 */
using System;
using System.Windows.Forms;
using System.Xml;

namespace MultilanguageDocumentationPlugIn
{
    /// <summary>
    /// Settings form
    /// </summary>
    public partial class SettingsForm : Form
    {
        public string Config { get; internal set; }

        private XmlDocument configDoc;
        private XmlNode configRoot;

        private const string TAG_TARGETLANGUAGE = "targetLanguage";


        public SettingsForm()
        {
            InitializeComponent();
        }

        public SettingsForm(string currentConfig) : this()
        {
            Config = currentConfig;

            configDoc = new XmlDocument();
            configDoc.LoadXml(currentConfig);
            configRoot = configDoc.SelectSingleNode("configuration");

            if (configRoot != null)
            {
                XmlNode node = configRoot.SelectSingleNode(TAG_TARGETLANGUAGE);
                if (node != null)
                {
                    textBoxLanguage.Text = node.InnerText;
                }
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (configRoot != null)
            {
                XmlNode node = configRoot.SelectSingleNode(TAG_TARGETLANGUAGE);
                if (node == null)
                {
                    node = configDoc.CreateNode(XmlNodeType.Element, TAG_TARGETLANGUAGE, null);
                    configRoot.AppendChild(node);
                }
                node.InnerText = textBoxLanguage.Text.Trim();

                Config = configDoc.OuterXml;
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
