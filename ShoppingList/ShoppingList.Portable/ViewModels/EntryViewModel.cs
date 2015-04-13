namespace ShoppingList.Portable.ViewModels
{
    using System;
    using System.Windows.Input;

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

        public ICommand AddCommand
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ICommand RemoveCommand
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
