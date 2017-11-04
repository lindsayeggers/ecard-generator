using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Project.Web.DeliveryProviders
{
    public class EmailDeliveryService : IDeliveryService
    {
        public void Send(string recipient, string filePath)
        {
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress("trollingwithtrolls@gmail.com");
            msg.To.Add(recipient);
            msg.Subject = "Someone has sent you a troll! " + DateTime.Now.ToString();
            msg.Body = filePath;
            var attachment = new Attachment(filePath);
            msg.Attachments.Add(attachment);
            //msg.Attachments.Add()
              

            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            #region Credentials
            client.Credentials = new NetworkCredential("trollingwithtrolls@gmail.com", "Ilovetrolls");
            #endregion
            client.Timeout = 20000;
            try
            {
                client.Send(msg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                msg.Dispose();
            }
        }
    }
}