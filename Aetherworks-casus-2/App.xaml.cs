namespace Aetherworks_casus_2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new MVVM.Views.MainPage();
        }
    }
}