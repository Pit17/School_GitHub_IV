using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
namespace ConsoleAppClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StartClient();
        }

        public static void StartClient()
        {
            byte[] bytes = new byte[1024];// Data buffer per i dati 

            try
            {
                IPHostEntry host = Dns.GetHostEntry("localhost");// IP preso dal file di configurazione
                IPAddress ipAddress = host.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);// Creazione del socket

                Socket sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);// Creazione del socket

                try
                {
                    sender.Connect(remoteEP);// Connessione al socket
                    Console.WriteLine("Soclet connected to {0}", sender.RemoteEndPoint.ToString());// Stampa a video del messaggio
                    byte[] msg = Encoding.ASCII.GetBytes("Ventura + Jessica = <3 <EOF>");// Messaggio da inviare
                    int bytesSent = sender.Send(msg);// Invio del messaggio
                    int bytesRec = sender.Receive(bytes);// Ricezione del messaggio
                    Console.WriteLine("Echoed test = {0}", Encoding.ASCII.GetString(bytes, 0, bytesRec));// Stampa a video del messaggio
                    sender.Shutdown(SocketShutdown.Both);// Chiusura del socket
                    sender.Close();

                }
                catch (ArgumentNullException ane)// Gestione delle eccezioni
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {

                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }
            }catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("\n Press any key...");// Messaggio a video
            Console.ReadKey();
        }
    }
}
