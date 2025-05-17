namespace TINEXPRESSAPIS.DownloadAPI.Helpers
{
    using System.Drawing;
    using System.Drawing.Imaging;
    using ZXing;
    using ZXing.Rendering;

    public static class BarcodeHelper
    {
        public static byte[] GenerateQrCode(string content, int size = 150)
        {
            try
            {
                var writer = new BarcodeWriterPixelData
                {
                    Format = BarcodeFormat.QR_CODE,
                    Options = new ZXing.Common.EncodingOptions
                    {
                        Width = size,
                        Height = size,
                        Margin = 0 // 👈 Removes white padding (quiet zone)
                    }
                };
                return ToByteArray(writer.Write(content));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public static byte[] GenerateBarcode(string content, int width = 500, int height = 150)
        {
            try
            {
                var writer = new BarcodeWriterPixelData
                {
                    Format = BarcodeFormat.CODE_128,
                    Options = new ZXing.Common.EncodingOptions
                    {
                        Width = width,
                        Height = height,
                        Margin = 0
                    }
                };
                return ToByteArray(writer.Write(content));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        private static byte[] ToByteArray(PixelData pixelData)
        {
            try
            {
                using var bitmap = new Bitmap(pixelData.Width, pixelData.Height, PixelFormat.Format32bppRgb);
                var data = bitmap.LockBits(
                    new Rectangle(0, 0, pixelData.Width, pixelData.Height),
                    ImageLockMode.WriteOnly,
                    PixelFormat.Format32bppRgb);

                System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, data.Scan0, pixelData.Pixels.Length);
                bitmap.UnlockBits(data);

                using var ms = new MemoryStream();
                bitmap.Save(ms, ImageFormat.Png);
                return ms.ToArray();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
