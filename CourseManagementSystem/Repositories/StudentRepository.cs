using CourseManagementSystem.Interfaces;
using CourseManagementSystem.Models;

namespace CourseManagementSystem.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly List<Student> _students = new List<Student>();
        private int _nextStudentId = 1;

        public void AddStudent(Student student)
        {
            student.Id = _nextStudentId++;
            _students.Add(student);
        }

        public void RemoveStudent(int studentId)
        {
            var student = _students.FirstOrDefault(s => s.Id == studentId);
            if (student != null)
            {
                _students.Remove(student);
            }
        }

        public Student GetStudentById(int studentId)
        {
            return _students.FirstOrDefault(s => s.Id == studentId);
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _students.AsReadOnly();
        }

        public IEnumerable<Student> GetStudentsByCourse(int courseId)
        {
            return _students.Where(s => s.Courses.Any(c => c.Id == courseId));
        }
    }
}