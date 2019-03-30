using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class ApiDbContext
    {
        private string datastore;

        public List<User> Users { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Leave> Leaves { get; set; }

        public ApiDbContext()
        {
            this.datastore = "./Datastore";
        }

        public void InitializeUsersStore()
        {
            // Get users
            string filename = "/users.json";
            string json = File.ReadAllText(this.datastore + filename);
            this.Users = JsonConvert.DeserializeObject<List<User>>(json);
        }

        public void SaveUserChanges()
        {
            // Save users
            string json = JsonConvert.SerializeObject(this.Users);
            string filename = "/users.json";
            File.WriteAllText(this.datastore + filename, json);
        }

        public void InitializeEmployeesStore()
        {
            // Get employees
            string filename = "/employees.json";
            string json = File.ReadAllText(this.datastore + filename);
            this.Employees = JsonConvert.DeserializeObject<List<Employee>>(json);
        }

        public void SaveEmployeesChanges()
        {
            // Save employees
            string json = JsonConvert.SerializeObject(this.Employees);
            string filename = "/employees.json";
            File.WriteAllText(this.datastore + filename, json);
        }

        public void InitializeLeavesStore()
        {
            // Get employees
            string filename = "/leaves.json";
            string json = File.ReadAllText(this.datastore + filename);
            this.Leaves = JsonConvert.DeserializeObject<List<Leave>>(json);
        }

        public void SaveLeavesChanges()
        {
            // Save employees
            string json = JsonConvert.SerializeObject(this.Leaves);
            string filename = "/leaves.json";
            File.WriteAllText(this.datastore + filename, json);
        }
    }
}
