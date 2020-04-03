using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Multicast
{
    public class MulticastListener
    {
        public IPAddress McastAddress { get; private set; }
        public int McastPort { get; private set; }
        public Socket McastSocket { get; private set; }
        public MulticastOption McastOption { get; private set; }

        public MulticastListener(string direccion, int puerto)
        {
            McastAddress = IPAddress.Parse(direccion);
            McastPort = puerto;
        }

        public void ImprimirInformacion()
        {
            Console.WriteLine("El grupo es: {0}", McastOption.Group.ToString());
            Console.WriteLine("La dirección IP es: {0}", McastOption.LocalAddress.ToString());
        }

        public void IniciarMulticast(string direccion)
        {
            try
            {
                McastSocket = new Socket(AddressFamily.InterNetwork,
                    SocketType.Dgram,
                    ProtocolType.Udp);
                // McastSocket.ExclusiveAddressUse = false;
                IPAddress ip = IPAddress.Parse(direccion);

                EndPoint endPointLocal = (EndPoint)new IPEndPoint(ip, McastPort);
                McastSocket.Bind(endPointLocal);

                // Define un objeto MulticastOption especificando 
                // la dirección del grupo multicast y la IPAddress local.
                // La dirección del grupo multicast es la misma que 
                // la dirección utilizada por el servidor.
                McastOption = new MulticastOption(McastAddress, ip);
                McastSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, McastOption);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("\nIniciar Multicast");
                Console.WriteLine(ex.ToString());
            }
        }

        public void RecibirMensajesMulticast()
        {
            bool terminado = false;
            byte[] bytes = new Byte[100];
            IPEndPoint grupoEP = new IPEndPoint(McastAddress, McastPort);
            EndPoint remotoEP = (EndPoint)new IPEndPoint(IPAddress.Any, 0);

            try
            {
                while(!terminado)
                {
                    Console.WriteLine("Esperando por paquetes multicast...");
                    
                    McastSocket.ReceiveFrom(bytes, ref remotoEP);

                    Console.WriteLine("Transmisión recibida desde {0} :\n {1}\n",
                        grupoEP.ToString(),
                        Encoding.UTF8.GetString(bytes, 0, bytes.Length));
                }

                McastSocket.Close();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Recibir mensajes");
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
