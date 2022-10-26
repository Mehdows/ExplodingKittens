using System;
using System.Net.Sockets;
using System.Text; 
using System.Collections;

namespace explodingKittens
{
    public class client
    {
        System.Net.Sockets.TcpClient clientSocket;
        NetworkStream serverStream; 
        
        public client(string ip)
        {
            clientSocket = new System.Net.Sockets.TcpClient();
            clientSocket.Connect(ip, 8888);
            getMessage();
        }
        
        
        private void getMessage()
        {
            while(true){
                serverStream = clientSocket.GetStream();
                byte[] inStream = new byte[65536];
                serverStream.Read(inStream, 0, inStream.Length);
                string returndata = Encoding.ASCII.GetString(inStream);
                returndata = returndata.Replace("\0", "");
                var arr = returndata.Split('\n');
                foreach(var item in arr){
                    if (item == "get")
                    {
                        sendMessage(Console.ReadLine());
                    }
                    else{
                        Console.WriteLine(item);
                    }
                }
                
            }
        }
        public void sendMessage(string msg)
        {
            serverStream = clientSocket.GetStream();
            byte[] outStream = Encoding.ASCII.GetBytes(msg);
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
        }
    }
}