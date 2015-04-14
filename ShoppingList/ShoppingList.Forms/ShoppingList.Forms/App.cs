namespace ShoppingList.Forms
{
    using GalaSoft.MvvmLight.Ioc;
    using GalaSoft.MvvmLight.Views;

    using ShoppingList.Forms.Services;
    using ShoppingList.Forms.Views;
    using ShoppingList.Portable.ViewModels;

    using Xamarin.Forms;

    public class App : Application
    {
        public App()
        {
            NavigationPage navigationPage;

            // The root page of your application
            this.MainPage = navigationPage = new NavigationPage(new ListPage(SimpleIoc.Default.GetInstance<ListViewModel>()));

            var navigationService = (NavigationService)SimpleIoc.Default.GetInstance<INavigationService>();
            navigationService.Initialize(navigationPage);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
