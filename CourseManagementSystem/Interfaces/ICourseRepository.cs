using System.Collections.Generic;
using CourseManagementSystem.Models;

namespace CourseManagementSystem.Interfaces
{
    public interface ICourseRepository
    {
        void AddCourse(Course course);
        void RemoveCourse(int courseId);
        Course GetCourseById(int courseId);
        IEnumerable<Course> GetAllCourses();
        IEnumerable<Course> GetCoursesByTeacher(int teacherId);
        void AssignTeacherToCourse(int courseId, Teacher teacher);
        void EnrollStudentInCourse(int courseId, Student student);
        void RemoveStudentFromCourse(int courseId, int studentId);
    }
}