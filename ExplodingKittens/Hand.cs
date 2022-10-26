namespace explodingKittens{
    using System.Collections;
    public class Hand : CardPack{
        public Hand() : base("Hand"){
            
        }
        public override void populate(int players){
            //do nothing
        }
        public int countOf(Card c){
            var count = 0;
            foreach(Card card in this.cards){
                if(card.getType() == c.getType()){
                    count++;
                }
            }
            return count;
        }
        public override void addKittens(int players){
            //do nothing
        }
        public Card getCard(string type){
            foreach(Card card in this.cards){
                if(card.getType().ToLower() == type.ToLower()){
                    return card;
                }
            }
            return null;
        }

        public ArrayList getPlayable(){
            var playable = new ArrayList();
            foreach(Card card in this.cards){
                if(card.isPlayable() && !playable.Contains(card.getType())){
                    playable.Add(card.getType());
                }
                if(this.countOf(card) > 1 && !playable.Contains("Two "+card.getType())){
                    playable.Add("Two "+card.getType());
                }
                if(this.countOf(card) > 2 && !playable.Contains("Three "+card.getType())){
                    playable.Add("Three "+card.getType());
                }
            }
            return playable;
        }
        public string printPlayable(){
            var playable = this.getPlayable();
            var output = "";
            foreach(string card in playable){
                output += card + "\n";
            }
            return output;
        }

        public string printCards(){
            var output = "[";
            foreach(Card card in this.cards){
                output += card.getType() + ", ";
            }
            return output + "]";
        }



    }
}