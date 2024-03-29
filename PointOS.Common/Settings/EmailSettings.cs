﻿using System.Security;

namespace PointOS.Common.Settings
{
    public class EmailSettings
    {
        public string SmtpHostName { get; set; }
        public int Port { get; set; }
        public string SenderAddress { get; set; }
        public object SenderName { get; set; }
        public string UserName { get; set; }
        public SecureString Password { get; set; }
    }
}