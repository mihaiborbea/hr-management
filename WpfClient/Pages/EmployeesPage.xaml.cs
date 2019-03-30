using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace WpfClient.Pages
{
    /// <summary>
    /// Interaction logic for EmployeesPage.xaml
    /// </summary>
    public partial class EmployeesPage : Page
    {
        public ObservableCollection<User> Customers;

        public EmployeesPage()
        {
            InitializeComponent();

            Customers = new ObservableCollection<User>();
            DG1.DataContext = Customers;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Customers.Add(Globals.LoggedInUser);
        }
    }
}
