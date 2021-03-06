﻿namespace ShoppingList.Forms
{
    using GalaSoft.MvvmLight.Ioc;

    using ShoppingList.Forms.Services;
    using ShoppingList.Forms.Views;
    using ShoppingList.Portable.ViewModels;

    using Xamarin.Forms;

    public class App : Application
    {
        public App()
        {
            // The root page of your application
            this.MainPage = new NavigationPage(new ListPage(SimpleIoc.Default.GetInstance<ListViewModel>()));

            ((NavigationService)SimpleIoc.Default.GetInstance<Portable.Services.INavigationService>()).Initialize();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
