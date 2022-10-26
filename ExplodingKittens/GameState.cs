namespace explodingKittens{
    using System.Collections;
    public class GameState{
        private List<Player> players;
        private Player currentPlayer;
        private int turnsLeft;

        private CardPack drawpile;
        private ArrayList discardPile;

        private const int CARDSPERHAND = 7, MAXPLAYERS = 5, MINPLAYERS = 2;

        public GameState(string dlc, int playerAmount){
            this.players = new List<Player>(playerAmount);
            this.discardPile = new ArrayList();
            this.turnsLeft = 1;
            this.drawpile = CardPackFactory.createCardPack(dlc);
            this.drawpile.populate(playerAmount);
            this.drawpile.shuffle();
            addPlayers(playerAmount);
            dealCards(CARDSPERHAND);
            this.drawpile.addKittens(playerAmount);
            randomizeCurrentPlayer();
        }

        public Player getPlayer(int index){
            return this.players[index];
        }
        public List<Player> getPlayers(){
            return this.players;
        }
        public Player getCurrentPlayer(){
            return this.currentPlayer;
        }
        public void setCurrentPlayer(Player player){
            //this.players[this.players.IndexOf(this.currentPlayer)] = this.currentPlayer;
            this.currentPlayer = player;
        }
        public int getTurnsLeft(){
            return this.turnsLeft;
        }
        public void setTurnsLeft(int turnsLeft){
            this.turnsLeft = turnsLeft;
        }
        public CardPack getDrawpile(){
            return this.drawpile;
        }
        public void setDrawpile(CardPack drawpile){
            this.drawpile = drawpile;
        }
        public ArrayList getDiscardPile(){
            return this.discardPile;
        }
        public void addToDiscardPile(Card card){
            this.discardPile.Add(card);
        }


        public void nextTurn(){
            this.turnsLeft--;
            if(this.turnsLeft == 0){
                nextPlayer();
                this.turnsLeft = 1;
            }
        }

        public void nextPlayer(){
            int index = currentPlayer.getId();
            for(int i = 1; i < players.Count; i++){
                var j = (index + i) % players.Count;
                if(players[j].IsAlive()){
                    setCurrentPlayer(players[j]);
                    break;
                }
            }
        }
        public bool isWinner(){
            int alivePlayers = 0;
            foreach(Player player in players){
                if(player.IsAlive()){
                    alivePlayers++;
                }
            }
            return alivePlayers == 1;
        }
        public void saveCurrentPlayer(){
            this.players[currentPlayer.getId()] = currentPlayer;
        }

        public void drawCardFromPlayer(int targetId){
            while(true){
                var hand = this.getPlayer(targetId).getHand();
                View.notifyPlayer("Choose a card to give to Player"+ this.getCurrentPlayer().getId()+ "\n"+ hand.printCards(), targetId);
                var input = View.getStringInput(targetId);
                var c = hand.getCard(input);

                try{
                    this.getCurrentPlayer().getHand().Add(c);
                    hand.Remove(c);
                    this.getPlayer(targetId).setHand(hand);
                    break;
                }catch(Exception e){
                    View.notifyPlayer("Invalid card, please try again", targetId);
                }
            }
        }

        public void drawCardFromPlayer(int targetId, string cardType){
            var hand = this.getPlayer(targetId).getHand();
            var c = hand.getCard(cardType);
            this.getCurrentPlayer().getHand().Add(c);
            hand.Remove(c);
            this.getPlayer(targetId).setHand(hand);
        }
        

        private void dealCards(int cardPerHand){
            foreach(Player p in this.players){
                for(int i = 0; i < cardPerHand; i++){
                    p.getHand().Add(this.drawpile.draw());
                }
                p.getHand().Add(new Defuse());
            }

        }
        private void addPlayers(int playerAmount){
            if (playerAmount < MINPLAYERS || playerAmount > MAXPLAYERS){
                throw new Exception("Invalid player amount");
            }
            for (int i = 0; i < playerAmount; i++){
                this.players.Add(new Player());
            }
        }
        private void randomizeCurrentPlayer(){
            int playerAmount = players.Count;
            Random rnd = new Random();
            int id = Convert.ToInt16(rnd.NextInt64(0, playerAmount-1));
            this.setCurrentPlayer(this.getPlayers()[id]);
        }

    }
}