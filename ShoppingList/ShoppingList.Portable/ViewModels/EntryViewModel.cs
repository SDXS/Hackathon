﻿namespace ShoppingList.Portable.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Ioc;
    using GalaSoft.MvvmLight.Views;

    using ShoppingList.Portable.Models;

    public class EntryViewModel : CoreViewModel
    {
        private Entry entity;

        private bool isSelected;

        private int amount;

        private string description;

        public EntryViewModel(INavigationService navigationService, Entry entity = null)
            : base(navigationService)
        {
            this.entity = entity;

            this.Reset();
        }

        internal Entry Entity
        {
            get { return this.entity; }
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
            get { return entity != null; }
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
                                if (this.entity == null)
                                {
                                    this.entity = new Entry();
                                }

                                entity.Description = this.Description;
                                entity.Amount = this.Amount;

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

        internal void Reset()
        {
            if (this.entity != null)
            {
                this.Description = this.entity.Description;
                this.Amount = this.entity.Amount;
            }
        }
    }
}
