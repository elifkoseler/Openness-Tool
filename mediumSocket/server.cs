using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{

    class Server
    {

        // Main Method 
        static void Main(string[] args)
        {
            //Run server until it close itself
            RunServer();
        }

        public static void RunServer()
        {
            // Establish the local endpoint  
            // for the socket. Dns.GetHostName 
            // returns the name of the host  
            // running the application. 
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 5005);
            
            // Creation TCP/IP Socket using  
            // Socket Class Costructor 
            Socket serverSocket = new Socket(ipAddr.AddressFamily,
                         SocketType.Stream, ProtocolType.Tcp);

            // Associate network address with the socket
            serverSocket.Bind(localEndPoint);

            // #clients at the same time
            serverSocket.Listen(2);

            Socket sender = new Socket(ipAddr.AddressFamily,
                           SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint localEndPoint2 = new IPEndPoint(ipAddr, 5010);


            sender.Connect(localEndPoint2);

            Console.WriteLine("Socket connected to server -> 5010 ");

            byte[] messageSent = Encoding.ASCII.GetBytes("Client here.");
            int byteSent = sender.Send(messageSent);

            while (true)
            {
                Socket clientSocket = null;
                try
                {
                    while (true)
                    {

                        Console.WriteLine("Server Running at: 5005 ..");
                        Console.WriteLine(ipAddr);

                        // Suspend while waiting for 
                        // incoming connection Using  
                        // Accept() method the server  
                        // will accept connection of client 
                        clientSocket = serverSocket.Accept();

                        byte[] bytes = new Byte[1024];
                        string data = null;

                        while (true)
                        {
                            while (true)
                            {
                                int numByte = clientSocket.Receive(bytes);

                                data += Encoding.ASCII.GetString(bytes,
                                                           0, numByte);

                                if (data.IndexOf(".") > -1)
                                    break;
                            }

                            Console.WriteLine(data);
                            byte[] toServer = Encoding.ASCII.GetBytes(data);
                            int toServerByte = sender.Send(toServer);
                            data = "";
                            /*
                            string resText = "";
                            switch (tokens[0])
                            {
                                case "ADD":
                                    resText = "Add method will be called.";
                                    break;
                                case "CON":
                                    resText = "Connection method will be called.";
                                    break;
                                case "EDIT":
                                    resText = "Edit method will be called.";
                                    break;
                                case "COM":
                                    resText = "Compile method will be called.";
                                    break;
                                default:
                                    resText = "Method cannot be found.";
                                    break;
                            }*/
                            Console.WriteLine("Message forwarded.\n");

                            byte[] message1 = Encoding.ASCII.GetBytes("Success.");
                            clientSocket.Send(message1);
                        }

                        byte[] message = Encoding.ASCII.GetBytes("Test Server");

                        // Send a message to Client  
                        // using Send() method 
                        clientSocket.Send(message);

                        // Close client Socket using the 
                        // Close() method. After closing, 
                        // we can use the closed Socket  
                        // for a new Client Connection 
                        clientSocket.Shutdown(SocketShutdown.Both);
                        clientSocket.Close();
                    }
                }

                catch (SocketException e)
                {
                    Console.WriteLine("Client has disconnected.\n");
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                }

                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
    }
}
