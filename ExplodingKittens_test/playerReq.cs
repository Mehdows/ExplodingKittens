namespace explodingKittens;

public class playerReq
{
    const string DLC = "ExplodingKittens";
    [Fact]
    public void LessThenTwo()
    {
        int players = 1;
        try {
            var game = new Game(DLC, players);
            game.start();
        } catch (Exception e) {
            Assert.Equal("Invalid player amount", e.Message);
        }
    }
    [Fact]
    public void moreThenFive()
    {
        int players = 6;
        try {
            var game = new Game(DLC, players);
            game.start();
        } catch (Exception e) {
            Assert.Equal("Invalid player amount", e.Message);
        }
    }

}