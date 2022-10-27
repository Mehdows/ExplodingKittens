namespace explodingKittens{
    public class Game{
        GameState state;
        string dlc;
        int playerAmount;

        public Game(string dlc, int playerAmount){
            this.dlc = dlc;
            this.playerAmount = playerAmount;
        }

        public void start(){
            this.state = new GameState(dlc, playerAmount);
            while(true){
                
                if (state.isWinner()){
                    View.notify("Win", state.getCurrentPlayer().getId());
                    break;
                }   

                View.notifyPlayer("It is your turn\n" + state.getCurrentPlayer().getHand().printCards() + "\n" + state.getCurrentPlayer().getHand().printPlayable(), state.getCurrentPlayer().getId());
                var rawAction = View.getStringInput(state.getCurrentPlayer().getId());
                var action = rawAction.Split(" ");
                action = toLowerList(action);    

                try{
                    if (action[0] == "pass"){
                        pass();
                        continue;
                    }
                    if (!isViableOption(action[0])){
                        throw new Exception(action + " is not an option");
                    }
                    state.addToDiscardPile(state.getCurrentPlayer().getHand().getCard(action[0]));
                    //Check if player has a NOPE card or similar card(abstract)
                    if (interruptCheck()){
                        View.notify("is about to play " + rawAction, state.getCurrentPlayer().getId());
                        playerInterrupt();
                        
                    }

                    switch(action[0]){
                        case "two":
                            playTwoCards(action);
                            break;
                        case "three":
                            playThreeCards(action);
                            break;
                        default:
                            playCard(action);
                            break;
                    }
                }catch(Exception e){
                    View.notifyPlayer(e+", please try again", state.getCurrentPlayer().getId());
                }      
            }
        }
        public GameState getState(){
            return this.state;
        }
        //Every item in list is converted to lowercase
        private string[] toLowerList(string[] list){
            for(int i = 0; i < list.Count(); i++){
                list[i] = list[i].ToLower();
            }
            return list;
        }

        //plays interruptable card
        private void playerInterrupt(){
            foreach (Player p in state.getPlayers()){
                foreach (Card c in p.getHand().getCards()){
                    if(c.getInterruptable()){
                        state = c.onPlay(state);
                    }
                    
                }
            }
        }
        //Checks if a player has a NOPE card or similar card(abstract)
        private bool interruptCheck(){
            foreach(Player p in state.getPlayers()){
                foreach(Card c in p.getHand().getCards()){
                    if (c.getInterruptable()){
                        return true;
                    }
                }
            }
            return false;
        }
        private void pass(){
            var card = state.getDrawpile().draw();
            state.getCurrentPlayer().getHand().Add(card);
            View.notifyPlayer("You drew a " + card.getType(), state.getCurrentPlayer().getId());
            state = card.onDraw(state);
            state.nextTurn();
        }

        private void playTwoCards(string[] action){
            state.drawCardFromPlayer(Convert.ToInt32(action[2]));
            for(int i = 0; i < 3; i++){
                var card = state.getCurrentPlayer().getHand().getCard(action[1]);
                state.getCurrentPlayer().getHand().Remove(card);
            }
        }
        private void playThreeCards(String[] action){
            state.drawCardFromPlayer(Convert.ToInt32(action[2]), action[3]);
            for(int i = 0; i < 3; i++){
                var card = state.getCurrentPlayer().getHand().getCard(action[1]);
                state.getCurrentPlayer().getHand().Remove(card);
            }            
        }
        private void playCard(string[] action){
            var card = state.getCurrentPlayer().getHand().getCard(action[0]);
            if (card.getHasTarget()){
                var target = Convert.ToInt32(action[1]);
                state = card.onPlay(state, target);                
            }
            else{
                state = card.onPlay(state);
            }
            state.getCurrentPlayer().getHand().Remove(card);  
        }



        //Checks if the inputed action is an option
        private bool isViableOption(string action){
            foreach(string option in state.getCurrentPlayer().getHand().getPlayable()){
                var items = action.Split(" ");//Incase of two card play
                if (items[0] == option.ToLower()){
                    return true;
                }
            }
            return false;
        }
    }    
}
