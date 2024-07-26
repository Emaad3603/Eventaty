namespace EventReservation.PL.Helper.QrCodeHelper
{
    public interface IQrCodeGeneratorHelper
    {
        byte[] GenerateQrCode(string text);

    }
}
