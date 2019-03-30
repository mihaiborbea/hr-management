using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
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

        public Employee GetEmployee(Employee employeeId)
        {
            string endpoint = this.baseUrl + "/employees/" + employeeId;

            WebClient wc = new WebClient();
            wc.Headers.Add("Content-Type", "application/json");
            try
            {
                string response = wc.DownloadString(endpoint);
                var employee = JsonConvert.DeserializeObject<Employee>(response);
                return employee;
            }
            catch (WebException e)
            {
                return null;
            }
        }

        public List<Employee> GetEmployees()
        {
            string endpoint = this.baseUrl + "/employees/";

            WebClient wc = new WebClient();
            wc.Headers.Add("Content-Type", "application/json");
            try
            {
                string response = wc.DownloadString(endpoint);
                var employees = JsonConvert.DeserializeObject<List<Employee>>(response);
                return employees;
            }
            catch (WebException e)
            {
                return null;
            }
        }

        public Employee AddEmployee(string email, string firstname,
            string lastname, Department department)
        {
            string endpoint = this.baseUrl + "/employees";
            string method = "POST";
            string json = JsonConvert.SerializeObject(new
            {
                email = email,
                firstname = firstname,
                lastname = lastname,
                department = department
            });

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
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

        public Employee RemoveEmployee(string employeeId)
        {
            string endpoint = this.baseUrl + "/employees/" + employeeId;
            string method = "Delete";

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            try
            {
                string response = wc.UploadString(endpoint, method);
                return JsonConvert.DeserializeObject<Employee>(response);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
