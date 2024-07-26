using EventReservation.DAL.Models;

namespace EventReservation.PL.Helper
{
    public interface IMailSettings
    {
        public Task SendEmailAsync(Email email );

        public Task SendEmailWithQRCodeAsync(Email email, string qrCodeData);
    }
}
