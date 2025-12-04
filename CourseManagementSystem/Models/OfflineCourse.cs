namespace CourseManagementSystem.Models
{
    public class OfflineCourse : Course
    {
        public string Classroom { get; set; }
        public string Schedule { get; set; }
        public string Campus { get; set; }

        public override string GetCourseType() => "Offline";
    }
}