namespace ShoppingList.Portable.ViewModels
{
    using GalaSoft.MvvmLight.Views;

    public class EntryViewModel : CoreViewModel
    {
        public EntryViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }

        public string Description { get; set; }
        public float Amount { get; set; }
        public bool Checked { get; set; }
    }
}
