using Siemens.Engineering;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TiaCloud
{
    class Server
    {
        public static void RunServer(Project project)
        {
            // Establish the local endpoint  
            // for the socket. Dns.GetHostName 
            // returns the name of the host  
            // running the application. 
            string data = null;
            string dataToSend = "empty";
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 5010);

            // Creation TCP/IP Socket using  
            // Socket Class Costructor 
            Socket serverSocket = new Socket(ipAddr.AddressFamily,
                         SocketType.Stream, ProtocolType.Tcp);

            // Associate network address with the socket
            serverSocket.Bind(localEndPoint);

            // #clients at the same time
            serverSocket.Listen(2);

            while (true)
            {
                Socket clientSocket = null;
                try
                {
                    while (true)
                    {

                        Console.WriteLine("Server Running at 5010 .. ");
                        //Console.Write("Server is starting to listen:  ");
                        Console.WriteLine(ipAddr);


                        // Suspend while waiting for 
                        // incoming connection Using  
                        // Accept() method the server  
                        // will accept connection of client 
                        clientSocket = serverSocket.Accept();
                        clientSocket.NoDelay = true;

                        byte[] bytes = new Byte[1024];
                        data = null;
                        byte[] message1 = new Byte[1024];
                        while (true)
                        {
                            while (true)
                            {
                                int numByte = clientSocket.Receive(bytes);
                                data = "";
                                data += Encoding.ASCII.GetString(bytes,0, numByte);

                                //tokens = data.Split('_');

                                if (data.IndexOf(".") > -1)
                                    break;
                            }
                            //Console.WriteLine("Code: {0} , Info: {1} ", tokens[0], tokens[1]);
                            Console.WriteLine(data);
                            string[] tokens = data.Split('_');

                            for (int i =0;i<tokens.Length;i++)
                                Console.WriteLine(tokens[i]);

                            dataToSend = Select.FunctionSelector(project, tokens);
                            dataToSend += ".";

                            if (tokens[0].Equals("COMPILE"))
                            {
                                //string command = tokens[0];//moved to select.cs

                                message1 = Encoding.ASCII.GetBytes(dataToSend);

                                // Send a message to Client  
                                // using Send() method 
                                clientSocket.Send(message1);
                            }
                            data = "";
                            message1 = null;
                            //f(data.IndexOf(".") > -1)
                            //        break;
                        }

                        byte[] message = Encoding.ASCII.GetBytes("ShutDown");

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
