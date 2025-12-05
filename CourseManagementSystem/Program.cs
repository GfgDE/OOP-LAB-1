using CourseManagementSystem.Models;
using CourseManagementSystem.Repositories;
using CourseManagementSystem.Services;

namespace CourseManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var courseRepo = new CourseRepository();
            var teacherRepo = new TeacherRepository();
            var studentRepo = new StudentRepository();
            var service = new CourseManagementService(courseRepo, teacherRepo, studentRepo);

            InitializeTestData(service);

            DemonstrateSystem(service);
        }

        static void InitializeTestData(CourseManagementService service)
        {
            service.AddTeacher(new Teacher { FirstName = "Иван", LastName = "Петров", Department = "Информатика" });
            service.AddTeacher(new Teacher { FirstName = "Мария", LastName = "Сидорова", Department = "Математика" });

            service.AddStudent(new Student { FirstName = "Алексей", LastName = "Иванов", StudentId = "S001" });
            service.AddStudent(new Student { FirstName = "Елена", LastName = "Кузнецова", StudentId = "S002" });

            service.CreateOnlineCourse("C# Programming", "Основы программирования на C#", 3, "Microsoft Teams", "https://teams.com/csharp", false);
            service.CreateOfflineCourse("Высшая математика", "Продвинутая математика", 4, "Аудитория 101", "Пн/Ср 10:00-11:30", "Главный корпус");

            service.AssignTeacherToCourse(1, 1);
            service.AssignTeacherToCourse(2, 2);

            service.EnrollStudentInCourse(1, 1);
            service.EnrollStudentInCourse(1, 2);
            service.EnrollStudentInCourse(2, 2);
        }

        static void DemonstrateSystem(CourseManagementService service)
        {
            Console.WriteLine("=== СИСТЕМА УПРАВЛЕНИЯ КУРСАМИ ===");
            
            Console.WriteLine("\nВсе курсы:");
            foreach (var course in service.GetAllCourses())
            {
                Console.WriteLine($"{course.Id}. {course.Name} ({course.GetCourseType()}) - {course.Teacher?.FullName}");
            }

            Console.WriteLine("\nКурсы преподавателя Иван Петров:");
            foreach (var course in service.GetTeacherCourses(1))
            {
                Console.WriteLine($"{course.Name} - {course.Students.Count} студентов");
            }

            Console.WriteLine("\nСтуденты курса C# Programming:");
            foreach (var student in service.GetCourseStudents(1))
            {
                Console.WriteLine($"{student.FullName} ({student.StudentId})");
            }
        }
    }
}