using System;
using System.Collections.Generic;
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
using WpfClient.Services;

namespace WpfClient.Pages
{
    /// <summary>
    /// Interaction logic for EmployeeDetailsPage.xaml
    /// </summary>
    public partial class EmployeeDetailsPage : Page
    {
        public EmployeeDetailsPage()
        {
            InitializeComponent();
        }

        public void employeeDetailsPage_Loaded(Object sender, RoutedEventArgs e)
        {
            ShowUserInfo();
        }

        private void ShowUserInfo()
        {
            tbkFname.Text = Globals.SelectedEmployee.Firstname;
            tbkLname.Text = Globals.SelectedEmployee.Lastname;
            tbkEmail.Text = Globals.SelectedEmployee.Email;
            tbkDepartment.Text = Globals.SelectedEmployee.Department.ToString();
            tbkDepartment.Text = Globals.SelectedEmployee.HireDate.ToShortDateString();
        }


        private void BtnAddLeave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDetele_Click(object sender, RoutedEventArgs e)
        {
            EmployeesService ops = new EmployeesService();
            ops.RemoveEmployee(Globals.SelectedEmployee.Id);
            MessageBox.Show("Angajatul " + Globals.SelectedEmployee.Firstname + " " + Globals.SelectedEmployee.Lastname + " a fost sters!");
            NavigationService.GoBack();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
