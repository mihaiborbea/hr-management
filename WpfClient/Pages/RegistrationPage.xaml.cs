﻿using System;
using System.Collections.Generic;
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
    /// Interaction logic for RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        /**
         * Register method to handle the Rgister Button
         * @param object sender
         * @param RoutedEventArgs e
         */
        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            string username = tbxUsername.Text;
            string password = pbxPassword.Password;
            string firstname = tbxFirstname.Text;
            string lastname = tbxLastname.Text;
            string middlename = tbxMiddlename.Text;
            int age = int.Parse(tbxAge.Text);

            ApiOperations ops = new ApiOperations();
            User user = ops.RegisterUser(username, password, firstname, lastname, middlename, age);
            if (user == null)
            {
                MessageBox.Show("Username already exists");
                return;
            }
            MessageBox.Show("Registration successful");
            NavigationService.Navigate(new DetailsPage(), user);
        }

        /**
         * Method to handle going back to the previous screen
         * @param object  sender
         * @param RoutedEventArgs e
         */
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
