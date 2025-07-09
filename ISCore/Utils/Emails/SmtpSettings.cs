using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISCore.Utils.Emails
{
    public class SmtpSettings
    {
        public string Host { get; set; }        // اسم أو عنوان الخادم (SMTP Server)
        public int Port { get; set; }           // رقم المنفذ (مثل 587 لـ TLS أو 465 لـ SSL)
        public string Username { get; set; }    // اسم المستخدم (عادة البريد الإلكتروني)
        public string Password { get; set; }    // كلمة المرور
        public string FromEmail { get; set; }   // البريد الإلكتروني المرسل منه
        public bool UseSsl { get; set; }        // تفعيل التشفير SSL/TLS
    }
}
