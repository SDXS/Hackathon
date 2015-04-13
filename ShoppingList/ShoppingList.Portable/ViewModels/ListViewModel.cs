namespace ShoppingList.Portable.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Views;

    using ShoppingList.Portable.Models;







    public class ListViewModel : CoreViewModel
    {
        private List<EntryViewModel> entries;

        private RelayCommand deleteAllCommand;

        private RelayCommand addCommand;

        public ListViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }

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

        public RelayCommand DeleteAllCommand
        {
            get
            {
                return this.deleteAllCommand ??
                       (this.deleteAllCommand =
                        new RelayCommand(
                            () =>
                            {
                                this.Entries.Clear();
                                this.RaisePropertyChanged(() => this.Entries);
                                this.SaveChanges();
                            },
                            () => this.Entries.Count > 0));
            }
        }

        public RelayCommand AddCommand
        {
            get
            {
                return this.addCommand ??
                       (this.addCommand =
                        new RelayCommand(
                            () => this.navigationService.NavigateTo(NavigationConstants.EntryPage)));
            }
        }

        public RelayCommand<EntryViewModel> EditCommand
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        private void SaveChanges()
        {

        }
    }
}
