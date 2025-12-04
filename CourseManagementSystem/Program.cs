using System;
using CourseManagementSystem.Models;
using CourseManagementSystem.Repositories;
using CourseManagementSystem.Services;

namespace CourseManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            // Инициализация репозиториев и сервиса
            var courseRepo = new CourseRepository();
            var teacherRepo = new TeacherRepository();
            var studentRepo = new StudentRepository();
            var service = new CourseManagementService(courseRepo, teacherRepo, studentRepo);

            // Добавление тестовых данных
            InitializeTestData(service);

            // Демонстрация работы системы
            DemonstrateSystem(service);
        }

        static void InitializeTestData(CourseManagementService service)
        {
            // Добавляем преподавателей
            service.AddTeacher(new Teacher { FirstName = "Иван", LastName = "Петров", Department = "Информатика" });
            service.AddTeacher(new Teacher { FirstName = "Мария", LastName = "Сидорова", Department = "Математика" });

            // Добавляем студентов
            service.AddStudent(new Student { FirstName = "Алексей", LastName = "Иванов", StudentId = "S001" });
            service.AddStudent(new Student { FirstName = "Елена", LastName = "Кузнецова", StudentId = "S002" });

            // Создаем курсы
            service.CreateOnlineCourse("C# Programming", "Основы программирования на C#", 3, "Microsoft Teams", "https://teams.com/csharp", false);
            service.CreateOfflineCourse("Высшая математика", "Продвинутая математика", 4, "Аудитория 101", "Пн/Ср 10:00-11:30", "Главный корпус");

            // Назначаем преподавателей на курсы
            service.AssignTeacherToCourse(1, 1); // C# Programming -> Иван Петров
            service.AssignTeacherToCourse(2, 2); // Высшая математика -> Мария Сидорова

            // Записываем студентов на курсы
            service.EnrollStudentInCourse(1, 1); // Алексей Иванов на C# Programming
            service.EnrollStudentInCourse(1, 2); // Елена Кузнецова на C# Programming
            service.EnrollStudentInCourse(2, 2); // Елена Кузнецова на Высшую математику
        }

        static void DemonstrateSystem(CourseManagementService service)
        {
            Console.WriteLine("=== СИСТЕМА УПРАВЛЕНИЯ КУРСАМИ ===");
            
            // Показываем все курсы
            Console.WriteLine("\nВсе курсы:");
            foreach (var course in service.GetAllCourses())
            {
                Console.WriteLine($"{course.Id}. {course.Name} ({course.GetCourseType()}) - {course.Teacher?.FullName}");
            }

            // Показываем курсы преподавателя
            Console.WriteLine("\nКурсы преподавателя Иван Петров:");
            foreach (var course in service.GetTeacherCourses(1))
            {
                Console.WriteLine($"{course.Name} - {course.Students.Count} студентов");
            }

            // Показываем студентов курса
            Console.WriteLine("\nСтуденты курса C# Programming:");
            foreach (var student in service.GetCourseStudents(1))
            {
                Console.WriteLine($"{student.FullName} ({student.StudentId})");
            }
        }
    }
}