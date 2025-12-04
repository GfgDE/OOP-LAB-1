using System.Collections.Generic;
using CourseManagementSystem.Models;

namespace CourseManagementSystem.Interfaces
{
    public interface ITeacherRepository
    {
        void AddTeacher(Teacher teacher);
        void RemoveTeacher(int teacherId);
        Teacher GetTeacherById(int teacherId);
        IEnumerable<Teacher> GetAllTeachers();
    }
}