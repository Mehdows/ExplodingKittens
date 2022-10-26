namespace explodingKittens;

public class UnitTest1
{
    const string DLC = "ExplodingKittens";
    [Fact]
    public void Test1()
    {
        int players = 1;
        try {
            var game = new Game(DLC, players);
            game.start();
        } catch (Exception e) {
            Assert.Equal("Invalid player amount", e.Message);
        }

        players = 6;
        try {
            var game = new Game(DLC, players);
            game.start();
        } catch (Exception e) {
            Assert.Equal("Invalid player amount", e.Message);
        }
    }
}