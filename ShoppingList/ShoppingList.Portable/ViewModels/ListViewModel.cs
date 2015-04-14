namespace ShoppingList.Portable.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Views;

    using ShoppingList.Portable.Models;
    using ShoppingList.Portable.Services;

    public class ListViewModel : CoreViewModel
    {
        private readonly DataService dataService;

        public ListViewModel(INavigationService navigationService, DataService dataService)
            : base(navigationService)
        {
            this.dataService = dataService;

            this.LoadDataAsync();
        }

        private List<EntryViewModel> entries;
        public List<EntryViewModel> Entries
        {
            get { return this.entries ?? (this.entries = new List<EntryViewModel>()); }
            private set
            {
                this.entries = value;
                this.RaisePropertyChanged(() => this.Entries);
                this.DeleteAllCommand.RaiseCanExecuteChanged();
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
                        new RelayCommand(async () =>
                            {
                                this.Entries = new List<EntryViewModel>();
                                await this.SaveChangesAsync();
                            },
                            () => this.Entries.Count > 0));
            }
        }
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return this.addCommand ??
                       (this.addCommand =
                        new RelayCommand(() => this.NavigationService.NavigateTo(NavigationConstants.EntryPage, new EntryViewModel(this.NavigationService, false))));
            }
        }

        private RelayCommand<EntryViewModel> deleteCommand;
        public RelayCommand<EntryViewModel> DeleteCommand
        {
            get
            {
                return this.deleteCommand ??
                       (this.deleteCommand =
                        new RelayCommand<EntryViewModel>(async entry => { await this.RemoveAsync(entry); }));
            }
        }

        private RelayCommand<EntryViewModel> editCommand;
        public RelayCommand<EntryViewModel> EditCommand
        {
            get
            {
                return this.editCommand ??
                       (this.editCommand =
                       new RelayCommand<EntryViewModel>(entry => this.NavigationService.NavigateTo(NavigationConstants.EntryPage, entry)));
            }
        }

        #endregion Commands

        public async Task RemoveAsync(EntryViewModel entryViewModel)
        {
            this.Entries = this.Entries.Except(new[] { entryViewModel }).ToList();
            await this.SaveChangesAsync();
        }

        public async Task SaveAsync(EntryViewModel entryViewModel)
        {
            if (!entryViewModel.IsAdded)
            {
                this.Entries = this.Entries.Concat(new[] { entryViewModel }).ToList();
                entryViewModel.IsAdded = true;
            }

            await this.SaveChangesAsync();
        }

        private async Task SaveChangesAsync()
        {
            await this.dataService.SaveDataAsync(
                this.Entries.Select(
                    entry =>
                    new Entry
                    {
                        Description = entry.Description,
                        Amount = entry.Amount,
                    })
                    .ToList());
        }

        private async Task LoadDataAsync()
        {
            try
            {
                this.IsLoading = true;
                this.Entries = (await this.dataService.LoadDataAsync()).Select(
                    entry => new EntryViewModel(this.NavigationService)
                                 {
                                     Description = entry.Description,
                                     Amount = entry.Amount
                                 }).ToList();
            }
            finally
            {
                this.IsLoading = false;
            }
        }
    }
}
