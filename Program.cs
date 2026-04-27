using LMS.Services;
using LMS.UI;
using System;

namespace LMS
{
    class Program
    {
        static void Main()
        {
            AuthService auth = new AuthService();

            while (true)
            {
                Console.WriteLine("\n1. Register\n2. Login\n3. Exit");
                string c = Console.ReadLine();

                if (c == "1") auth.Register();

                else if (c == "2")
                {
                    var user = auth.Login();
                    if (user != null)
                        Menu.Show(user);
                }

                else break;
            }
        }
    }
}