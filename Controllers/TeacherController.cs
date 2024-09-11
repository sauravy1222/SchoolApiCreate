using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [Route("GetStudent")]
        [HttpGet]
        public IActionResult GetStudent()
        {
            var student = db.Students.ToList();

            return Ok(student);
        }

        [Route("StudentAttendence")]
        [HttpPost]
        public IActionResult StudentAttendence(StudentAttendance attendance)
        {

            db.StudentAttendance.Add(attendance);
            db.SaveChanges();
            return Ok(attendance);
        }


        [HttpPost("AddAttendance")]
        public IActionResult AddAttendance([FromForm] int[] selectedStudents)
        {
            if (selectedStudents == null || !selectedStudents.Any())
            {
                return BadRequest("No student IDs provided.");
            }

            // Fetch all students from the database
            var students = db.Students
                .Where(s => selectedStudents.Contains(s.userid))
                .ToList();

            if (!students.Any())
            {
                return NotFound("No students found.");
            }

            // Fetch class names and their IDs
            var classNames = students.Select(s => s.ClassId).Distinct().ToList();
            var classIdMapping = db.Classes
                .Where(c => classNames.Contains(c.ClassName))
                .ToDictionary(c => c.ClassName, c => c.ClassId);

            // Create attendance records for selected students
            var attendanceRecords = students.Select(stu => new StudentAttendance
            {
                StudentId = stu.userid,
                ClassId = classIdMapping.TryGetValue(stu.ClassId, out var classId) ? classId : default,
                Status = true // Mark as present
            }).ToList();

            // Find all students that were not selected
            var allStudents = db.Students.ToList();
            var unselectedStudents = allStudents
                .Where(s => !selectedStudents.Contains(s.userid))
                .ToList();

            // Create attendance records for unselected students
            var absentRecords = unselectedStudents.Select(stu => new StudentAttendance
            {
                StudentId = stu.userid,
                ClassId = classIdMapping.TryGetValue(stu.ClassId, out var classId) ? classId : default,
                Status = false // Mark as absent
            }).ToList();

            // Combine selected and absent records
            attendanceRecords.AddRange(absentRecords);

            // Handle case where ClassId might be missing if class name not found
            if (attendanceRecords.Any(a => a.ClassId == default))
            {
                return BadRequest("Some class names could not be mapped to class IDs.");
            }

            // Add attendance records to the database
            db.StudentAttendance.AddRange(attendanceRecords);
            db.SaveChanges();

            return Ok("Attendance records added successfully.");
        }


    }
}


