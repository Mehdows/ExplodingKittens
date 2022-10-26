namespace explodingKittens{    
    public class ExplodingKittens : CardPack{
        
        public ExplodingKittens() : base("ExplodingKittens"){

        }

        public override void populate(int players){
            var path = json.readListJson(this.type + ".json");
            var lst = json.jsonToList(path);
            foreach(Card card in lst){
                if(card.getType() != "ExplodingKitten" && card.getType() != "Defuse"){
                    this.cards.Add(card);
                    continue;
                }
                if(card.getType() == "ExplodingKitten"){
                    continue;
                }
                //card.getType() == "Defuse"
                this.cards.Add(card);
                if(players <= 4){
                    this.cards.Add(card);
                }

            }
        }

        public override void addKittens(int players){
            var path = json.readListJson(this.type + ".json");
            var lst = json.jsonToList(path);
            
            foreach(Card card in lst){
                if(card.getType() != "ExplodingKitten"){
                    continue;    
                }
                for(int i = 0; i < players; i++){
                    this.cards.Add(card);
                }
            }
        }



    }
}
