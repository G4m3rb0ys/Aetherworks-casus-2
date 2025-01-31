using Aetherworks_casus_2.Data;
using Aetherworks_casus_2.MVVM.Models;
using Aetherworks_casus_2.MVVM.ViewModels;

namespace Aetherworks_casus_2.MVVM.Views;

public partial class QRscanPage : ContentPage
{

    private readonly QRscanViewModel _viewModel;

    public QRscanPage(int activityId)
    {
        InitializeComponent();

        LocalDbService dbService = new LocalDbService();
        _viewModel = new QRscanViewModel(dbService, activityId.ToString(), "Activity", activityId);

        BindingContext = _viewModel;

        BarcodeReader.Options = new ZXing.Net.Maui.BarcodeReaderOptions
        {
            Formats = ZXing.Net.Maui.BarcodeFormat.QrCode,
            AutoRotate = true,
            Multiple = false
        };
    }

    private async void barcodeReader_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
    {
        var first = e.Results?.FirstOrDefault();
        if (first == null) return;

        /*
        await Dispatcher.DispatchAsync(async () =>
        {
            if (int.TryParse(first.Value, out int activityId))
            {
                if (BindingContext is QRscanViewModel viewModel)
                {
                    await viewModel.AddUserToParticipation(activityId);
                    await DisplayAlert("Id voor activiteit", $"ID: {activityId}", "OK");
                }
            }
            else
            {
                await DisplayAlert("Invalid QR Code", "The scanned QR code is not valid for an activity.", "OK");
            }
        });*/

        await Dispatcher.DispatchAsync(async () =>
        {
            await _viewModel.ProcessScannedQRCode(first.Value);
        });
    }
}