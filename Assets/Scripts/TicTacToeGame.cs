using System;
using System.Collections.Generic;

public class TicTacToeGame
{
    public enum Player { None, X, O }
    private Player[,] board;
    private int movesCount;

    public TicTacToeGame()
    {
        board = new Player[3, 3];
        movesCount = 0;
        // Initialize the board with Player.None to represent empty cells
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                board[i, j] = Player.None;
            }
        }
    }

    public bool MakeMove(int row, int col, Player player)
    {
        if (board[row, col] == Player.None)
        {
            board[row, col] = player;
            movesCount++;
            return true;
        }
        return false; // Invalid move (cell already occupied)
    }

    public bool CheckWin(Player player)
    {
        // Check rows and columns for a win
        for (int i = 0; i < 3; i++)
        {
            if ((board[i, 0] == player && board[i, 1] == player && board[i, 2] == player) || // Check rows
                (board[0, i] == player && board[1, i] == player && board[2, i] == player))   // Check columns
            {
                return true;
            }
        }

        // Check diagonals for a win
        if ((board[0, 0] == player && board[1, 1] == player && board[2, 2] == player) || // Main diagonal
            (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player))   // Anti diagonal
        {
            return true;
        }

        return false;
    }

    public bool IsDraw()
    {
        // A draw occurs when all cells are filled, and no player has won
        return movesCount == 9 && !CheckWin(Player.X) && !CheckWin(Player.O);
    }

    public Player GetPlayerAtPosition(int row, int col)
    {
        return board[row, col];
    }

    // Get all available (empty) positions on the board
    public List<(int Row, int Col)> GetAvailableMoves()
    {
        var availableMoves = new List<(int, int)>();
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == Player.None)
                {
                    availableMoves.Add((i, j));
                }
            }
        }
        return availableMoves;
    }

    // Undo the last move by setting the cell back to empty
    public void UndoMove(int row, int col)
    {
        if (board[row, col] != Player.None)
        {
            board[row, col] = Player.None;
            movesCount--;
        }
    }
}
