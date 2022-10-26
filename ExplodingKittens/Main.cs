
namespace explodingKittens
{
    class MainClass{
        public static void Main(string[] args){
            //starts server
            //args[playercount, botCount]
            //Bots are unimplemented
            if (args.Length == 2){
                int playerCount = Convert.ToInt32(args[0]);
                int botCount = Convert.ToInt32(args[1]);
                    
                Thread thread; 
                thread = new Thread(() => runClient("localhost"));
                thread.Start();
                
                Server.start(playerCount);
                var game = new Game("ExplodingKittens", playerCount);
                game.start();


            }
            //if args only ipaddress start client
            else{
                runClient(args[0]);
            }
        }
        public static void runClient(string ip){
            client client = new client(ip);
        }
    }
}