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
                this.viewModel.DeleteCommand.Execute(menuItem.BindingContext as Portable.Models.Entry);
            }
        }

        void editMenuItem_Clicked(object sender, System.EventArgs e)
        {
            var menuItem = sender as MenuItem;
            if (menuItem != null)
            {
                this.viewModel.EditCommand.Execute(menuItem.BindingContext as Portable.Models.Entry);
            }
        }

        void listvView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}
