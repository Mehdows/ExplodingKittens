using System;
using System.Threading;
using System.Net.Sockets;
using System.Text;
using System.Collections;
namespace explodingKittens
{
    public static class Server
    {
        static ArrayList clients = new ArrayList();
        static private int count = 0;
        static TcpListener serverSocket;
        static TcpClient clientSocket;
        

        //player is the amount of clients that are allowed to connect
        public static void start(int player)
        {
            serverSocket = new TcpListener(8888);
            clientSocket = default(TcpClient);

            serverSocket.Start();
            Console.WriteLine(" >> " + "Server Started"); 

            
            while (player != count)
            {
                count++;
                clientSocket = serverSocket.AcceptTcpClient();
                Console.WriteLine(" >> " + "Client started!");
                clients.Add(clientSocket);
            }

        }
        
        
        public static void sendMessage(string msg){
            for(int i = 0; i < clients.Count; i++){
                sendMessage(msg, i);
            }
        }
   
        public static void sendMessage(string msg, int id){
            TcpClient clientSocket = (TcpClient)clients[id];
            NetworkStream networkStream = clientSocket.GetStream();
            byte[] sendBytes = Encoding.ASCII.GetBytes(msg);
            networkStream.Write(sendBytes, 0, sendBytes.Length);
            networkStream.Flush();
        }
        //getMessage() is called from View.cs
        //Gets a message from a specific client
        //sends a "get" message to request a message from the client
        public static string getMessage(int id){
            sendMessage("get", id);
            byte[] bytesFrom = new byte[65536];
            string dataFromClient = null;
            var clientSocket = (TcpClient)clients[id];
            NetworkStream networkStream = clientSocket.GetStream();
            networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
            dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
            dataFromClient = dataFromClient.Replace("\0", "");
            return dataFromClient;
        }

    }
}