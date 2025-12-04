using System.Collections.Generic;
using CourseManagementSystem.Models;

namespace CourseManagementSystem.Interfaces
{
    public interface IStudentRepository
    {
        void AddStudent(Student student);
        void RemoveStudent(int studentId);
        Student GetStudentById(int studentId);
        IEnumerable<Student> GetAllStudents();
        IEnumerable<Student> GetStudentsByCourse(int courseId);
    }
}