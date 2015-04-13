namespace ShoppingList.Portable.ViewModels
{
    using System.Collections.Generic;

    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    using ShoppingList.Portable.Models;

    public class ListViewModel : ViewModelBase
    {
        private List<Entry> entries;

        private RelayCommand deleteAllCommand;

        public List<Entry> Entries
        {
            get { return this.entries ?? (this.entries = new List<Entry>()); }
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
            
        }

        private void SaveChanges()
        {

        }
    }
}
