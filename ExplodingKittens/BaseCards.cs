namespace explodingKittens{
    public class Card{
        public virtual String type{ get; set; }
        public virtual String description{ get; set; }
        public virtual bool playable{ get; set; }
        public virtual bool interruptable{ get; set; } 
        public virtual bool hasTarget{ get; set; }

        public Card(String type, String description, bool playable, bool hasTarget, bool discardable){
            this.type = type;
            this.description = description;
            this.playable = playable;
            this.hasTarget = hasTarget;
        }

        public String getType(){
            return this.type;
        }

        public String getDescription(){
            return this.description;
        }
        
        public bool isPlayable(){
            return this.playable;
        }

        public bool getHasTarget(){
            return this.hasTarget;
        }
        public bool getInterruptable(){
            return this.interruptable;
        }

        public virtual GameState onDraw(GameState obj){
            //do nothing
            return obj;
        }
        public virtual GameState onPlay(GameState obj){
            //do nothing
            return obj;
        }
        public virtual GameState onPlay(GameState obj, int target){
            //do nothing
            return obj;
        }
    }    

    public class Attack : Card{
        
        public Attack() : base("Attack", "Skip your turn. Your opponent takes an extra card.", true, false, false){
            
        }

        public override GameState onPlay(GameState obj){
            View.notify(this.type, obj.getCurrentPlayer().getId());
            obj.nextPlayer();
            if (obj.getTurnsLeft() == 1){
                obj.setTurnsLeft(2);
            }
            else{
                obj.setTurnsLeft(obj.getTurnsLeft()+2);
            }
            
            return obj;
        }
    }
    public class ExplodingKitten : Card{
        public ExplodingKitten() : base("ExplodingKitten", "You lose.", false, false, false){
            
        }
        

        public override GameState onDraw(GameState obj){
            View.notify(this.type, obj.getCurrentPlayer().getId());
            Card c = obj.getCurrentPlayer().getHand().getCard("Defuse");
            if(c != null){
                obj.getCurrentPlayer().getHand().Remove(c);
                obj.getCurrentPlayer().getHand().Remove(this);
                //Notify player and get ID
                while(true){
                    try{
                        View.notify(c.description+""+(obj.getDrawpile().count()-1), obj.getCurrentPlayer().getId()); //Notify of exploding kitten
                        var i = View.getIntInput(obj.getCurrentPlayer().getId());
                        if (obj.getDrawpile().count() != 0){
                            obj.getDrawpile().getCards()[i] = this;
                        }else{
                            obj.getDrawpile().Add(this);
                        }
                        
                        break;
                    }catch(Exception e){
                        View.notify("Invalid input, please try again", obj.getCurrentPlayer().getId());
                    }
                }
            }
            else{
                View.notify(description, obj.getCurrentPlayer().getId()); //Notify of exploding kitten
                obj.getCurrentPlayer().kill();
                obj.nextPlayer();
            }
            return obj;
        }
    }
    
    public class Defuse : Card{
        public Defuse() : base("Defuse", "You defused the kitten. Where in the deck do you wish to place the ExplodingKitten? 0..", false, false, false){
            
        }
    }

    public class Favor : Card{
        public Favor() : base("Favor", "Force another player to give you a card from their hand.", true, true, false){
            
        }

        public override GameState onPlay(GameState obj, int target){
            View.notify(this.type, obj.getCurrentPlayer().getId());
            obj.drawCardFromPlayer(target);
            return obj;
        }
    }

    public class Shuffle : Card{

        public Shuffle() : base("Shuffle", "Shuffle the deck.", true, false, false){
            
        }

        public override GameState onPlay(GameState obj){
            View.notify(this.type, obj.getCurrentPlayer().getId());
            obj.getDrawpile().shuffle();
            return obj;
        }
    }

    public class Skip : Card{
        public Skip() : base("Skip", "Skip your turn.", true, false, false){
            
        }

        public override GameState onPlay(GameState obj){
            View.notify(this.type, obj.getCurrentPlayer().getId());
            obj.nextTurn();
            return obj;
        }

    }

    public class SeeTheFuture : Card{
        public SeeTheFuture() : base("SeeTheFuture", "Look at the top 3 cards of the deck.", true, false, false){
            
        }

        public override GameState onPlay(GameState obj){
            //Notify of see the future
            View.notify(this.type, obj.getCurrentPlayer().getId());
            List<Card> cards = new List<Card>(3);
            for(int i = 0; i < 3; i++){
                cards.Add((Card?)obj.getDrawpile().getCards()[i]);
            }
            View.notifyPlayer("1: " + cards[0].getType() + " 2: " + cards[1].getType() + " 3: " + cards[2].getType(), obj.getCurrentPlayer().getId());

            return obj;
        }
    }

    public class Nope : Card{
        public Nope() : base("Nope", "Cancel the last action played.", false, false, true){
            
        }

        public override GameState onPlay(GameState obj){
            //Didn't implement :)
            return obj;
        }
    }

    public class HairyPotatoCat : Card{
        public HairyPotatoCat() : base("HairyPotatoCat", "", false, false, false){
            
        }
    }

    public class Cattermelon : Card{
        public Cattermelon() : base("Cattermelon", "", false, false, false){
            
        }
    }
    
    public class RainbowRalphingCat : Card{

        public RainbowRalphingCat() : base("RainbowRalphingCat", "", false, false, false){
            
        }
    }

    public class TacoCat : Card{

        public TacoCat() : base("TacoCat", "", false, false, false){
            
        }
    }

    public class OverweightBikiniCat : Card{

        public OverweightBikiniCat() : base("OverweightBikiniCat", "", false, false, false){
            
        }
    }
}
