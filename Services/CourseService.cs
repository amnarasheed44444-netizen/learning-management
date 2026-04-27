using LMS.Database;
using MySql.Data.MySqlClient;
using System;

namespace LMS.Services
{
    class CourseService
    {
        // ================= ADD COURSE =================
        public void AddCourse(string teacher)
        {
            Console.Write("Course Title: ");
            string title = Console.ReadLine();

            using (var con = DBHelper.GetConnection())
            {
                con.Open();

                string q = "INSERT INTO Courses(Title,Teacher) VALUES(@t,@teach)";
                var cmd = new MySqlCommand(q, con);

                cmd.Parameters.AddWithValue("@t", title);
                cmd.Parameters.AddWithValue("@teach", teacher);

                cmd.ExecuteNonQuery();
            }

            Console.WriteLine("✅ Course Added Successfully");
        }

        // ================= VIEW COURSES =================
        public void ViewCourses()
        {
            using (var con = DBHelper.GetConnection())
            {
                con.Open();

                string q = "SELECT * FROM Courses";
                var cmd = new MySqlCommand(q, con);

                var reader = cmd.ExecuteReader();

                Console.WriteLine("\n📚 AVAILABLE COURSES:");
                Console.WriteLine("--------------------------------");

                while (reader.Read())
                {
                    Console.WriteLine(
                        $"ID: {reader["Id"]} | Title: {reader["Title"]} | Teacher: {reader["Teacher"]}"
                    );
                }
            }
        }

        // ================= DELETE COURSE (ADMIN ONLY) =================
        public void DeleteCourse()
        {
            Console.Write("Enter Course ID to Delete: ");
            int id = int.Parse(Console.ReadLine());

            using (var con = DBHelper.GetConnection())
            {
                con.Open();

                string q = "DELETE FROM Courses WHERE Id=@id";
                var cmd = new MySqlCommand(q, con);

                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }

            Console.WriteLine("❌ Course Deleted Successfully");
        }
    }
}