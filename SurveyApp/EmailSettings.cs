using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SurveyApp
{
    public class EmailSettings
    {
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public EmailSettings(string smtpServer, int port, string email, string password)
        {
            SmtpServer = smtpServer;
            Port = port;  
            Email = email;
            Password = password;
        }

    }
}
