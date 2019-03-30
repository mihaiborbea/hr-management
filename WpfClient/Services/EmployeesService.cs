using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfClient.Models;

namespace WpfClient.Services
{
    class EmployeesService
    {
        /**
         * Base Url @string
         */
        private string baseUrl;

        public EmployeesService()
        {
            this.baseUrl = "http://localhost:5000/api";
        }

        public Employee GetEmployee(int employeeId)
        {
            string endpoint = this.baseUrl + "/employees/" + employeeId;

            WebClient wc = new WebClient();
            wc.Headers.Add("Content-Type", "application/json");
            wc.Headers.Add("Token", Globals.LoggedInUser.access_token);
            try
            {
                string response = wc.DownloadString(endpoint);
                var employee = JsonConvert.DeserializeObject<Employee>(response);
                return employee;
            }
            catch (WebException)
            {
                return null;
            }
        }

        public List<Employee> GetEmployees()
        {
            string endpoint = this.baseUrl + "/employees";

            WebClient wc = new WebClient();
            wc.Headers.Add("Content-Type", "application/json");
            wc.Headers.Add("Token", Globals.LoggedInUser.access_token);
            try
            {
                string response = wc.DownloadString(endpoint);
                var employees = JsonConvert.DeserializeObject<List<Employee>>(response);
                return employees;
            }
            catch (WebException e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }

        public Employee AddEmployee(string email, string firstname,
            string lastname, Department department, DateTime hiredate)
        {
            string endpoint = this.baseUrl + "/employees";
            string method = "POST";
            string json = JsonConvert.SerializeObject(new
            {
                email,
                firstname,
                lastname,
                department,
                hiredate
            });

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            wc.Headers.Add("Token", Globals.LoggedInUser.access_token);
            try
            {
                string response = wc.UploadString(endpoint, method, json);
                return JsonConvert.DeserializeObject<Employee>(response);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Employee RemoveEmployee(int employeeId)
        {
            string endpoint = this.baseUrl + "/employees/" + employeeId;
            string method = "DELETE";

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            wc.Headers.Add("Token", Globals.LoggedInUser.access_token);
            try
            {
                string response = wc.UploadString(endpoint, method, "");
                return JsonConvert.DeserializeObject<Employee>(response);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
