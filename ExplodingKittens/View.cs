namespace explodingKittens {
    public class View{
        public static void notify(string message, int id){
            Server.sendMessage("Player " + id + ": " + message+"\n");
        }

        public static void notifyPlayer(string message, int id){
            Server.sendMessage("Player " + id + ": " + message+"\n", id);
        }

        public static int getIntInput(int id){
            int str = 0;
            for (int i = 0; i < 3; i++){
                try{
                    str = Convert.ToInt32(Server.getMessage(id));
                    break;
                }
                catch (Exception e){
                    Console.WriteLine("Invalid input, please try again");
                }
            }
            return str;
        }
        public static String getStringInput(int id){
            return Server.getMessage(id);
        }
    }
}