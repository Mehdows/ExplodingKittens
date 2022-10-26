using System.Collections;
namespace explodingKittens {

    public abstract class CardPack{
        public String type { get; set; }
        public ArrayList cards { get; set; }

        public CardPack(String type){
            this.type = type;
            this.cards = new ArrayList();
        }

        abstract public void populate(int players);

        abstract public void addKittens(int players);
        public void shuffle(){
            var rnd = new Random();
            for(int i = 0; i < this.cards.Count; i++){
                var index = rnd.Next(0, this.cards.Count);
                var temp = this.cards[i];
                this.cards[i] = this.cards[index];
                this.cards[index] = temp;
            }
        }

        public Card draw(){
            var card = this.cards[0];
            this.cards.RemoveAt(0);
            return (Card?)card!;
        }

        public ArrayList getCards(){
            return this.cards;
        }
        public void Add(Card c){
            this.cards.Add(c);
        }
        public void Remove(Card c){
            this.cards.Remove(c);
        }

        public int count(){
            return this.cards.Count;
        }
    }
}