namespace ShoppingList.Portable.ViewModels
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Views;

    public abstract class CoreViewModel : ViewModelBase
    {
        protected readonly INavigationService navigationService;

        public CoreViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }
    }
}