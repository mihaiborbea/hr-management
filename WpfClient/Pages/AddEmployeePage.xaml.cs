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
using WpfClient.Models;
using WpfClient.Services;

namespace WpfClient.Pages
{
    /// <summary>
    /// Interaction logic for AddEmployeePage.xaml
    /// </summary>
    public partial class AddEmployeePage : Page
    {
        public AddEmployeePage()
        {
            InitializeComponent();
            tbxDepartment.ItemsSource = Enum.GetValues(typeof(Department));
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            string email = tbxEmail.Text;
            string firstname = tbxFirstname.Text;
            string lastname = tbxLastname.Text;
            DateTime hiredate = tbxHireDate.DisplayDate;
            Enum.TryParse(tbxDepartment.Text, out Department department);

            EmployeesService ops = new EmployeesService();
            Employee employee = ops.AddEmployee(email, firstname, lastname, department, hiredate);
            if (employee == null)
            {
                MessageBox.Show("Email deja folosit!");
                return;
            }
            
            MessageBox.Show("Adaugare cu succes!");
            NavigationService.GoBack();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
