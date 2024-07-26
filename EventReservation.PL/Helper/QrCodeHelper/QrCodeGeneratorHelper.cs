using QRCoder;
using System.Drawing;

namespace EventReservation.PL.Helper.QrCodeHelper
{
    public class QrCodeGeneratorHelper : IQrCodeGeneratorHelper
    {
        public byte[] GenerateQrCode(string text)
        {
            byte[] QrCode = new byte[0];
            if (!string.IsNullOrEmpty(text))
            {
                QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
                QRCodeData data = qRCodeGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
                BitmapByteQRCode bitmap = new BitmapByteQRCode(data);
                QrCode = bitmap.GetGraphic(20);

            }
            return QrCode;



        }
    }
}
