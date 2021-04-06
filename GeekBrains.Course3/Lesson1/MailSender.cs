using System;
using System.Diagnostics;
using System.Net.Mail;

namespace Lesson1
{
    public class MailSender
    {
        private readonly string _receiverMail;
        private readonly string _receiverName;
        private readonly string _subject;
        private readonly string _message;

        public bool Send()
        {
            var sender = new MailAddress(MailSettings.Mail, MailSettings.Name);
            var receiver = new MailAddress(_receiverMail, _receiverName);

            using (var message = new MailMessage(sender, receiver) { Subject = _subject, Body = _message })
            {
                try
                {
                    MailSettings.Prepared.Send(message);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return false;
                }

                return true;
            }
        }

        public MailSender(string receiverMail, string receiverName, string subject, string message)
        {
            _receiverMail = receiverMail;
            _receiverName = receiverName;
            _subject = subject;
            _message = message;
        }

    }
}