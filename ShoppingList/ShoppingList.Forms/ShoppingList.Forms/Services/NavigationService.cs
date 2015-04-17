namespace ShoppingList.Forms.Services
{
    using ShoppingList.Forms.Views;
    using ShoppingList.Portable;
    using ShoppingList.Portable.ViewModels;

    using Xamarin.Forms;

    public class NavigationService : Portable.Services.INavigationService
    {
        private INavigation navigation;

        public void Initialize(INavigation navigation)
        {
            this.navigation = navigation;
        }

        public void GoBack()
        {
            this.navigation.PopAsync();
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            switch (pageKey)
            {
                case NavigationConstants.EntryPage:
                    this.navigation.PushAsync(new EntryPage(parameter as EntryViewModel));
                    break;
                case NavigationConstants.ListPage:
                    this.navigation.PushAsync(new ListPage(parameter as ListViewModel));
                    break;
            }
        }

        public void NavigateTo(string pageKey)
        {
            this.NavigateTo(pageKey, null);
        }
    }
}