namespace ShoppingList.Portable.ViewModels
{
    using GalaSoft.MvvmLight;

    public abstract class CoreViewModel : ViewModelBase
    {
        protected readonly Services.INavigationService NavigationService;

        private bool isLoading;

        protected CoreViewModel(Services.INavigationService navigationService)
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