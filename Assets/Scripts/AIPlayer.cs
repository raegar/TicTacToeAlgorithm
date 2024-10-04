using System;
using System.Collections.Generic;
using UnityEngine;
using static TicTacToeGame;

public class AIPlayer
{
    // Minimax algorithm without alpha-beta pruning.
    // 'game' is the current game state, 'depth' tracks how deep we are in the tree,
    // 'isMaximizing' indicates if we are maximizing the AI (O) or minimizing (X).
    public int Minimax(TicTacToeGame game, int depth, bool isMaximizing)
    {
        // Base case: check if any player has won or if there's a draw.
        // A win for Player X returns a negative score (-10 + depth), as X is the opponent.
        if (game.CheckWin(TicTacToeGame.Player.X))
            return -10 + depth;  // Player X win is bad for AI, so we return negative.

        // A win for Player O (the AI) returns a positive score (10 - depth).
        if (game.CheckWin(TicTacToeGame.Player.O))
            return 10 - depth;  // Player O win is good for AI, so we return positive.

        // A draw returns a score of 0 (neutral outcome).
        if (game.IsDraw())
            return 0;

        // If it's the AI's turn (maximizing player), we try to get the highest score.
        if (isMaximizing)
        {
            int maxEval = int.MinValue;  // Initialize the best score to the minimum possible.
            
            // Loop through all available moves on the board.
            foreach (var move in game.GetAvailableMoves())
            {
                // Simulate the move for Player O (AI).
                game.MakeMove(move.Row, move.Col, Player.O);
                
                // Recursively call Minimax for the opponent's turn (minimizing).
                int eval = Minimax(game, depth + 1, false);
                
                // Undo the move after evaluation to reset the game state.
                game.UndoMove(move.Row, move.Col);
                
                // Choose the highest evaluation score.
                maxEval = Mathf.Max(eval, maxEval);
            }
            return maxEval;  // Return the best score found.
        }
        else  // If it's the opponent's turn (minimizing player), we try to minimize the score.
        {
            int minEval = int.MaxValue;  // Initialize the best score to the maximum possible.
            
            // Loop through all available moves on the board.
            foreach (var move in game.GetAvailableMoves())
            {
                // Simulate the move for Player X (opponent).
                game.MakeMove(move.Row, move.Col, TicTacToeGame.Player.X);
                
                // Recursively call Minimax for the AI's turn (maximizing).
                int eval = Minimax(game, depth + 1, true);
                
                // Undo the move after evaluation to reset the game state.
                game.UndoMove(move.Row, move.Col);
                
                // Choose the lowest evaluation score.
                minEval = Mathf.Min(eval, minEval);
            }
            return minEval;  // Return the best score found.
        }
    }

    // Placeholder for Minimax with alpha-beta pruning.
    // This version will optimize the Minimax algorithm by cutting off branches that don't
    // need to be evaluated, improving performance.
    public int Minimax(TicTacToeGame game, int depth, bool isMaximizing, int alpha, int beta)
    {
        // Alpha-beta pruning version not yet implemented.
        // Alpha is the best value that the maximizer can guarantee,
        // and beta is the best value that the minimizer can guarantee.
        throw new NotImplementedException();  // Placeholder for the alpha-beta pruning implementation.
    }
}
