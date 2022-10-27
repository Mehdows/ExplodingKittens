namespace explodingKittens{
    using System.Collections;
    
    public class Player{
        private static int count = 0;
        private int id;
        private bool Alive = true;
        private Hand hand;

        public Player(){
            this.id = count;
            count++;
            this.hand = new Hand();
        }

        public Hand getHand(){
            return this.hand;
        }
        public void setHand(Hand hand){
            this.hand = hand;
        }
        public bool IsAlive(){
            return this.Alive;
        }
        public void kill(){
            this.Alive = false;
        }
        public int getId(){
            return this.id;
        }
        public void setId(int id){
            this.id = id;
        }

    }
}