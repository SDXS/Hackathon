namespace ShoppingList.Portable.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Views;

    using ShoppingList.Portable.Models;
    using ShoppingList.Portable.Services;

    public class EntryViewModel : CoreViewModel
    {
        private readonly DataService dataService;

        private Entry model;

        private int amount = 1;

        private string description = string.Empty;

        public EntryViewModel(INavigationService navigationService, DataService dataService, Entry model = null)
            : base(navigationService)
        {
            this.dataService = dataService;

            this.model = model;
            this.Description = this.model.Description;
            this.Amount = this.model.Amount;
        }

        internal Entry Model
        {
            get { return this.model; }
        }

        public string Description
        {
            get { return this.description; }
            set
            {
                if (this.Set(ref this.description, value))
                {
                    this.SaveCommand.RaiseCanExecuteChanged();
                }
            }
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
            get { return this.model != null; }
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
                                var newEntry = !this.IsAdded;
                                if (newEntry)
                                {
                                    this.model = new Entry();
                                }

                                this.model.Description = this.Description;
                                this.model.Amount = this.Amount;

                                if (newEntry) await this.dataService.AddAsync(this.model);
                                else await this.dataService.UpdateAsync(this.model);

                                this.NavigationService.GoBack();
                            },
                            () => this.amount > 0 && this.description.Length > 0));
            }
        }

        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return this.removeCommand ??
                        (this.removeCommand =
                        new RelayCommand(async () =>
                            {
                                await this.dataService.RemoveAsync(this.model);
                                this.NavigationService.GoBack();
                            },
                            () => this.IsAdded));
            }
        }

        #endregion Commands
    }
}
