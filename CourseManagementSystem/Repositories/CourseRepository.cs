using CourseManagementSystem.Interfaces;
using CourseManagementSystem.Models;

namespace CourseManagementSystem.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly List<Course> _courses = new List<Course>();
        private int _nextCourseId = 1;

        public void AddCourse(Course course)
        {
            course.Id = _nextCourseId++;
            _courses.Add(course);
        }

        public void RemoveCourse(int courseId)
        {
            var course = _courses.FirstOrDefault(c => c.Id == courseId);
            if (course != null)
            {
                _courses.Remove(course);
            }
        }

        public Course GetCourseById(int courseId)
        {
            return _courses.FirstOrDefault(c => c.Id == courseId);
        }

        public IEnumerable<Course> GetAllCourses()
        {
            return _courses.AsReadOnly();
        }

        public IEnumerable<Course> GetCoursesByTeacher(int teacherId)
        {
            return _courses.Where(c => c.Teacher?.Id == teacherId);
        }

        public void AssignTeacherToCourse(int courseId, Teacher teacher)
        {
            var course = GetCourseById(courseId);
            if (course != null)
            {
                course.Teacher = teacher;
                if (!teacher.Courses.Contains(course))
                {
                    teacher.Courses.Add(course);
                }
            }
        }

        public void EnrollStudentInCourse(int courseId, Student student)
        {
            var course = GetCourseById(courseId);
            if (course != null && !course.Students.Any(s => s.Id == student.Id))
            {
                course.Students.Add(student);
                if (!student.Courses.Contains(course))
                {
                    student.Courses.Add(course);
                }
            }
        }

        public void RemoveStudentFromCourse(int courseId, int studentId)
        {
            var course = GetCourseById(courseId);
            if (course != null)
            {
                var student = course.Students.FirstOrDefault(s => s.Id == studentId);
                if (student != null)
                {
                    course.Students.Remove(student);
                    student.Courses.Remove(course);
                }
            }
        }
    }
}