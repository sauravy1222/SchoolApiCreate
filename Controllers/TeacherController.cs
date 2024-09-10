using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMSWEBAPI.Data;
using SMSWEBAPI.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SMSWEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        public IWebHostEnvironment env;
        public TeacherController(ApplicationDbContext db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }
        [Route("GetTeacherById/{id}")]
        [HttpGet]
        public IActionResult GetTeacherById(int id)
        {
            var Teacher = db.Teachers.Find(1);
            return Ok(Teacher);
        }

        [Route("StudentAttendence")]
        [HttpPost]
        public IActionResult StudentAttendence(StudentAttendance attendance)
        {
            
            db.StudentAttendance.Add(attendance);
            db.SaveChanges();
            return Ok(attendance);
        }


    }
}
