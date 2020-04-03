using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Multicast
{
    public class MulticastSender
    {
        public IPAddress McastAddress { get; private set; }
        public int McastPort { get; private set; }
        public Socket McastSocket { get; private set; }
        
        public MulticastSender(string direccion, int puerto)
        {
            McastAddress = IPAddress.Parse(direccion);
            McastPort = puerto;
        }

        public void UnirseAGrupo(string direccion)
        {
            try
            {
                McastSocket = new Socket(AddressFamily.InterNetwork,
                    SocketType.Dgram,
                    ProtocolType.Udp);

                // Console.Write("\nIngresa la dirección local para enviar paquetes multicast");
                IPAddress ip = IPAddress.Parse(direccion);

                // Crea objeto IPEndpoint
                IPEndPoint IPlocal = new IPEndPoint(ip, 0);

                // Asocia endpoint al socket multicast
                McastSocket.Bind(IPlocal);

                // Define un objeto MulticastOption que especifica 
                // la dirección del grupo multicasty la dirección IP local.
                // La dirección del grupo multicast es la misma que la de Listener.
                MulticastOption multicastOption = new MulticastOption(McastAddress, ip);
                McastSocket.SetSocketOption(SocketOptionLevel.IP,
                    SocketOptionName.AddMembership,
                    multicastOption);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void TransmitirMensaje(string mensaje)
        {
            IPEndPoint endPoint;

            try
            {
                // Enviar paquetes multicast a los que escuchan
                endPoint = new IPEndPoint(McastAddress, McastPort);
                McastSocket.SendTo(UTF8Encoding.UTF8.GetBytes(mensaje), endPoint);
                Console.WriteLine("Datos multicast enviados...");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            McastSocket.Close();
        }
    }
}
