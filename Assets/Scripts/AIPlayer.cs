using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TicTacToeGame;
public class AIPlayer
{
    public int Minimax(TicTacToeGame game, int depth, bool isMaximizing, int alpha, int beta)
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
                int eval = Minimax(game, depth + 1, false, alpha, beta);
                game.UndoMove(move.Row, move.Col);
                maxEval = Mathf.Max(eval, maxEval);
                alpha = Mathf.Max(alpha, eval);
                if (beta <= alpha) break;
            }
            return maxEval;
        }
        else
        {
            int minEval = int.MaxValue;
            foreach (var move in game.GetAvailableMoves())
            {
                game.MakeMove(move.Row, move.Col, TicTacToeGame.Player.X);
                int eval = Minimax(game, depth + 1, true, alpha, beta);
                game.UndoMove(move.Row, move.Col);
                minEval = Mathf.Min(eval, minEval);
                beta = Mathf.Min(beta, eval);
                if (beta <= alpha) break;
            }
            return minEval;
        }
    }
}
