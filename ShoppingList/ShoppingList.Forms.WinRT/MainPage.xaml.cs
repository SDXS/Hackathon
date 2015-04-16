﻿namespace ShoppingList.Forms.WinRT
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage
    {
        public MainPage()
        {
            Bootstrap.Setup();

            this.InitializeComponent();

            this.LoadApplication(new Forms.App());
        }
    }
}