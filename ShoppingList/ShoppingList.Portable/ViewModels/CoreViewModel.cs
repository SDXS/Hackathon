namespace ShoppingList.Portable.ViewModels
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Views;

    public abstract class CoreViewModel : ViewModelBase
    {
        protected readonly INavigationService NavigationService;

        private bool isLoading;

        protected CoreViewModel(INavigationService navigationService)
        {
            this.NavigationService = navigationService;
        }

        public bool IsLoading
        {
            get { return this.isLoading; }
            set { this.Set(ref this.isLoading, value); }
        }
    }
}