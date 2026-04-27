using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace LMS.Database
{
    class DBHelper
    {
        // 🔐 Connection String (XAMPP MySQL)
        private static string connectionString =
            "server=127.0.0.1;port=3306;database=learning_management_db;user=root;password=;";

        // 🔌 Get Connection
        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        // ✅ OPEN CONNECTION SAFELY
        public static MySqlConnection OpenConnection()
        {
            var con = GetConnection();

            try
            {
                con.Open();

                // 🔥 IMPORTANT FIX: force database selection
                con.ChangeDatabase("learning_management_db");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Database Connection Error: " + ex.Message);
            }

            return con;
        }

        // 🔴 CLOSE CONNECTION SAFELY
        public static void CloseConnection(MySqlConnection con)
        {
            try
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error Closing Connection: " + ex.Message);
            }
        }

        // 🧠 EXECUTE SELECT (RETURN TABLE)
        public static DataTable ExecuteQuery(string query)
        {
            DataTable table = new DataTable();

            using (var con = OpenConnection())
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(table);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("❌ Query Error: " + ex.Message);
                }
                finally
                {
                    CloseConnection(con);
                }
            }

            return table;
        }

        // ✏️ EXECUTE INSERT / UPDATE / DELETE
        public static int ExecuteNonQuery(string query)
        {
            int rows = 0;

            using (var con = OpenConnection())
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    rows = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("❌ Execute Error: " + ex.Message);
                }
                finally
                {
                    CloseConnection(con);
                }
            }

            return rows;
        }

        // 🔢 EXECUTE SCALAR (Single Value)
        public static object ExecuteScalar(string query)
        {
            object result = null;

            using (var con = OpenConnection())
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    result = cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("❌ Scalar Error: " + ex.Message);
                }
                finally
                {
                    CloseConnection(con);
                }
            }

            return result;
        }
    }
}