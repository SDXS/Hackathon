namespace ShoppingList.Forms.Services
{
    using System;
    using System.Collections.Generic;

    using GalaSoft.MvvmLight.Views;

    using ShoppingList.Forms.Views;
    using ShoppingList.Portable;
    using ShoppingList.Portable.ViewModels;

    using Xamarin.Forms;

    public class NavigationService : INavigationService
    {
        private readonly Stack<string> navigationPath = new Stack<string>();

        private NavigationPage navigationPage;

        public void Initialize(NavigationPage navigationPage)
        {
            this.navigationPath.Clear();
            this.navigationPage = navigationPage;
            this.navigationPage.Popped += this.navigationPage_Popped;
            this.navigationPage.Pushed += this.navigationPage_Pushed;
            this.navigationPage_Pushed(this, new NavigationEventArgs(navigationPage.CurrentPage));
        }

        public event Action<Page, Page> Navigate;

        public string CurrentPageKey
        {
            get { return this.navigationPath.Count > 0 ? this.navigationPath.Peek() : null; }
        }

        public void GoBack()
        {
            if (this.navigationPath.Count > 0)
            {
                ((NavigationPage)Application.Current.MainPage).PopAsync();
            }
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            switch (pageKey)
            {
                case NavigationConstants.EntryPage:
                    this.navigationPage.PushAsync(new EntryPage(parameter as EntryViewModel));
                    break;
                case NavigationConstants.ListPage:
                    this.navigationPage.PushAsync(new ListPage(parameter as ListViewModel));
                    break;
            }
        }

        public void NavigateTo(string pageKey)
        {
            this.NavigateTo(pageKey, null);
        }

        private void navigationPage_Popped(object sender, NavigationEventArgs e)
        {
            this.navigationPath.Pop();
        }

        private void navigationPage_Pushed(object sender, NavigationEventArgs e)
        {
            if (e.Page is EntryPage)
            {
                this.navigationPath.Push(NavigationConstants.EntryPage);
            }
            else if (e.Page is ListPage)
            {
                this.navigationPath.Push(NavigationConstants.ListPage);
            }
        }
    }
}