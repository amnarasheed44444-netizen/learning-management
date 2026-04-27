using LMS.Database;
using MySql.Data.MySqlClient;
using System;

namespace LMS.Services
{
    class QuizService
    {
        public void CreateQuiz()
        {
            Console.Write("Course: ");
            string c = Console.ReadLine();

            Console.Write("Question: ");
            string q = Console.ReadLine();

            Console.Write("Answer: ");
            string a = Console.ReadLine();

            using (var con = DBHelper.OpenConnection())
            {
                var cmd = new MySqlCommand(
                    "INSERT INTO Quizzes(Course,Question,Answer) VALUES(@c,@q,@a)", con);

                cmd.Parameters.AddWithValue("@c", c);
                cmd.Parameters.AddWithValue("@q", q);
                cmd.Parameters.AddWithValue("@a", a);

                cmd.ExecuteNonQuery();
            }

            Console.WriteLine("✅ Quiz Added");
        }

        public void ViewQuizzes()
        {
            using (var con = DBHelper.OpenConnection())
            {
                var cmd = new MySqlCommand("SELECT * FROM Quizzes", con);
                var r = cmd.ExecuteReader();

                while (r.Read())
                {
                    Console.WriteLine("Q: " + r["Question"]);
                }
            }
        }

        public void TakeQuiz()
        {
            int score = 0;

            using (var con = DBHelper.GetConnection())
            {
                con.Open();

                var cmd = new MySqlCommand("SELECT * FROM Quizzes", con);
                var r = cmd.ExecuteReader();

                while (r.Read())
                {
                    Console.WriteLine(r["Question"]);
                    string ans = Console.ReadLine();

                    if (ans == r["Answer"].ToString())
                        score++;
                }
            }

            Console.WriteLine("Score: " + score);
        }
    }
}