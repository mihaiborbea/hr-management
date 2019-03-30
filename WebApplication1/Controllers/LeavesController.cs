using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeavesController : ControllerBase
    {
        private ApiDbContext _db;
        public LeavesController()
        {
            _db = new ApiDbContext();
            _db.InitializeLeavesStore();
        }

        // GET: api/Employees
        [HttpGet(Name = "GetLeaves")]
        public ActionResult<List<Leave>> GetLeaves([FromQuery]int employeeId = 0)
        {
            string token = this.Request.Headers["Token"];
            if (token == null)
                return BadRequest(new { error = "No authorization header" });

            return employeeId == 0 ? _db.Leaves : _db.Leaves.Where(l => l.EmployeeId == employeeId).ToList();
        }

        // GET: api/Employees/5
        [HttpGet("{id}", Name = "GetLeave")]
        public ActionResult<Leave> GetLeave(int id)
        {
            string token = this.Request.Headers["Token"];
            if (token == null)
                return BadRequest(new { error = "No authorization header" });

            Leave leave = _db.Leaves.FirstOrDefault(u => u.Id == id);
            if (leave != null)
            {
                return new ObjectResult(new
                {
                    leave.Id,
                    leave.EmployeeId,
                    leave.Start,
                    leave.End,
                    leave.Type
                });
            }
            return BadRequest(new { error = "Invalid Id" });
        }

        // POST: api/leaves
        [HttpPost]
        public ActionResult<Leave> Post([FromBody] Leave leave)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var leaves = _db.Leaves;

            // Validate uniqueness of submitted email
            if (leaves.FirstOrDefault(u => u.EmployeeId == leave.EmployeeId && (leave.Start > u.Start && leave.End < u.End)) != null)
            {
                return BadRequest(new { error = "Invalid leave" });
            }

            // Auto increment Id
            if (leaves.Count == 0)
                leave.Id = 1;
            else
                leave.Id = leaves.Last().Id + 1;

            _db.Leaves.Add(leave);
            _db.SaveLeavesChanges();

            return new ObjectResult(new
            {
                leave.Id,
                leave.EmployeeId,
                leave.Start,
                leave.End,
                leave.Type,

            });
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult<Leave> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var leaves = _db.Leaves;

            if (leaves.FirstOrDefault(u => u.Id == id) == null)
            {
                return BadRequest(new { error = "Invalid leave" });
            }
            var leave = leaves.FirstOrDefault(u => u.Id == id);

            _db.Leaves.Remove(leave);
            _db.SaveLeavesChanges();

            return new ObjectResult(new
            {
                leave.Id,
                leave.EmployeeId,
                leave.Start,
                leave.End,
                leave.Type,

            });
        }
    }
}
