using LMS.Database;
using MySql.Data.MySqlClient;
using System;

namespace LMS.Services
{
    class AssignmentService
    {
        public void CreateAssignment()
        {
            Console.Write("Course: ");
            string c = Console.ReadLine();

            Console.Write("Title: ");
            string t = Console.ReadLine();

            Console.Write("Deadline: ");
            string d = Console.ReadLine();

            using (var con = DBHelper.GetConnection())
            {
                con.Open();

                string q = "INSERT INTO Assignments(Course,Title,Deadline) VALUES(@c,@t,@d)";
                var cmd = new MySqlCommand(q, con);

                cmd.Parameters.AddWithValue("@c", c);
                cmd.Parameters.AddWithValue("@t", t);
                cmd.Parameters.AddWithValue("@d", d);

                cmd.ExecuteNonQuery();
            }

            Console.WriteLine("✅ Created");
        }

        public void ViewAssignments()
        {
            using (var con = DBHelper.GetConnection())
            {
                con.Open();

                var cmd = new MySqlCommand("SELECT * FROM Assignments", con);
                var r = cmd.ExecuteReader();

                while (r.Read())
                {
                    Console.WriteLine($"{r["Id"]} | {r["Title"]} | {r["Deadline"]}");
                }
            }
        }

        public void SubmitAssignment(string student)
        {
            Console.Write("Assignment ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Submission: ");
            string s = Console.ReadLine();

            using (var con = DBHelper.GetConnection())
            {
                con.Open();

                var cmd = new MySqlCommand(
                    "UPDATE Assignments SET Student=@s,Submission=@sub WHERE Id=@id", con);

                cmd.Parameters.AddWithValue("@s", student);
                cmd.Parameters.AddWithValue("@sub", s);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }

            Console.WriteLine("✅ Submitted");
        }

        public void GradeAssignment()
        {
            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Marks: ");
            int m = int.Parse(Console.ReadLine());

            using (var con = DBHelper.GetConnection())
            {
                con.Open();

                var cmd = new MySqlCommand("UPDATE Assignments SET Marks=@m WHERE Id=@id", con);
                cmd.Parameters.AddWithValue("@m", m);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }

            Console.WriteLine("🎯 Graded");
        }
    }
}