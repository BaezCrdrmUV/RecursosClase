using System;

namespace Multicast
{
    class Program
    {
        static void Main(string[] args)
        {
            string grupo = "224.168.100.2";
            Console.WriteLine("Ingresa tu dirección IP");
            string ip = "192.168.100.5";
            // string ip = Console.ReadLine();
            Console.WriteLine("Ingresa el puerto");
            int puerto = 11000;
            // int puerto = Int32.Parse(Console.ReadLine());

            if(args[0].ToLower() == "sender")
            {
                MulticastSender sender = new MulticastSender(grupo, puerto);
                sender.UnirseAGrupo(ip);
                sender.TransmitirMensaje("Hola mundo");
            }
            else if(args[0].ToLower() == "listener")
            {
                MulticastListener listener = new MulticastListener(grupo, puerto);
                listener.IniciarMulticast(ip);
                listener.ImprimirInformacion();
                listener.RecibirMensajesMulticast();
            }           
        }
    }
}
