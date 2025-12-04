namespace CourseManagementSystem.Models
{
    public class OnlineCourse : Course
    {
        public string Platform { get; set; }
        public string MeetingUrl { get; set; }
        public bool IsSelfPaced { get; set; }

        public override string GetCourseType() => "Online";
    }
}