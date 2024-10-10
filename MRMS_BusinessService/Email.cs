using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MimeKit;
using MRMS_Model;
using MailKit.Net.Smtp;
using System.Security.Cryptography.X509Certificates;
using MRMS_Data;
namespace MRMS_BusinessService
{
    public class Email
    {
        public bool SendCustomerWelcomeEmail(string customerName)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("ADMIN", "admin_mrmsemail.com")); // Admin's email
            message.To.Add(new MailboxAddress(customerName, customerName + ".com")); 
            message.Subject = "Welcome to Ka-Movie!";

            message.Body = new TextPart("html")
            {
                Text = "<h1>Welcome to Ka-Movie!</h1>" +
                       "<p>Thank you for registering with us.</p>" +
                       "<p><strong>Enjoy renting movies with us!</strong></p>"
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect("sandbox.smtp.mailtrap.io", 2525, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate("f1c21756e88f1c", "d39dc555453293"); // Replace with your Mailtrap credentials
                    client.Send(message);
                    client.Disconnect(true);
                    return true; // Email sent successfully
                }
                catch (Exception ex)
                {
                    
                    Console.WriteLine($"Error sending email: {ex.Message}");
                    return false; // Email sending failed
                }
            }
        }

      

        public bool SendRentalNotification(string customerEmail, int movieCode)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Movie Rental Service", "admin_mrmsemail.com"));
            message.To.Add(new MailboxAddress("Customer", customerEmail));
            message.Subject = "Movie Rental Confirmation";

            message.Body = new TextPart("html")
            {
                Text = $"<h1>Thank you for renting!</h1>" +
                       $"<p>You have successfully rented the movie with code: {movieCode}.</p>" +
                       "<p>We hope you enjoy it!</p>"
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect("sandbox.smtp.mailtrap.io", 2525, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate("f1c21756e88f1c", "d39dc555453293");
                    client.Send(message);
                    Console.WriteLine("Rental confirmation email sent successfully.");
                    return true; // Email sent successfully
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending email: {ex.Message}");
                    return false; // Email sending failed
                }
                finally
                {
                    client.Disconnect(true);
                }
            }
        }







    }
}

