using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using WpfClient.Models;
using WpfClient.Operations;

namespace WpfClient.Pages
{
    /// <summary>
    /// Interaction logic for DetailsPage.xaml
    /// </summary>
    public partial class DetailsPage : Page, INotifyPropertyChanged
    {
        private User _loggedUser;

        public User LoggedUser
        {
            get { return _loggedUser; }
            set
            {
                if (value.Username != _loggedUser.Username)
                {
                    _loggedUser = value;
                    OnPropertyChanged("LoggedUser");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public DetailsPage()
        {
            InitializeComponent();
            NavigationService.LoadCompleted += NavigationService_LoadCompleted;
        }

        private void NavigationService_LoadCompleted(object sender, NavigationEventArgs e)
        {
            LoggedUser = (User)e.ExtraData;
            FetchUserDetails();
            ShowUserInfo();
        }

        /**
         * Fetch User Details
         */
        private void FetchUserDetails()
        {
            ApiOperations ops = new ApiOperations();
            User user = ops.GetUserDetails(LoggedUser);
            if (user == null)
            {
                MessageBox.Show("Session expired");
                // Navigate back to login page
                NavigationService.Navigate(new LoginPage());
                return;
            }
            LoggedUser = user;
        }

        /**
         * Show User Info on the Screen
         */
        private void ShowUserInfo()
        {
            tbkWelcome.Text = LoggedUser.Username;
            tbkFname.Text = LoggedUser.Firstname;
            tbkMname.Text = LoggedUser.Middlename;
            tbkLname.Text = LoggedUser.Lastname;
            tbkAge.Text = LoggedUser.Age.ToString();
        }

        /**
         * Logout Method to be called on the logout Button
         * @param  object sender
         * @param  RoutedEventArgs e
         */
        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }
    }
}
