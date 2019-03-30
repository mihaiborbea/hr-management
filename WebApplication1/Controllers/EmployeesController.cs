using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Auth;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private ApiDbContext _db;
        public EmployeesController()
        {
            _db = new ApiDbContext();
            _db.InitializeEmployeesStore();
        }

        // GET: api/Employees
        [HttpGet]
        public ActionResult<List<Employee>> Get()
        {
            string token = this.Request.Headers["Token"];
            if (token == null)
                return BadRequest(new { error = "No authorization header" });
            
            return _db.Employees;
        }

        // GET: api/Employees/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<Employee> Get(int id)
        {
            string token = this.Request.Headers["Token"];
            if (token == null)
                return BadRequest(new { error = "No authorization header" });
            
            Employee employee = _db.Employees.FirstOrDefault(u => u.Id == id);
            if (employee != null)
            {
                return new ObjectResult(new
                {
                    employee.Id,
                    employee.Email,
                    employee.Firstname,
                    employee.Lastname,
                    employee.Department,
                    employee.HireDate,
                    employee.Leaves
                });
            }
            return BadRequest(new { error = "Invalid Id" });
        }

        // POST: api/Employees
        [HttpPost]
        public ActionResult<Employee> Post([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employees = _db.Employees;

            // Validate uniqueness of submitted email
            if (employees.FirstOrDefault(u => u.Email == employee.Email) != null)
            {
                return BadRequest(new { error = "Email already in use" });
            }

            // Auto increment Id
            if (employees.Count == 0)
                employee.Id = 1;
            else
                employee.Id = employees.Last().Id + 1;

            _db.Employees.Add(employee);
            _db.SaveEmployeesChanges();
            
            return new ObjectResult(new
            {
                employee.Id,
                employee.Email,
                employee.Firstname,
                employee.Lastname,
                employee.HireDate,
                employee.Department,
                employee.Leaves

            });
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult<Employee> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employees = _db.Employees;
            
            if (employees.FirstOrDefault(u => u.Id == id) == null)
            {
                return BadRequest(new { error = "Invalid employee" });
            }
            var employee = employees.FirstOrDefault(u => u.Id == id);

            _db.Employees.Remove(employee);
            _db.SaveEmployeesChanges();

            return new ObjectResult(new
            {
                employee.Id,
                employee.Email,
                employee.Firstname,
                employee.Lastname,
                employee.HireDate,
                employee.Department,
                employee.Leaves

            });
        }
    }
}
