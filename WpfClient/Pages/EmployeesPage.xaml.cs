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
        public ObservableCollection<Employee> Employees;

        public EmployeesPage()
        {
            InitializeComponent();

            Employees = new ObservableCollection<Employee>();
            DG1.DataContext = Employees;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Employee emp = new Employee
            {
                Firstname = "Dummy",
                Lastname = "Data",
                Email = "dummy.data@gmail.com",
                Department = Department.Management
            };
            for (var i = 0; i < 5; i++)
            {
                Employees.Add(emp);
            }
        }
    }
}
