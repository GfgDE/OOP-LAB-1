using System.Collections.Generic;

namespace CourseManagementSystem.Models
{
    public class Student : Person
    {
        public string StudentId { get; set; }
        public List<Course> Courses { get; set; } = new List<Course>();
    }
}