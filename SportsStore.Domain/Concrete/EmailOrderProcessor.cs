using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System.Net;
using System.Net.Mail;


namespace SportsStore.Domain.Concrete
{
    public class EmailSettings 
    {
        public string MailToAdress = "abc@o2.pl";
        public string MailFromAddress = "abc@o2.pl";
        public bool UseSsl = true;
        public string Username = "abc";
        public string Password = "abc";
        public string ServerName = "poczta.o2.pl";
        public int ServerPort = 587;
        public bool WriteAsFile = false;
        public string FileLocation = "";
    }

    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;
        public EmailOrderProcessor(EmailSettings settings) 
        {
            emailSettings = settings;
        }

        public void ProcessOrder(Cart cart, ShippingDetails shippingInfo)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials =
                    new NetworkCredential(emailSettings.Username, emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                    .AppendLine("Nowe zamówienie")
                    .AppendLine("_______________")
                    .AppendLine("Produkty: ");

                foreach (var line in cart.Lines)
                {
                    var subtotal = line.SchoesModel.Price * line.Quantity;
                    body.AppendFormat("{0} x {1} (wartość: {2:c}", 
                        line.Quantity, $"{line.SchoesModel.Brand.BrandName} {line.SchoesModel.SchoesModelName}", subtotal);
                }

                body.AppendFormat("Wartość całkowita: {0:c}",
                    cart.ComputeTotalValue())
                    .AppendLine("__________")
                    .AppendLine("Wysyłka Dla")
                    .AppendLine(shippingInfo.Name)
                    .AppendLine(shippingInfo.Line1)
                    .AppendLine(shippingInfo.Line2 ?? "")
                    .AppendLine(shippingInfo.Line3 ?? "")
                    .AppendLine(shippingInfo.City)
                    .AppendLine(shippingInfo.State ?? "")
                    .AppendLine(shippingInfo.Zip)
                    .AppendLine(shippingInfo.Country)
                    .AppendLine("__________")
                    .AppendFormat("Pakowanie prezentu: {0}", (shippingInfo.GiftWrap ? "Tak" : "Nie"));

                MailMessage mailMessage = new MailMessage(
                    emailSettings.MailFromAddress,
                    emailSettings.MailToAdress,
                    "Otrzymano nowe zamówienie!",
                    body.ToString());

                smtpClient.Send(mailMessage);
            }
        }
    }
}
