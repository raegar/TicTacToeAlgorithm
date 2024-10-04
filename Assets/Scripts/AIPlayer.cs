using System;
using System.Collections.Generic;
using UnityEngine;
using static TicTacToeGame;

public class AIPlayer
{
    public int Minimax(TicTacToeGame game, int depth, bool isMaximizing)
    {
        if (game.CheckWin(TicTacToeGame.Player.X))
            return -10 + depth;
        if (game.CheckWin(TicTacToeGame.Player.O))
            return 10 - depth;
        if (game.IsDraw())
            return 0;

        if (isMaximizing)
        {
            int maxEval = int.MinValue;
            foreach (var move in game.GetAvailableMoves())
            {
                game.MakeMove(move.Row, move.Col, Player.O);
                int eval = Minimax(game, depth + 1, false);
                game.UndoMove(move.Row, move.Col);
                maxEval = Mathf.Max(eval, maxEval);
            }
            return maxEval;
        }
        else
        {
            int minEval = int.MaxValue;
            foreach (var move in game.GetAvailableMoves())
            {
                game.MakeMove(move.Row, move.Col, TicTacToeGame.Player.X);
                int eval = Minimax(game, depth + 1, true);
                game.UndoMove(move.Row, move.Col);
                minEval = Mathf.Min(eval, minEval);
            }
            return minEval;
        }
    }

    public int Minimax(TicTacToeGame game, int depth, bool isMaximizing, int alpha, int beta) //With alpha-beta pruning
    {
        throw new NotImplementedException();
    }
}
