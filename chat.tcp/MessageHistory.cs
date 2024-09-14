using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chat.tcp
{
    public class MessageHistory
    {
        private string filePath = "chat_history.txt";

        public void SaveMessage(string message)
        {
            File.AppendAllText(filePath, message + "\n");
        }

        public void ShowHistory()
        {
            if (File.Exists(filePath))
            {
                string[] history = File.ReadAllLines(filePath);
                foreach (var message in history)
                {
                    Console.WriteLine(message);
                }
            }
        }
    }
}
