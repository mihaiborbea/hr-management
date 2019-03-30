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
    /// Interaction logic for EmployeeDetailsPage.xaml
    /// </summary>
    public partial class EmployeeDetailsPage : Page
    {
        public ObservableCollection<Leave> Leaves;

        public EmployeeDetailsPage()
        {
            InitializeComponent();
            Leaves = new ObservableCollection<Leave>();
            DG1.DataContext = Leaves;
        }

        public void employeeDetailsPage_Loaded(Object sender, RoutedEventArgs e)
        {
            ShowUserInfo();
            LeavesService ops = new LeavesService();
            var employees = ops.GetLeaves(Globals.SelectedEmployee.Id);
            Leaves.Clear();
            employees.ForEach(emp => Leaves.Add(emp));
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
            NavigationService.Navigate(new AddLeavePage());
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

        private void BtnDeleteLeave_Click(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    // var row = (DataGrid)vis;

                    var rows = GetDataGridRowsForButtons(DG1);
                    int id;
                    LeavesService ops = new LeavesService();
                    foreach (DataGridRow dr in rows)
                    {
                        var lv = dr.Item as Leave;
                        id = lv.Id;
                        ops.RemoveLeave(id);
                        MessageBox.Show("Concediul a fost sters!");
                        Leaves.Clear();
                        var employees = ops.GetLeaves(Globals.SelectedEmployee.Id);
                        employees.ForEach(empl => Leaves.Add(empl));
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
    }
}
