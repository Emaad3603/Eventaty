using EventReservation.DAL.Models;
using EventReservation.PL.Settings;
using MailKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net;
using MailKit.Security;
using System.IO;
using static EventReservation.PL.Helper.EmailSettings;


namespace EventReservation.PL.Helper
{
    public class EmailSettings : IMailSettings
    {

        private readonly IOptions<MailSettings> _options;

        public EmailSettings(IOptions<MailSettings> options)
        {
            _options = options;
        }

        public async Task SendEmailAsync(Email email)
        {
            var mail = new MimeMessage();

            try
            {
                mail.Sender = MailboxAddress.Parse(_options.Value.Email);
                mail.Subject = email.Subject;
                mail.To.Add(MailboxAddress.Parse(email.Recipients));

                // Optional: Set From address with display name
               
                    mail.From.Add(new MailboxAddress(_options.Value.DisplayName, _options.Value.Email));
               

                var bodyPart = new TextPart("plain") { Text = email.Body };

                mail.Body = bodyPart;

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_options.Value.Host, _options.Value.Port, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_options.Value.Email, _options.Value.Password);
                await smtp.SendAsync(mail);
            }
            catch (Exception ex)
            {
                // Handle email sending errors (e.g., log the exception)
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }

        public async Task SendEmailWithQRCodeAsync(Email email, string qrCodeData)
        {
            var mail = new MimeMessage();

            try
            {
                mail.Sender = MailboxAddress.Parse(_options.Value.Email);
                mail.Subject = email.Subject;
                mail.To.Add(MailboxAddress.Parse(email.Recipients));

                // Optional: Set From address with display name
              
                mail.From.Add(new MailboxAddress(_options.Value.DisplayName, _options.Value.Email));


                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = $"<b>Here's your QR Code:</b><br><img src=\"data:image/png;base64,{qrCodeData}\">";
                //  bodyBuilder.HtmlBody = $"<img src=~\{qrCodeData}\" alt=\"Girl in a jacket\" width=\"500\" height=\"600\">";
                mail.Body = bodyBuilder.ToMessageBody();
                //var bodyPart = new TextPart("plain") { Text = email.Body };

                //// Create QR code attachment using MemoryStream and StreamContent
                //using (var memoryStream = new MemoryStream(qrCodeData))
                //{
                //    var qrCodePart = new MimePart("image", "png")
                //    {
                //        Content = (IMimeContent)new StreamContent(memoryStream)
                //    };
                //    qrCodePart.ContentDisposition = new ContentDisposition(ContentDisposition.Attachment);


                //    // Add the body and QR code attachment parts
                //    var multipart = new Multipart("mixed");
                //    multipart.Add(bodyPart);
                //    multipart.Add(qrCodePart);

                //    mail.Body = multipart;
                //}

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_options.Value.Host, _options.Value.Port, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_options.Value.Email, _options.Value.Password);
                await smtp.SendAsync(mail);
            }
            catch (Exception ex)
            {
                // Handle email sending errors (e.g., log the exception)
                Console.WriteLine($"Error sending email with QR code: {ex.Message}");
            }
        }

    }
}
