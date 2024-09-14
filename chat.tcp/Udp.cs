using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace chat.tcp
{
    public class UdpDiscovery
    {
        public void BroadcastServerPresence()
        {
            UdpClient udpClient = new UdpClient();
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Broadcast, 5001);
            byte[] data = Encoding.ASCII.GetBytes("Чат-сервер");
            udpClient.Send(data, data.Length, endPoint);
            udpClient.Close();
        }
    }

    public class UdpClientDiscovery
    {
        public void FindServers()
        {
            UdpClient udpClient = new UdpClient(5001);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 5001);
            while (true)
            {
                byte[] data = udpClient.Receive(ref endPoint);
                string message = Encoding.ASCII.GetString(data);
                Console.WriteLine($"Знайдено сервер: {message} від {endPoint.Address}");
            }
        }
    }


}
