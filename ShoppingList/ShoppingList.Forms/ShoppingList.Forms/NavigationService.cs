using System;
using GalaSoft.MvvmLight.Views;
using ShoppingList.Portable;
using Xamarin.Forms;

namespace ShoppingList.Forms
{
    using ShoppingList.Portable.ViewModels;

    public class NavigationService : INavigationService
    {
        public string CurrentPageKey
        {
            get { throw new NotImplementedException(); }
        }

        public void GoBack()
        {
            throw new NotImplementedException();
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            switch (pageKey)
            {
                case NavigationConstants.EntryPage:
                    ((NavigationPage)Application.Current.MainPage).PushAsync(new EntryPage(parameter as EntryViewModel));
                    break;
            }
        }

        public void NavigateTo(string pageKey)
        {
            this.NavigateTo(pageKey, null);
        }
    }
}