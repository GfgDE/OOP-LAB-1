using CourseManagementSystem.Interfaces;
using CourseManagementSystem.Models;

namespace CourseManagementSystem.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly List<Teacher> _teachers = new List<Teacher>();
        private int _nextTeacherId = 1;

        public void AddTeacher(Teacher teacher)
        {
            teacher.Id = _nextTeacherId++;
            _teachers.Add(teacher);
        }

        public void RemoveTeacher(int teacherId)
        {
            var teacher = _teachers.FirstOrDefault(t => t.Id == teacherId);
            if (teacher != null)
            {
                _teachers.Remove(teacher);
            }
        }

        public Teacher GetTeacherById(int teacherId)
        {
            return _teachers.FirstOrDefault(t => t.Id == teacherId);
        }

        public IEnumerable<Teacher> GetAllTeachers()
        {
            return _teachers.AsReadOnly();
        }
    }
}