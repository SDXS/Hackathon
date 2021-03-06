﻿namespace ShoppingList.Portable.Services
{
    public interface INavigationService
    {
        /// <summary>
        /// If possible, instructs the navigation service to discard the current page
        ///  and display the previous page on the navigation stack.
        /// </summary>
        void GoBack();
        
        /// <summary>
        /// Instructs the navigation service to display a new page corresponding to the
        ///  given key, and passes a parameter to the new page.  Depending on the platforms,
        ///  the navigation service might have to be Configure with a key/page list.
        /// </summary>
        /// <param name="pageKey">The key corresponding to the page that should be displayed.</param>
        /// <param name="parameter">The parameter that should be passed to the new page.</param>
        void NavigateTo(string pageKey, object parameter);
    }
}
