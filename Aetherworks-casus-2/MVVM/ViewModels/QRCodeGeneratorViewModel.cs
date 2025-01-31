using Aetherworks_casus_2.MVVM.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.PlatformConfiguration;
using SkiaSharp;
using System.Runtime.InteropServices;
using ZXing;

namespace Aetherworks_casus_2.MVVM.ViewModels
{
    public partial class QRCodeGeneratorViewModel : ObservableObject
    {
        [ObservableProperty]
        private string id;

        [ObservableProperty]
        private string type;

        [ObservableProperty]
        private ImageSource qrCodeImage;

        public QRCodeGeneratorViewModel(string id, string type)
        {
            Id = id;
            Type = type;
            GenerateQRCode();
        }

        private string GenerateQRCode()
        {
            if (!string.IsNullOrWhiteSpace(Id))
            {
                var writer = new BarcodeWriterPixelData
                {
                    Format = ZXing.BarcodeFormat.QR_CODE,
                    Options = new ZXing.Common.EncodingOptions
                    {
                        Height = 300,
                        Width = 300,
                        Margin = 2
                    }
                };

                // Use JSON format for QR data
                var qrData = $"{Id}";
                var result = writer.Write(qrData);
                QrCodeImage = PixelDataToImageSource(result);                
            }

            return Id;
        }

        private ImageSource PixelDataToImageSource(ZXing.Rendering.PixelData pixelData)
        {
            var memoryStream = new MemoryStream();
            var info = new SKImageInfo(pixelData.Width, pixelData.Height, SKColorType.Bgra8888);

            using var skBitmap = new SKBitmap(info);
            skBitmap.InstallPixels(info, ByteArrayToNint(pixelData.Pixels));

            using var skImage = SKImage.FromBitmap(skBitmap);
            using var skData = skImage.Encode(SKEncodedImageFormat.Png, 100);

            skData.SaveTo(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return ImageSource.FromStream(() => memoryStream);
        }

        private nint ByteArrayToNint(byte[] byteArray)
        {
            var unmanagedPointer = Marshal.AllocHGlobal(byteArray.Length);
            Marshal.Copy(byteArray, 0, unmanagedPointer, byteArray.Length);
            return unmanagedPointer;
        }
    }
}
