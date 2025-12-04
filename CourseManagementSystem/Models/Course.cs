using System.Collections.Generic;

namespace CourseManagementSystem.Models
{
    public abstract class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Credits { get; set; }
        public Teacher Teacher { get; set; }
        public List<Student> Students { get; set; } = new List<Student>();

        public abstract string GetCourseType();
    }
}