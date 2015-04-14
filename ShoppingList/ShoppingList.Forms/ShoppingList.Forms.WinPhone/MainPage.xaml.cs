namespace ShoppingList.Forms.WinPhone
{
    using Microsoft.Phone.Controls;

    public partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.SupportedOrientations = SupportedPageOrientation.PortraitOrLandscape;

            Bootstrap.Setup();

            Xamarin.Forms.Forms.Init();
            this.LoadApplication(new Forms.App());
        }
    }
}
