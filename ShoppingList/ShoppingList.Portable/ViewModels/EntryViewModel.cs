namespace ShoppingList.Portable.ViewModels
{
    using System.Runtime.InteropServices.WindowsRuntime;

    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Ioc;
    using GalaSoft.MvvmLight.Views;

    public class EntryViewModel : CoreViewModel
    {
        private bool isNotAdded;

        private bool isSelected;

        private int amount;

        private string description;

        public EntryViewModel(INavigationService navigationService, bool isAdded = true)
            : base(navigationService)
        {
            this.isNotAdded = !isAdded;
        }

        public string Description
        {
            get { return this.description; }
            set { this.Set(ref this.description, value); }
        }

        public int Amount
        {
            get { return this.amount; }
            set
            {
                if (this.Set(ref this.amount, value))
                {
                    this.SaveCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public bool IsAdded
        {
            get { return !this.isNotAdded; }
            set { this.Set(ref this.isNotAdded, !value); }
        }

        public bool IsSelected
        {
            get { return this.isSelected; }
            set { this.Set(ref this.isSelected, value); }
        }

        #region Commands

        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return this.saveCommand ??
                       (this.saveCommand =
                        new RelayCommand(async () =>
                            {
                                await SimpleIoc.Default.GetInstance<ListViewModel>().SaveAsync(this);
                                this.NavigationService.GoBack();
                            },
                            () => this.amount > 0));
            }
        }

        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return this.removeCommand ??
                        (this.removeCommand =
                        new RelayCommand(
                            () =>
                            {
                                SimpleIoc.Default.GetInstance<ListViewModel>().RemoveAsync(this);
                                this.NavigationService.GoBack();
                            },
                            () => this.IsAdded));
            }
        }

        #endregion Commands
    }
}
