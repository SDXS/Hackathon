namespace ShoppingList.Forms.Services
{
    using ShoppingList.Forms.Views;
    using ShoppingList.Portable;
    using ShoppingList.Portable.ViewModels;

    using Xamarin.Forms;

    public class NavigationService : Portable.Services.INavigationService
    {
        public void GoBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            switch (pageKey)
            {
                case NavigationConstants.EntryPage:
                    Application.Current.MainPage.Navigation.PushAsync(new EntryPage(parameter as EntryViewModel));
                    break;
                case NavigationConstants.ListPage:
                    Application.Current.MainPage.Navigation.PushAsync(new ListPage(parameter as ListViewModel));
                    break;
            }
        }

        public void NavigateTo(string pageKey)
        {
            this.NavigateTo(pageKey, null);
        }
    }
}