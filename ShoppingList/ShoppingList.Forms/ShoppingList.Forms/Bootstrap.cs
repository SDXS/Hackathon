namespace ShoppingList.Forms
{
    using GalaSoft.MvvmLight.Ioc;

    using ShoppingList.Forms.Services;

    public static class Bootstrap
    {
        public static void Setup()
        {
            // Services
            SimpleIoc.Default.Register<GalaSoft.MvvmLight.Views.INavigationService, NavigationService>();

            Portable.Bootstrap.Setup();
        }
    }
}
