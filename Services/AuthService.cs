using LMS.Database;
using LMS.Models;
using MySql.Data.MySqlClient;
using System;

namespace LMS.Services
{
    class AuthService
    {
        public User Login()
        {
            Console.Write("Username: ");
            string u = Console.ReadLine();

            Console.Write("Password: ");
            string p = Console.ReadLine();

            using (var con = DBHelper.GetConnection())
            {
                con.Open();

                string query = "SELECT * FROM Users WHERE Username=@u AND Password=@p";
                var cmd = new MySqlCommand(query, con);

                cmd.Parameters.AddWithValue("@u", u);
                cmd.Parameters.AddWithValue("@p", p);

                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new User
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Username = reader["Username"].ToString(),
                        Role = reader["Role"].ToString()
                    };
                }
            }

            Console.WriteLine("❌ Invalid Login");
            return null;
        }

        public void Register()
        {
            Console.Write("Username: ");
            string u = Console.ReadLine();

            Console.Write("Password: ");
            string p = Console.ReadLine();

            Console.Write("Role (admin/teacher/student): ");
            string r = Console.ReadLine();

            // 🔒 ONLY ONE ADMIN ALLOWED
            if (r == "admin" && u != "admin")
            {
                Console.WriteLine("❌ Only 'admin' username allowed for admin role");
                return;
            }

            using (var con = DBHelper.GetConnection())
            {
                con.Open();

                string q = "INSERT INTO Users(Username,Password,Role) VALUES(@u,@p,@r)";
                var cmd = new MySql.Data.MySqlClient.MySqlCommand(q, con);

                cmd.Parameters.AddWithValue("@u", u);
                cmd.Parameters.AddWithValue("@p", p);
                cmd.Parameters.AddWithValue("@r", r);

                cmd.ExecuteNonQuery();
            }

            Console.WriteLine("✅ Registered");
        }
    }
}