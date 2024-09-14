using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chat.tcp
{
    public class UserService
    {
        private string filePath = "users.txt"; 

        public void Register(string username, string password)
        {
            if (!File.Exists(filePath) || !File.ReadAllLines(filePath).Any(line => line.StartsWith(username)))
            {
                File.AppendAllText(filePath, $"{username}:{password}\n"); 
                Console.WriteLine("Реєстрація успішна!");
            }
            else
            {
                Console.WriteLine("Користувач з таким ім'ям вже існує.");
            }
        }

        public bool Login(string username, string password)
        {
            if (File.Exists(filePath))
            {
                string[] users = File.ReadAllLines(filePath);
                foreach (var user in users)
                {
                    string[] parts = user.Split(':');
                    if (parts[0] == username && parts[1] == password)
                    {
                        Console.WriteLine("Вхід успішний!");
                        return true;
                    }
                }
            }
            Console.WriteLine("Невірний логін або пароль.");
            return false;
        }
    }

}
