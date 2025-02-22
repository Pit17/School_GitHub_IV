using System;
using System.Net;
using System.Text;
using System.Net.Sockets;

namespace ConsoleAppServer
{
    class Program 
    {
        static void Main(string[] args)
        {
            StartServer();

        }

        public static void StartServer()
        {
            IPHostEntry host = Dns.GetHostEntry("localhost");//ip
            IPAddress iPAddress = host.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(iPAddress, 11000);//porta

            try
            {
                Socket listener = new Socket(iPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);//socket per la connessione protocollo tcp, tipo stream, ipaddress family
                listener.Bind(localEndPoint);//associa il socket all'endpoint
                listener.Listen(10);//ascolta
                Console.WriteLine("Waiting....");//attesa
                Socket handler = listener.Accept();//accetta la connessione
                string data = null;
                byte[] bytes = null;
                while (true)//ciclo per ricevere il messaggio
                {
                    bytes = new byte[1024];//messaggio
                    int bytesRec = handler.Receive(bytes);//lunghezza del messaggio
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);//decodifica
                    if (data.IndexOf("<EOF>") > -1)//se il messaggio contiene <EOF> esce
                    {
                        break;
                    }
                }
                Console.WriteLine("Text received : {0}", data);//stampa il messaggio

                byte[] msg = Encoding.ASCII.GetBytes(data);//codifica il messaggio
                handler.Send(msg);//invia il messaggio
                handler.Shutdown(SocketShutdown.Both);//chiude la connessione
                handler.Close();//chiude il socket
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());//stampa l'errore

            }
            Console.WriteLine("\n Press any key to continue...");//attesa
            Console.ReadKey();
        }
    
    }

}