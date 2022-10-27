namespace explodingKittens
{
    public class deckReq
    {
        const string DLC = "ExplodingKittens";
        [Fact]
        public void defusesTwoPlayers()
        {
            var res = checkCardAmount(2, "Defuse");
            res -= 2;
            Assert.Equal(2, res);
        }
        [Fact]
        public void defusesFivePlayers(){
            var res = checkCardAmount(5, "Defuse");
            res -= 5;
            Assert.Equal(1, res);
        }

        [Fact]
        public void attackCards(){
            var res = checkCardAmount(2, "Attack");
            Assert.Equal(4, res);
        }
        
        [Fact]
        public void favorCards(){
            var res = checkCardAmount(2, "Favor");
            Assert.Equal(4, res);
        }
        [Fact]
        public void nopeCards(){
            var res = checkCardAmount(2, "Nope");
            Assert.Equal(5, res);
        }
        [Fact]
        public void shuffleCards(){
            var res = checkCardAmount(2, "Shuffle");
            Assert.Equal(4, res);
        }
        [Fact]
        public void skipCards(){
            var res = checkCardAmount(2, "Skip");
            Assert.Equal(4, res);
        }
        [Fact]
        public void seeTheFutureCards(){
            var res = checkCardAmount(2, "SeeTheFuture");
            Assert.Equal(5, res);
        }
        [Fact]
        public void hairyPotatoCatCards(){
            var res = checkCardAmount(2, "HairyPotatoCat");
            Assert.Equal(4, res);
        }
        [Fact]
        public void cattermelonCards(){
            var res = checkCardAmount(2, "Cattermelon");
            Assert.Equal(4, res);
        }
        [Fact]
        public void rainbowRalphingCatCards(){
            var res = checkCardAmount(2, "RainbowRalphingCat");
            Assert.Equal(4, res);
        }
        [Fact]
        public void tacoCatCards(){
            var res = checkCardAmount(2, "TacoCat");
            Assert.Equal(4, res);
        }
        [Fact]
        public void overweightBikiniCatCards(){
            var res = checkCardAmount(2, "OverweightBikiniCat");
            Assert.Equal(4, res);
        }
        public int checkCardAmount(int playerCount, string cardType){
            
            var game = new Game(DLC, playerCount);
            var state = new GameState(DLC, playerCount);
            var deck = state.getDrawpile().getCards();
            
            var count = 0;
            foreach(Card card in deck) {
                if (card.getType() == cardType) {
                    count++;
                }
            }
            var players = state.getPlayers();
            foreach (Player player in players) {
                var hand = player.getHand().getCards();
                foreach (Card card in hand) {
                    if (card.getType() == cardType) {
                        count++;
                    }
                }
            }
            return count;
        }
    }
}
