using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class ApiDbContext
    {
        private string datastore;

        public ApiDbContext()
        {
            this.datastore = "./Datastore";
            InitializeUsersStore();
        }

        private void InitializeUsersStore()
        {
            // Get users
            string filename = "/users.json";
            string json = File.ReadAllText(this.datastore + filename);
            this.Users = JsonConvert.DeserializeObject<List<User>>(json);
        }

        public List<User> Users { get; set; }

        public void SaveUserChanges()
        {
            // Save users
            string json = JsonConvert.SerializeObject(this.Users);
            string filename = "/users.json";
            File.WriteAllText(this.datastore + filename, json);
        }
    }
}
