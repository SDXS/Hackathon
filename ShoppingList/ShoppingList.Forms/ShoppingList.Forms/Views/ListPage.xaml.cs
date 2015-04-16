namespace ShoppingList.Forms.Views
{
    using System.Collections.Generic;

    using ShoppingList.Portable.ViewModels;

    using Xamarin.Forms;

    public partial class ListPage : ContentPage
    {
        private readonly ListViewModel viewModel;

        public ListPage(ListViewModel viewModel)
        {
            this.viewModel = viewModel;
            this.BindingContext = this.viewModel;

            this.InitializeComponent();
        }

        void removeButton_Clicked(object sender, System.EventArgs e)
        {
            var menuItem = sender as MenuItem;
            if (menuItem != null)
            {
                this.viewModel.DeleteCommand.Execute(menuItem.BindingContext as EntryViewModel);
            }
        }

        void editButton_Clicked(object sender, System.EventArgs e)
        {
            var menuItem = sender as MenuItem;
            if (menuItem != null)
            {
                this.viewModel.EditCommand.Execute(menuItem.BindingContext as EntryViewModel);
            }
        }

        void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            foreach (var variable in viewModel.Entries)
            {
                variable.IsSelected = false;
            }

            var entryViewModel = e.SelectedItem as EntryViewModel;
            if (entryViewModel != null)
            {
                entryViewModel.IsSelected = true;
            }
        }
    }
}
