using LMS.Database;
using MySql.Data.MySqlClient;
using System;

namespace LMS.Services
{
    class EnrollmentService
    {
        public void Enroll(string student)
        {
            Console.Write("Course ID: ");
            int id = int.Parse(Console.ReadLine());

            using (var con = DBHelper.GetConnection())
            {
                con.Open();

                string q = "INSERT INTO Enrollments(Student,CourseId) VALUES(@s,@c)";
                var cmd = new MySqlCommand(q, con);

                cmd.Parameters.AddWithValue("@s", student);
                cmd.Parameters.AddWithValue("@c", id);

                cmd.ExecuteNonQuery();
            }

            Console.WriteLine("✅ Enrolled");
        }
    }
}