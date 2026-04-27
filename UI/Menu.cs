using LMS.Models;
using LMS.Services;
using System;

namespace LMS.UI
{
    class Menu
    {
        public static void Show(User user)
        {
            var course = new CourseService();
            var enroll = new EnrollmentService();
            var assign = new AssignmentService();
            var quiz = new QuizService();
            var users = new UserService();

            while (true)
            {
                Console.WriteLine("\n===== LMS MENU =====");
                Console.WriteLine($"Logged in as: {user.Username} ({user.Role})");
                Console.WriteLine("-----------------------------------");

                // ================= ADMIN MENU =================
                if (user.Role == "admin")
                {
                    Console.WriteLine("1. View Users");
                    Console.WriteLine("2. Delete User");
                    Console.WriteLine("3. Add Course");
                    Console.WriteLine("4. View Courses");
                    Console.WriteLine("5. Delete Course");
                }

                // ================= TEACHER MENU =================
                else if (user.Role == "teacher")
                {
                    Console.WriteLine("1. Add Course");
                    Console.WriteLine("2. Create Assignment");
                    Console.WriteLine("3. Grade Assignment");
                    Console.WriteLine("4. Create Quiz");
                    Console.WriteLine("5. View Courses");
                }

                // ================= STUDENT MENU =================
                else if (user.Role == "student")
                {
                    Console.WriteLine("1. View Courses");
                    Console.WriteLine("2. Enroll in Course");
                    Console.WriteLine("3. Submit Assignment");
                    Console.WriteLine("4. Take Quiz");
                }

                Console.WriteLine("0. Logout");
                Console.Write("Enter choice: ");
                string c = Console.ReadLine();

                if (c == "0")
                {
                    Console.WriteLine("👋 Logged out successfully");
                    break;
                }

                // ================= ADMIN ACTIONS =================
                if (user.Role == "admin")
                {
                    switch (c)
                    {
                        case "1": users.ViewUsers(); break;
                        case "2": users.DeleteUser(); break;
                        case "3": course.AddCourse(user.Username); break;
                        case "4": course.ViewCourses(); break;
                        case "5": course.DeleteCourse(); break;
                    }
                }

                // ================= TEACHER ACTIONS =================
                else if (user.Role == "teacher")
                {
                    switch (c)
                    {
                        case "1": course.AddCourse(user.Username); break;
                        case "2": assign.CreateAssignment(); break;
                        case "3": assign.GradeAssignment(); break;
                        case "4": quiz.CreateQuiz(); break;
                        case "5": course.ViewCourses(); break;
                    }
                }

                // ================= STUDENT ACTIONS =================
                else if (user.Role == "student")
                {
                    switch (c)
                    {
                        case "1": course.ViewCourses(); break;
                        case "2": enroll.Enroll(user.Username); break;
                        case "3": assign.SubmitAssignment(user.Username); break;
                        case "4": quiz.TakeQuiz(); break;
                    }
                }
            }
        }
    }
}