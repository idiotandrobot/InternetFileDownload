using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using InternetFileDownload.Resources;
using System.IO;
using System.IO.IsolatedStorage;

namespace InternetFileDownload
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            Browser.Navigate(new Uri("http://cr.yp.to/proto/netstrings.txt"));
        }

        private void Browser_Navigated(object sender, NavigationEventArgs e)
        {
            var store = IsolatedStorageFile.GetUserStoreForApplication();
            var filename = "InTheRoot.txt";

            if (store.FileExists(filename)) store.DeleteFile(filename);

            using (var stream = store.CreateFile(filename))
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(Browser.SaveToString());
                }
            }

            using (var file = store.OpenFile(filename, FileMode.Open))
            {
                using(var reader = new StreamReader(file))
	            {
		            Text.Text = reader.ReadToEnd();
	            }
            }
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}