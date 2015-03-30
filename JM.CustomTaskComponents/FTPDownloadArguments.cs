using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace JM.CustomTaskComponents
{
    [Serializable()]
    public class FTPDownloadArguments
    {
        private string url = string.Empty;
        private string user = string.Empty;
        private string password = string.Empty;
        private int retryCount = 3;
        private int retryInterval = 5;

        [Description("The URL of the file to be downloaded"),
        CategoryAttribute("Document")]
        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        [Description("FTP authentication: user name"),
        CategoryAttribute("FTP Authentication")]
        public string User
        {
            get { return user; }
            set { user = value; }
        }

        [Description("FTP authentication: password"),
            CategoryAttribute("FTP Authentication"),
            EditorAttribute("Microsoft.BizTalk.Adapter.Framework.ComponentModel.PasswordUITypeEditor, Microsoft.BizTalk.Adapter.Framework, Version=3.0.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", typeof(System.Drawing.Design.UITypeEditor)),
            TypeConverterAttribute("Microsoft.BizTalk.Adapter.Framework.ComponentModel.PasswordTypeConverter, Microsoft.BizTalk.Adapter.Framework, Version=3.0.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        [Description("Retry count"),
        CategoryAttribute("Network Failure")]
        public int RetryCount
        {
            get { return retryCount; }
            set { retryCount = value; }
        }

        [Description("Retry interval (min)"),
        CategoryAttribute("Network Failure")]
        public int RetryInterval
        {
            get { return retryInterval; }
            set { retryInterval = value; }
        }
    }
}
