namespace ShoppingList.Portable
{
    using GalaSoft.MvvmLight.Ioc;

    using ShoppingList.Portable.Services;
    using ShoppingList.Portable.ViewModels;

    public static class Bootstrap
    {
        public static void Setup()
        {
            // Services
            SimpleIoc.Default.Register<DataService>();

            // ViewModels
            SimpleIoc.Default.Register<ListViewModel>();
        }
    }
}
