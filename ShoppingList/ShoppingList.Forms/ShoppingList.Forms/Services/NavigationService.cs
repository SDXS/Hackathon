namespace ShoppingList.Forms.Services
{
    using ShoppingList.Forms.Views;
    using ShoppingList.Portable;
    using ShoppingList.Portable.ViewModels;

    using Xamarin.Forms;

    public class NavigationService : Portable.Services.INavigationService
    {
        private string currentPageKey = string.Empty;

        public void Initialize()
        {
            ((NavigationPage)Application.Current.MainPage).Popped += this.NavigationService_Popped;
        }

        public void GoBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            if (pageKey == this.currentPageKey) return;
            this.currentPageKey = pageKey;

            if (pageKey == NavigationConstants.EntryPage)
            {
                Application.Current.MainPage.Navigation.PushAsync(new EntryPage(parameter as EntryViewModel));
            }
        }

        void NavigationService_Popped(object sender, NavigationEventArgs e)
        {
            this.currentPageKey = string.Empty;
        }
    }
}