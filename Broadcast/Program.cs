using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Broadcast
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args[0] == "sender")
            {
                Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                IPAddress broadcast = IPAddress.Parse("192.168.100.5");

                byte[] sendbuf = Encoding.UTF8.GetBytes("Hola Mundo");
                IPEndPoint ep = new IPEndPoint(broadcast, 11000);

                s.SendTo(sendbuf, ep);

                Console.WriteLine("Message sent to the broadcast address");
            } else 
            {
                UDPListener udpl = new UDPListener();
                udpl.StartListener();
            }
        }
    }
}
