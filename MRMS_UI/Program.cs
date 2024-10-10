using MimeKit;
using MRMS_BusinessService;
using MRMS_Data;
using MRMS_Model;
using System;
using MailKit.Net.Smtp;

namespace MRMS_UI
{
    internal class Program
    {
        static void Main(string[] args)
        {

         
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("FromMyNotes", "do-not-reply@frommynotes.com"));
                message.To.Add(new MailboxAddress("User", "user@example.com"));
                message.Subject = "Thank you!";

                message.Body = new TextPart("html")
                {
                    Text = "<h1>Hi, Ka-Movie</h1>" +
                    "<p>Thank you for renting movie.</p>" +
                    "<p><strong>Enjoy!</strong></p>"
                };

                using (var client = new SmtpClient())
                {
                    try
                    {
                        client.Connect("sandbox.smtp.mailtrap.io", 2525, MailKit.Security.SecureSocketOptions.StartTls);

                        client.Authenticate("f1c21756e88f1c", "d39dc555453293");

                        client.Send(message);
                        Console.WriteLine("Email sent successfully through Mailtrap.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error sending email: {ex.Message}");
                    }
                    finally
                    {
                        client.Disconnect(true);
                    }
                }
            

    /*  MovieData getServices = new MovieData();

      var movies = getServices.GetMovies();

      foreach (var item in movies)
      {
          Console.WriteLine($"Code: {item.Code}");
          Console.WriteLine($"Title: {item.Title}");
          Console.WriteLine($"Genre: {item.Genre}");
          Console.WriteLine($"Year: {item.Year}");
          Console.WriteLine($"Price: {item.Price}");
          Console.WriteLine($"IsRented: {item.IsRented}");
          Console.WriteLine();
      }


      SqlCustomerdbData getService = new SqlCustomerdbData();

      var customers = getService.GetCustomers();

      foreach (var item in customers)
      {
          Console.WriteLine($"Username: {item.Username}");
          Console.WriteLine($"Password: {item.Password}");


          Console.WriteLine();
      } */

}
    }






}
       
    
    
    
 

    


