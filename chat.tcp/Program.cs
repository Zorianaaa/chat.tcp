
using chat.tcp;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введіть 'server', щоб запустити сервер, або 'client', щоб підключитися як клієнт:");
        string choice = Console.ReadLine();

        if (choice.ToLower() == "server")
        {
            ChatServer server = new ChatServer();

            UdpDiscovery udpDiscovery = new UdpDiscovery();
            Task.Run(() =>
            {
                while (true)
                {
                    udpDiscovery.BroadcastServerPresence();
                    Thread.Sleep(5000); 
                }
            });

            server.StartServer();
        }
        else if (choice.ToLower() == "client")
        {
            Console.WriteLine("Клієнт чату");

            UdpClientDiscovery udpDiscovery = new UdpClientDiscovery();
            Task.Run(() => udpDiscovery.FindServers());

            ChatClient client = new ChatClient();
            Console.WriteLine("Введіть IP-адресу сервера:");
            string serverIp = Console.ReadLine();

            client.ConnectToServer(serverIp);

            Console.WriteLine("Введіть ваше ім'я:");
            string username = Console.ReadLine();
            client.SendMessage(username);

            while (true)
            {
                Console.WriteLine("Введіть повідомлення:");
                string message = Console.ReadLine();
                client.SendMessage(message); 
            }
        }
        else
        {
            Console.WriteLine("Невірний вибір. Будь ласка, введіть 'server' або 'client'.");
        }
    }
}


