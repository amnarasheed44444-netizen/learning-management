using LMS.Database;
using MySql.Data.MySqlClient;
using System;

namespace LMS.Services
{
    class UserService
    {
        public void ViewUsers()
        {
            using (var con = DBHelper.OpenConnection())
            {
                var cmd = new MySqlCommand("SELECT * FROM Users", con);
                var r = cmd.ExecuteReader();

                while (r.Read())
                {
                    Console.WriteLine($"{r["Id"]} | {r["Username"]} | {r["Role"]}");
                }
            }
        }

        public void DeleteUser()
        {
            Console.Write("User ID: ");
            int id = int.Parse(Console.ReadLine());

            using (var con = DBHelper.OpenConnection())
            {
                var cmd = new MySqlCommand("DELETE FROM Users WHERE Id=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }

            Console.WriteLine("❌ Deleted");
        }
    }
}