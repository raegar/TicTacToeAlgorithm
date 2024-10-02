using NUnit.Framework;
using static TicTacToeGame;

[TestFixture]
public class TicTacToeTests
{
    [Test]
    public void TestPlayerWinsWithHorizontalLine()
    {
        TicTacToeGame game = new TicTacToeGame();
        game.MakeMove(0, 0, Player.X);
        game.MakeMove(0, 1, Player.X);
        game.MakeMove(0, 2, Player.X);

        Assert.IsTrue(game.CheckWin(Player.X));
    }

    [Test]
    public void TestDrawCondition()
    {
        TicTacToeGame game = new TicTacToeGame();
         
        // Fill the board to create a draw situation
        // X | O | X
        // O | X | O
        // O | X | O
        game.MakeMove(0, 0, Player.X);
        game.MakeMove(0, 1, Player.O);
        game.MakeMove(0, 2, Player.X);

        game.MakeMove(1, 0, Player.O);
        game.MakeMove(1, 1, Player.X);
        game.MakeMove(1, 2, Player.O);

        game.MakeMove(2, 0, Player.O);
        game.MakeMove(2, 1, Player.X);
        game.MakeMove(2, 2, Player.O);

        // Assert that the game results in a draw
        Assert.IsTrue(game.IsDraw());
    }

}
