using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace GDF_DATA
{
    /// <summary>
    /// 메일 보내기
    /// Gmail
    /// </summary>
    class MailManager
    {
        private MailAddress _sendAddress = null;
        private MailAddress _toAddress = null;
        private string _sendPassword = "";

        public MailManager(string sendMail, string sendPassword, string toMail)
        {
            _sendAddress = new MailAddress(sendMail, "GDF", System.Text.Encoding.UTF8);
            _toAddress = new MailAddress(toMail);
            _sendPassword = sendPassword;
        }

        public void SetMailInfomation(string sendMail, string sendPassword, string toMail)
        {
            _sendAddress = new MailAddress(sendMail, "GDF", System.Text.Encoding.UTF8);
            _toAddress = new MailAddress(toMail);
            _sendPassword = sendPassword;
        }

        public void SendEmail(string subject, string body)
        {
            SmtpClient smtp = null;
            MailMessage message = null;

            try
            {
                smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(_sendAddress.Address, _sendPassword),
                    Timeout = 20000
                };
                message = new MailMessage(_sendAddress, _toAddress)
                {
                    Subject = subject,
                    Body = body,
                    BodyEncoding = System.Text.Encoding.UTF8,
                    SubjectEncoding = System.Text.Encoding.UTF8

                };
                smtp.Send(message);
                message.Dispose();
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }
        }

    }
}
