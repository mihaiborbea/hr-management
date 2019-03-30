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
using WpfClient.Services;

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

            EmployeesService ops = new EmployeesService();
            var employees = ops.GetEmployees();
            Employees.Clear();
            employees.ForEach(emp => Employees.Add(emp));
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtnAddEmployee_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new AddEmployeePage());
        }

        private void BtnDeleteEmp_Click(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    // var row = (DataGrid)vis;

                    var rows = GetDataGridRowsForButtons(DG1);
                    int id;
                    EmployeesService ops = new EmployeesService();
                    foreach (DataGridRow dr in rows)
                    {
                        var emp = dr.Item as Employee;
                        id = emp.Id;
                        ops.RemoveEmployee(id);
                        MessageBox.Show("Angajatul " + emp.Firstname + " " + emp.Lastname + " a fost sters!");
                        Employees.Clear();
                        var employees = ops.GetEmployees();
                        employees.ForEach(empl => Employees.Add(empl));
                        break;
                    }
                    break;
                }
        }

        private void BtnAddLeave_Click(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    var rows = GetDataGridRowsForButtons(DG1);
                    EmployeesService ops = new EmployeesService();
                    foreach (DataGridRow dr in rows)
                    {
                        var emp = ops.GetEmployee((dr.Item as Employee).Id);
                        Globals.SelectedEmployee = emp;
                        NavigationService.Navigate(new AddLeavePage());
                        break;
                    }
                    break;
                }
        }

        private IEnumerable<DataGridRow> GetDataGridRowsForButtons(DataGrid grid)
        { 
            var itemsSource = grid.ItemsSource;
            if (null == itemsSource) yield return null;
            foreach (var item in itemsSource)
            {
                var row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (null != row & row.IsSelected) yield return row;
            }
        }

        private void DataGridCell_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    // var row = (DataGrid)vis;

                    var rows = GetDataGridRowsForButtons(DG1);
                    int id;
                    EmployeesService ops = new EmployeesService();
                    foreach (DataGridRow dr in rows)
                    {
                        var emp = ops.GetEmployee((dr.Item as Employee).Id);
                        Globals.SelectedEmployee = emp;
                        NavigationService.Navigate(new EmployeeDetailsPage());
                        break;
                    }
                    break;
                }
        }
    }
}
