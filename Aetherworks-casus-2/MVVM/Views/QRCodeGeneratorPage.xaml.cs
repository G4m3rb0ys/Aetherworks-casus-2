using Aetherworks_casus_2.MVVM.ViewModels;

namespace Aetherworks_casus_2.MVVM.Views;

public partial class QRCodeGeneratorPage : ContentPage
{
	public QRCodeGeneratorPage()
	{
		InitializeComponent();
        BindingContext = new QRCodeGeneratorViewModel();
    }
}