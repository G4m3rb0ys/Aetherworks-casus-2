using Aetherworks_casus_2.Data;
using Aetherworks_casus_2.MVVM.Models;
using Aetherworks_casus_2.MVVM.ViewModels;

namespace Aetherworks_casus_2.MVVM.Views;

public partial class QRscanPage : ContentPage
{

    private readonly QRscanViewModel _viewModel;
    private bool _hasScanned = false;

    // Constructor for scanning from an activity
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

    // Constructor for scanning from MainPage (no activityId provided)
    public QRscanPage()
    {
        InitializeComponent();

        LocalDbService dbService = new LocalDbService();
        _viewModel = new QRscanViewModel(dbService, "", "MainPage", 0);

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
        if (_hasScanned)
        {
            return;
        } else
        {
            _hasScanned = true;
        }

        var first = e.Results?.FirstOrDefault();
        if (first == null) return;

        BarcodeReader.IsDetecting = false;

        await Dispatcher.DispatchAsync(async () =>
        {
            await _viewModel.ProcessScannedQRCode(first.Value);
            if (Navigation.NavigationStack.Count > 1)
            {
                await Navigation.PopAsync();
            }
        });
    }
}
