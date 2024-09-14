using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace chat.tcp
{
    public class ChatServer
    {
        private TcpListener listener;
        private List<TcpClient> clients = new List<TcpClient>();

        public void StartServer()
        {
            listener = new TcpListener(IPAddress.Any, 5000);
            listener.Start();
            Console.WriteLine("Сервер запущений!");

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                clients.Add(client);
                Task.Run(() => HandleClient(client));
            }
        }

        private void HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            string message;
            while ((message = reader.ReadLine()) != null)
            {
                BroadcastMessage(message); 
            }
        }

        private void BroadcastMessage(string message)
        {
            foreach (var client in clients)
            {
                NetworkStream stream = client.GetStream();
                StreamWriter writer = new StreamWriter(stream);
                writer.WriteLine(message);
                writer.Flush();
            }
        }
    }
    public class ChatClient
    {
        private TcpClient client;

        public void ConnectToServer(string serverIp)
        {
            client = new TcpClient(serverIp, 5000);
            Task.Run(() => ListenForMessages());
        }

        private void ListenForMessages()
        {
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            string message;
            while ((message = reader.ReadLine()) != null)
            {
                Console.WriteLine(message);
            }
        }

        public void SendMessage(string message)
        {
            NetworkStream stream = client.GetStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine(message);
            writer.Flush();
        }
    }


}
