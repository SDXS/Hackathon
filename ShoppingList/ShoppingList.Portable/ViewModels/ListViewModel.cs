namespace ShoppingList.Portable.ViewModels
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using GalaSoft.MvvmLight.Command;

    using ShoppingList.Portable.Models;
    using ShoppingList.Portable.Services;

    public class ListViewModel : CoreViewModel
    {
        private readonly DataService dataService;

        public ListViewModel(INavigationService navigationService, DataService dataService)
            : base(navigationService)
        {
            this.dataService = dataService;
            this.dataService.ListChanged += this.HandleListChanged;

            this.LoadDataAsync();
        }

        private List<Entry> entries;

        public List<Entry> Entries
        {
            get { return this.entries; }
            private set
            {
                if (this.Set(ref this.entries, value))
                {
                    this.DeleteAllCommand.RaiseCanExecuteChanged();
                }
            }
        }

        #region Commands

        private RelayCommand deleteAllCommand;
        public RelayCommand DeleteAllCommand
        {
            get
            {
                return this.deleteAllCommand ??
                       (this.deleteAllCommand =
                        new RelayCommand(
                            async () => { await this.dataService.RemoveAllAsync(); },
                            () => this.Entries != null && this.Entries.Count > 0));
            }
        }
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return this.addCommand ??
                       (this.addCommand =
                        new RelayCommand(() => this.NavigationService.NavigateTo(NavigationConstants.EntryPage, new EntryViewModel(this.NavigationService, this.dataService))));
            }
        }

        private RelayCommand<Entry> deleteCommand;
        public RelayCommand<Entry> DeleteCommand
        {
            get
            {
                return this.deleteCommand ??
                       (this.deleteCommand =
                        new RelayCommand<Entry>(async entry => { await this.dataService.RemoveAsync(entry); }));
            }
        }

        private RelayCommand<Entry> editCommand;
        public RelayCommand<Entry> EditCommand
        {
            get
            {
                return this.editCommand ??
                       (this.editCommand =
                       new RelayCommand<Entry>(entry => this.NavigationService.NavigateTo(NavigationConstants.EntryPage, new EntryViewModel(this.NavigationService, this.dataService, entry))));
            }
        }

        #endregion Commands

        private async Task LoadDataAsync()
        {
            try
            {
                this.IsLoading = true;
                this.Entries = await this.dataService.GetAsync();
            }
            finally
            {
                this.IsLoading = false;
            }
        }

        private async void HandleListChanged()
        {
            await this.LoadDataAsync();
        }
    }
}
