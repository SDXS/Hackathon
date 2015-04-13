using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;

namespace ShoppingList.Forms
{
    public static class Bootstrap
    {
        public static void Setup()
        {
            SimpleIoc.Default.Register<INavigationService, NavigationService>();
        }
    }
}
