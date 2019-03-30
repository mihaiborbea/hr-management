using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

using WpfClient.Models;
using WpfClient.Operations;

namespace WpfClient.Pages
{
    public partial class DetailsPage : Page
    {

        public DetailsPage()
        {
            InitializeComponent();
        }

        public void detailsPage_Loaded(Object sender, RoutedEventArgs e)
        {
            FetchUserDetails();
            ShowUserInfo();
        }

        private void FetchUserDetails()
        {
            ApiOperations ops = new ApiOperations();
            User user = ops.GetUserDetails(Globals.LoggedInUser);
            if (user == null)
            {
                MessageBox.Show("Session expired");
                NavigationService.Navigate(new LoginPage());
                return;
            }
            Globals.LoggedInUser = user;
        }
        
        private void ShowUserInfo()
        {
            tbkWelcome.Text = Globals.LoggedInUser.Username;
            tbkFname.Text = Globals.LoggedInUser.Firstname;
            tbkMname.Text = Globals.LoggedInUser.Middlename;
            tbkLname.Text = Globals.LoggedInUser.Lastname;
            tbkAge.Text = Globals.LoggedInUser.Age.ToString();
        }
        
        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }
    }
}
