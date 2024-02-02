using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POC_2_JWT_Auth.Models;

namespace POC_2_JWT_Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        [Route("GetDetails")]
        public List<Student> GetDetails()
        {
            List<Student> result = new List<Student>
            {
                new Student { Id = 1,Name="Ram",Age=16,Standarded ="10th",Contact="6261630981"},
                new Student { Id = 2,Name="Shyam",Age=18,Standarded ="12th",Contact="9753334303"},
                new Student { Id = 3,Name="Mohan",Age=17,Standarded ="11th",Contact="6261630971"},
                new Student { Id = 4,Name="Mangesh",Age=15,Standarded ="9th",Contact="7261630981"},
            };

            return result;
        }

        [HttpGet]
        [Route("SchoolDetails")]
        public string SchoolDetails() 
        {
            return "School Detail : SPS School Mangona Khurd 460665";
        }

        [HttpPost]
        [Route("AddStudent")]
        public string AddStudent(Student student)
        {
            return "Student added with Name " + student.Name;
        }
    }
}
