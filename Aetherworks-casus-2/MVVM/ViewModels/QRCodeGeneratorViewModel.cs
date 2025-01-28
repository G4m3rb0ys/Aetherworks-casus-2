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
        private string activityId;

        [ObservableProperty] 
        private string userId;

        [ObservableProperty]
        private ImageSource qrCodeImage;

        /*
        [RelayCommand]
        private void GenerateQRCodeForActivity()
        {
            if (int.TryParse(ActivityId, out int activityId))
            {
                GenerateQRCode(activityId.ToString());
            }
            else
            {
                QrCodeImage = null;
            }
        }

        [RelayCommand]
        private void GenerateQRCodeForProfile()
        {
            if (int.TryParse(UserId, out int userId))
            {
                GenerateQRCode(userId.ToString());
            }
            else 
            { 
                QrCodeImage = null;
            }
        }
        */
        [RelayCommand]
        private void GenerateQRCode()
        {
            if (!string.IsNullOrWhiteSpace(activityId))
            {
                var writer = new BarcodeWriterPixelData
                {
                    Format = ZXing.BarcodeFormat.QR_CODE,
                    Options = new ZXing.Common.EncodingOptions
                    {
                        Height = 200,
                        Width = 200,
                        Margin = 2
                    }
                };

                // Generate QR Code as a drawable image
                var result = writer.Write(activityId);
                QrCodeImage = PixelDataToImageSource(result);
            };
        }

        private ImageSource PixelDataToImageSource(ZXing.Rendering.PixelData pixelData)
        {
            // Create a new stream for the image
            var memoryStream = new MemoryStream();

            // Create an SKBitmap from the pixel data
            var info = new SKImageInfo(pixelData.Width, pixelData.Height, SKColorType.Bgra8888);
            using var skBitmap = new SKBitmap(info);

            // Copy the pixel data byte array to the bitmap
            skBitmap.InstallPixels(info, ByteArrayToNint(pixelData.Pixels));

            // Encode the SKBitmap to PNG
            using var skImage = SKImage.FromBitmap(skBitmap);
            using var skData = skImage.Encode(SKEncodedImageFormat.Png, 100);

            // Save the encoded PNG to the memory stream
            skData.SaveTo(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);

            // Convert the stream to an ImageSource
            return ImageSource.FromStream(() => memoryStream);
        }

        private nint ByteArrayToNint(byte[] byteArray)
        {
            // Allocate unmanaged memory
            var unmanagedPointer = Marshal.AllocHGlobal(byteArray.Length);

            // Copy the byte array to the unmanaged memory
            Marshal.Copy(byteArray, 0, unmanagedPointer, byteArray.Length);

            // Return the unmanaged pointer (nint)
            return unmanagedPointer;
        }

        [RelayCommand]
        private void BackButton()
        {
            Application.Current.MainPage = new MainPage();
        }
    }
}
