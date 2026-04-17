using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Learning_Management
{
    // ===================== MODELS =====================
    class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // student, teacher, admin
    }

    class Course
    {
        public string Title { get; set; }
        public string Teacher { get; set; }
    }

    class Assignment
    {
        public string Course { get; set; }
        public string Title { get; set; }
        public string Student { get; set; }
        public string Submission { get; set; }
        public int Marks { get; set; }
    }

    class Quiz
    {
        public string Course { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }

    // ===================== FILE HELPER =====================
    class FileHelper
    {
        public static void Write(string path, string data)
        {
            File.AppendAllText(path, data + Environment.NewLine);
        }

        public static List<string> Read(string path)
        {
            if (!File.Exists(path)) return new List<string>();
            return File.ReadAllLines(path).ToList();
        }
    }

    // ===================== SYSTEM =====================
    class LMS
    {
        string userFile = "users.txt";
        string courseFile = "courses.txt";
        string assignmentFile = "assignments.txt";
        string quizFile = "quizzes.txt";

        public User CurrentUser = null;

        // ---------- AUTH ----------
        public void Register()
        {
            Console.Write("Username: ");
            string u = Console.ReadLine();

            Console.Write("Password: ");
            string p = Console.ReadLine();

            Console.Write("Role (student/teacher/admin): ");
            string r = Console.ReadLine();

            FileHelper.Write(userFile, $"{u},{p},{r}");
            Console.WriteLine("✅ Registered Successfully!");
        }

        public void Login()
        {
            Console.Write("Username: ");
            string u = Console.ReadLine();

            Console.Write("Password: ");
            string p = Console.ReadLine();

            var users = FileHelper.Read(userFile);

            foreach (var line in users)
            {
                var data = line.Split(',');
                if (data[0] == u && data[1] == p)
                {
                    CurrentUser = new User { Username = u, Password = p, Role = data[2] };
                    Console.WriteLine($"✅ Login Successful! Role: {CurrentUser.Role}");
                    return;
                }
            }

            Console.WriteLine("❌ Invalid Credentials");
        }

        // ---------- COURSES ----------
        public void AddCourse()
        {
            if (CurrentUser.Role != "admin" && CurrentUser.Role != "teacher")
            {
                Console.WriteLine("❌ Access Denied");
                return;
            }

            Console.Write("Course Title: ");
            string title = Console.ReadLine();

            FileHelper.Write(courseFile, $"{title},{CurrentUser.Username}");
            Console.WriteLine("✅ Course Added");
        }

        public void ViewCourses()
        {
            var courses = FileHelper.Read(courseFile);
            Console.WriteLine("\n📚 COURSES:");

            foreach (var c in courses)
                Console.WriteLine(" - " + c);
        }

        // ---------- ASSIGNMENTS ----------
        public void CreateAssignment()
        {
            Console.Write("Course: ");
            string c = Console.ReadLine();

            Console.Write("Assignment Title: ");
            string t = Console.ReadLine();

            FileHelper.Write(assignmentFile, $"{c},{t},,0");
            Console.WriteLine("✅ Assignment Created");
        }

        public void SubmitAssignment()
        {
            Console.Write("Course: ");
            string c = Console.ReadLine();

            Console.Write("Assignment Title: ");
            string t = Console.ReadLine();

            Console.Write("Submission: ");
            string s = Console.ReadLine();

            FileHelper.Write(assignmentFile, $"{c},{t},{CurrentUser.Username},{s},0");
            Console.WriteLine("✅ Submitted");
        }

        // ---------- QUIZ ----------
        public void CreateQuiz()
        {
            Console.Write("Course: ");
            string c = Console.ReadLine();

            Console.Write("Question: ");
            string q = Console.ReadLine();

            Console.Write("Answer: ");
            string a = Console.ReadLine();

            FileHelper.Write(quizFile, $"{c},{q},{a}");
            Console.WriteLine("✅ Quiz Added");
        }

        public void TakeQuiz()
        {
            var quizzes = FileHelper.Read(quizFile);

            int score = 0;

            foreach (var q in quizzes)
            {
                var data = q.Split(',');

                Console.WriteLine("\nQ: " + data[1]);
                Console.Write("Your Answer: ");
                string ans = Console.ReadLine();

                if (ans == data[2])
                    score++;
            }

            Console.WriteLine($"\n🎯 Final Score: {score}/{quizzes.Count}");
        }

        // ---------- MENU ----------
        public void Menu()
        {
            while (true)
            {
                Console.WriteLine("\n===== LMS MENU =====");
                Console.WriteLine("1. Add Course");
                Console.WriteLine("2. View Courses");
                Console.WriteLine("3. Create Assignment");
                Console.WriteLine("4. Submit Assignment");
                Console.WriteLine("5. Create Quiz");
                Console.WriteLine("6. Take Quiz");
                Console.WriteLine("7. Logout");
                Console.Write("Choose: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddCourse(); break;
                    case "2": ViewCourses(); break;
                    case "3": CreateAssignment(); break;
                    case "4": SubmitAssignment(); break;
                    case "5": CreateQuiz(); break;
                    case "6": TakeQuiz(); break;
                    case "7": CurrentUser = null; return;
                }
            }
        }
    }

    // ===================== MAIN =====================
    class Program
    {
        static void Main(string[] args)
        {
            LMS lms = new LMS();

            while (true)
            {
                Console.WriteLine("\n===== LOGIN SYSTEM =====");
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit");
                Console.Write("Select: ");

                string choice = Console.ReadLine();

                if (choice == "1")
                    lms.Register();

                else if (choice == "2")
                {
                    lms.Login();
                    if (lms.CurrentUser != null)
                        lms.Menu();
                }

                else if (choice == "3")
                    break;
            }
        }
    }
}