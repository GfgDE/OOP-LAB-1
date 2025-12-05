using CourseManagementSystem.Interfaces;
using CourseManagementSystem.Models;

namespace CourseManagementSystem.Services
{
    public class CourseManagementService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IStudentRepository _studentRepository;

        public CourseManagementService(
            ICourseRepository courseRepository,
            ITeacherRepository teacherRepository,
            IStudentRepository studentRepository)
        {
            _courseRepository = courseRepository;
            _teacherRepository = teacherRepository;
            _studentRepository = studentRepository;
        }

        public void CreateOnlineCourse(string name, string description, int credits, string platform, string meetingUrl, bool isSelfPaced)
        {
            var course = new OnlineCourse
            {
                Name = name,
                Description = description,
                Credits = credits,
                Platform = platform,
                MeetingUrl = meetingUrl,
                IsSelfPaced = isSelfPaced
            };
            _courseRepository.AddCourse(course);
        }

        public void CreateOfflineCourse(string name, string description, int credits, string classroom, string schedule, string campus)
        {
            var course = new OfflineCourse
            {
                Name = name,
                Description = description,
                Credits = credits,
                Classroom = classroom,
                Schedule = schedule,
                Campus = campus
            };
            _courseRepository.AddCourse(course);
        }

        public bool AssignTeacherToCourse(int courseId, int teacherId)
        {
            var course = _courseRepository.GetCourseById(courseId);
            var teacher = _teacherRepository.GetTeacherById(teacherId);
            
            if (course == null || teacher == null)
                return false;

            _courseRepository.AssignTeacherToCourse(courseId, teacher);
            return true;
        }

        public bool EnrollStudentInCourse(int courseId, int studentId)
        {
            var course = _courseRepository.GetCourseById(courseId);
            var student = _studentRepository.GetStudentById(studentId);
            
            if (course == null || student == null)
                return false;

            _courseRepository.EnrollStudentInCourse(courseId, student);
            return true;
        }

        public IEnumerable<Course> GetTeacherCourses(int teacherId)
        {
            return _courseRepository.GetCoursesByTeacher(teacherId);
        }

        public IEnumerable<Student> GetCourseStudents(int courseId)
        {
            return _studentRepository.GetStudentsByCourse(courseId);
        }

        public void RemoveCourse(int courseId)
        {
            _courseRepository.RemoveCourse(courseId);
        }

        public void AddTeacher(Teacher teacher)
        {
            _teacherRepository.AddTeacher(teacher);
        }

        public void AddStudent(Student student)
        {
            _studentRepository.AddStudent(student);
        }

        public IEnumerable<Course> GetAllCourses() => _courseRepository.GetAllCourses();
        public IEnumerable<Teacher> GetAllTeachers() => _teacherRepository.GetAllTeachers();
        public IEnumerable<Student> GetAllStudents() => _studentRepository.GetAllStudents();
        public Course GetCourse(int courseId) => _courseRepository.GetCourseById(courseId);
        public Teacher GetTeacher(int teacherId) => _teacherRepository.GetTeacherById(teacherId);
        public Student GetStudent(int studentId) => _studentRepository.GetStudentById(studentId);
    }
}