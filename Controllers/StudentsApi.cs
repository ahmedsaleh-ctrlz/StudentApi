using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentApi.Classes;
using System.Net.WebSockets;

namespace StudentApi.Controllers
{
    [Route("api/StudentsApi")]
    [ApiController]
    public class StudentsApi : ControllerBase
    {
        [HttpGet("All", Name = "All")]
        public ActionResult<IEnumerable<Student>> GetAllStudents()
        {
            return Ok(Data.StudentsList);
        }


        [HttpGet("Passed", Name = "Passed")]

        public ActionResult<IEnumerable<Student>> GetPassedStudents()
        {
            var PassedStudents = Data.StudentsList.Where(student => student.Grade >= 50);
            return Ok(PassedStudents);
        }

        [HttpGet("AverageGrade", Name = "AverageGrade")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<float> GetAverageStudents()
        {
            if (Data.StudentsList.Count == 0)
            {
                return NotFound("No students found");
            }

            var avg = Data.StudentsList.Average(student => student.Grade);
            return Ok(avg);

        }


        [HttpGet("{id}",Name ="GetStudentByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Student> GetStudentByID(int id)
        {
            if (id < 1)
            {
                return BadRequest("Not Accepted ID");
            }
            var student = Data.StudentsList.FirstOrDefault(s => s.Id == id);
            if(student == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }
            return Ok(student);
        }

        [HttpPost("AddStudent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Student> AddStudent(Student newStudent) 
        {
            if(newStudent == null || string.IsNullOrEmpty(newStudent.Name)) 
            {
                return BadRequest("Invalid Studentd data");
            }

            newStudent.Id = Data.StudentsList.Count > 0 ? Data.StudentsList.Max(s => s.Id) + 1 : 1;
            Data.StudentsList.Add(newStudent);
            return CreatedAtRoute("GetStudentByID", new { id = newStudent.Id }, newStudent);
        }

        [HttpDelete("{id}",Name ="DeleteStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Student> DeleteStudent(int id) 
        {
            if (id < 1)
            {
                return BadRequest("Not Accepted ID");
            }
            var student = Data.StudentsList.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }
            Data.StudentsList.Remove(student);
            return Ok("Deleted"); 
        }

        [HttpPut("{id}",Name = "UpdateStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<Student> UpdateStudent(int id, Student updatedStudent) 
        {
            if (id < 1)
            {
                return BadRequest("Not Accepted ID");
            }
            var student = Data.StudentsList.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }

            student.Name = updatedStudent.Name;
            student.Age = updatedStudent.Age;
            student.Grade = updatedStudent.Grade;

            return Ok("Updated");
        }

    }
}
