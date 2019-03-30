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
    /// Interaction logic for AddLeavePage.xaml
    /// </summary>
    public partial class AddLeavePage : Page
    {
        public AddLeavePage()
        {
            InitializeComponent();
            tbxLeaveType.ItemsSource = Enum.GetValues(typeof(LeaveType));
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            int employeeId = Globals.SelectedEmployee.Id;
            DateTime start = tbxStart.SelectedDate.Value;
            DateTime end = tbxEnd.SelectedDate.Value;
            Enum.TryParse(tbxLeaveType.Text, out LeaveType leaveType);

            LeavesService ops = new LeavesService();
            Leave leave = ops.AddLeave(employeeId, start, end, leaveType);
            if (leave == null)
            {
                MessageBox.Show("Concediu invalid!");
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
