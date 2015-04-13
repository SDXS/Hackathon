namespace ShoppingList.Forms
{
    using ShoppingList.Portable.ViewModels;

    using Xamarin.Forms;

    public partial class EntryPage : ContentPage
    {
        private readonly EntryViewModel viewModel;

        public EntryPage(EntryViewModel viewModel)
        {
            this.viewModel = viewModel;
            this.BindingContext = viewModel;

            InitializeComponent();
        }
    }
}
