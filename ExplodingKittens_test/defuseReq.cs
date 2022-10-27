namespace explodingKittens;

public class defuseReq
{
    const string DLC = "ExplodingKittens";
    [Fact]
    public void hasOneDefuse()
    {
        int playerCount = 2;
        var game = new Game(DLC, playerCount);
        var state = new GameState(DLC, playerCount);
        var players = state.getPlayers();
        for (int i = 0; i < players.Count; i++) {
            var player = players[i];
            var hand = player.getHand();
            var defuse = hand.getCard("Defuse");
            Assert.True(defuse != null);
        }
    }
}