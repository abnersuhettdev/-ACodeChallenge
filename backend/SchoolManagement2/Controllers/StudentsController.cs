
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.BusinessRules.Interface;
using SchoolManagement.Data;
using SchoolManagement.Models;
using SchoolManagement.Services;
using SchoolManagement.Services.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRules _studentRules;

        public StudentsController(IStudentRules studentRules)
        {
            _studentRules = studentRules;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            var result = _studentRules.GetAllStudents();
            if (result.IsNotOkResult())
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Data);
        }

        [HttpGet("{ra}")]
        public IActionResult GetStudentByRA(string ra)
        {
            var result = _studentRules.GetStudentByRA(ra);
            if (result.IsNotOkResult())
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Data);
        }

        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            var result = _studentRules.CreateStudent(student);
            if (result.IsNotOkResult())
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Data);
        }

       

        [HttpPut("{ra}")]
        public IActionResult UpdateStudent(string ra, Student student)
        {
            if (ra != student.RA)
            {
                return BadRequest();
            }

            var result = _studentRules.UpdateStudent(student);
            if (result.IsNotOkResult())
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Data);
        }

        [HttpDelete("{ra}")]
        public IActionResult DeleteStudent(string ra)
        {
            var result = _studentRules.DeleteStudent(ra);
            if (result.IsNotOkResult())
            {
                return BadRequest(result.Error);
            }

            return Ok(result);
        }
    }
}
