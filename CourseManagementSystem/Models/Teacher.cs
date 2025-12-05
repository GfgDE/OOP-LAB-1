namespace CourseManagementSystem.Models
{
    public class Teacher : Person
    {
        public string Department { get; set; }
        public List<Course> Courses { get; set; } = new List<Course>();
    }
}