namespace ShoppingList.Forms.Views
{
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

        void removeMenuItem_Clicked(object sender, System.EventArgs e)
        {
            var menuItem = sender as MenuItem;
            if (menuItem != null)
            {
                this.viewModel.DeleteCommand.Execute(menuItem.BindingContext as EntryViewModel);
            }
        }

        void editMenuItem_Clicked(object sender, System.EventArgs e)
        {
            var menuItem = sender as MenuItem;
            if (menuItem != null)
            {
                this.viewModel.EditCommand.Execute(menuItem.BindingContext as EntryViewModel);
            }
        }

        void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e == null)
            {
                return; // has been set to null, do not 'process' tapped event
            }

            ((ListView)sender).SelectedItem = null; // de-select the row
        }
    }
}
