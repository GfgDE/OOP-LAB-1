using CourseManagementSystem.Models;
using CourseManagementSystem.Repositories;
using CourseManagementSystem.Services;
using Xunit;

namespace CourseManagementSystem.Tests
{
    public class CourseManagementServiceTests
    {
        private readonly CourseManagementService _service;

        public CourseManagementServiceTests()
        {
            var courseRepo = new CourseRepository();
            var teacherRepo = new TeacherRepository();
            var studentRepo = new StudentRepository();
            _service = new CourseManagementService(courseRepo, teacherRepo, studentRepo);
        }

        [Fact]
        public void CreateOnlineCourse_ShouldAddCourseToRepository()
        {
            // Act
            _service.CreateOnlineCourse("C# Programming", "Learn C#", 3, "Zoom", "https://zoom.us/meeting", false);

            // Assert
            var courses = _service.GetAllCourses();
            Assert.Single(courses);
            var course = courses.First();
            Assert.IsType<OnlineCourse>(course);
            Assert.Equal("C# Programming", course.Name);
            Assert.Equal("Online", course.GetCourseType());
        }

        [Fact]
        public void CreateOfflineCourse_ShouldAddCourseToRepository()
        {
            // Act
            _service.CreateOfflineCourse("Mathematics", "Advanced Math", 4, "Room 101", "Mon/Wed 10:00-11:30", "Main Campus");

            // Assert
            var courses = _service.GetAllCourses();
            Assert.Single(courses);
            var course = courses.First();
            Assert.IsType<OfflineCourse>(course);
            Assert.Equal("Mathematics", course.Name);
            Assert.Equal("Offline", course.GetCourseType());
        }

        [Fact]
        public void AssignTeacherToCourse_WithValidIds_ShouldAssignTeacher()
        {
            // Arrange
            _service.CreateOnlineCourse("Test Course", "Test", 3, "Platform", "URL", false);
            var teacher = new Teacher { FirstName = "John", LastName = "Doe", Department = "CS" };
            _service.AddTeacher(teacher);

            var course = _service.GetAllCourses().First();
            var teacherId = _service.GetAllTeachers().First().Id;

            // Act
            var result = _service.AssignTeacherToCourse(course.Id, teacherId);

            // Assert
            Assert.True(result);
            var updatedCourse = _service.GetCourse(course.Id);
            Assert.NotNull(updatedCourse.Teacher);
            Assert.Equal("John Doe", updatedCourse.Teacher.FullName);
        }

        [Fact]
        public void AssignTeacherToCourse_WithInvalidIds_ShouldReturnFalse()
        {
            // Act
            var result = _service.AssignTeacherToCourse(999, 999);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void EnrollStudentInCourse_WithValidIds_ShouldEnrollStudent()
        {
            // Arrange
            _service.CreateOnlineCourse("Test Course", "Test", 3, "Platform", "URL", false);
            var student = new Student { FirstName = "Alice", LastName = "Smith", StudentId = "S001" };
            _service.AddStudent(student);

            var course = _service.GetAllCourses().First();
            var studentId = _service.GetAllStudents().First().Id;

            // Act
            var result = _service.EnrollStudentInCourse(course.Id, studentId);

            // Assert
            Assert.True(result);
            var courseStudents = _service.GetCourseStudents(course.Id);
            Assert.Single(courseStudents);
            Assert.Equal("Alice Smith", courseStudents.First().FullName);
        }

        [Fact]
        public void GetTeacherCourses_ShouldReturnCorrectCourses()
        {
            // Arrange
            var teacher = new Teacher { FirstName = "John", LastName = "Doe", Department = "CS" };
            _service.AddTeacher(teacher);
            _service.CreateOnlineCourse("Course 1", "Desc 1", 3, "Platform", "URL", false);
            _service.CreateOfflineCourse("Course 2", "Desc 2", 4, "Room 101", "Schedule", "Campus");

            var teacherId = _service.GetAllTeachers().First().Id;
            var course1 = _service.GetAllCourses().First(c => c.Name == "Course 1");
            var course2 = _service.GetAllCourses().First(c => c.Name == "Course 2");

            _service.AssignTeacherToCourse(course1.Id, teacherId);
            _service.AssignTeacherToCourse(course2.Id, teacherId);

            // Act
            var teacherCourses = _service.GetTeacherCourses(teacherId);

            // Assert
            Assert.Equal(2, teacherCourses.Count());
            Assert.Contains(teacherCourses, c => c.Name == "Course 1");
            Assert.Contains(teacherCourses, c => c.Name == "Course 2");
        }

        [Fact]
        public void RemoveCourse_ShouldRemoveCourseFromRepository()
        {
            // Arrange
            _service.CreateOnlineCourse("Test Course", "Test", 3, "Platform", "URL", false);
            var course = _service.GetAllCourses().First();

            // Act
            _service.RemoveCourse(course.Id);

            // Assert
            var courses = _service.GetAllCourses();
            Assert.Empty(courses);
        }

        [Theory]
        [InlineData("Online")]
        [InlineData("Offline")]
        public void Course_GetCourseType_ShouldReturnCorrectType(string expectedType)
        {
            // Arrange
            Course course = expectedType == "Online" 
                ? new OnlineCourse { Name = "Test" }
                : new OfflineCourse { Name = "Test" };

            // Act
            var courseType = course.GetCourseType();

            // Assert
            Assert.Equal(expectedType, courseType);
        }

        [Fact]
        public void GetCourseStudents_ShouldReturnStudentsEnrolledInCourse()
        {
            // Arrange
            _service.CreateOnlineCourse("Test Course", "Test", 3, "Platform", "URL", false);
            var student1 = new Student { FirstName = "Alice", LastName = "Smith", StudentId = "S001" };
            var student2 = new Student { FirstName = "Bob", LastName = "Johnson", StudentId = "S002" };
            _service.AddStudent(student1);
            _service.AddStudent(student2);

            var course = _service.GetAllCourses().First();
            var studentId1 = _service.GetAllStudents().First(s => s.StudentId == "S001").Id;
            var studentId2 = _service.GetAllStudents().First(s => s.StudentId == "S002").Id;

            _service.EnrollStudentInCourse(course.Id, studentId1);
            _service.EnrollStudentInCourse(course.Id, studentId2);

            // Act
            var courseStudents = _service.GetCourseStudents(course.Id);

            // Assert
            Assert.Equal(2, courseStudents.Count());
            Assert.Contains(courseStudents, s => s.FullName == "Alice Smith");
            Assert.Contains(courseStudents, s => s.FullName == "Bob Johnson");
        }
    }
}