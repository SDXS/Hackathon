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
                        new RelayCommand(() => this.NavigationService.NavigateTo(NavigationConstants.EntryPage, new EntryViewModel(this.NavigationService))));
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
            if (!this.Entries.Contains(entryViewModel))
            {
                this.Entries = this.Entries.Concat(new[] { entryViewModel }).ToList();
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

                var list = await this.dataService.LoadDataAsync();
                if (list == null || !list.Any())
                {
                    // Just provide some demo data.
                    list = new List<Entry>
                               {
                                   new Entry { Description = "Intel Xeon E5-2698V3 16 CORE CPU", Amount = 2 },
                                   new Entry { Description = "Kühlerpaste 20ml Tube", Amount = 2 },
                                   new Entry { Description = "AB-14 Kühlkörper", Amount = 2 },
                                   new Entry { Description = "16GB DDR4 RAM Riegel", Amount = 20 },
                                   new Entry { Description = "1000W Netzteil 90+", Amount = 1 },
                                   new Entry { Description = "RJ45 CAT8 Kabel 0,5m", Amount = 12 },
                                   new Entry { Description = "RJ45 CAT8 Kabel 1m", Amount = 18 },
                                   new Entry { Description = "RJ45 CAT8 Kabel 2m", Amount = 5 },
                                   new Entry { Description = "RJ45 CAT8 Kabel 10m", Amount = 8 },
                                   new Entry { Description = "RJ45 CAT8 Kabel 20m", Amount = 7 },
                                   new Entry { Description = "16 Slot Router", Amount = 1 },
                                   new Entry { Description = "Ersatz Blade-Frontblende", Amount = 1 },
                                   new Entry { Description = "6TB Enterprise Hard Disk SAS2", Amount = 8 },
                                   new Entry { Description = "1TB Enterprise SSD SATA-III", Amount = 2 },
                                   new Entry { Description = "1TB Enterprise SSD PCIe", Amount = 1 },
                                   new Entry { Description = "ESX Lizenz", Amount = 2 },
                                   new Entry { Description = "MS SQL Server Kernlizenz", Amount = 16 },
                                   new Entry { Description = "MS Windows Server Core Lizenz", Amount = 2 },
                               };
                }

                this.Entries = list.Select(entry => new EntryViewModel(this.NavigationService, entry)).ToList();
            }
            finally
            {
                this.IsLoading = false;
            }
        }
    }
}
