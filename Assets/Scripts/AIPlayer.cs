using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TicTacToeGame;

public class AIPlayer
{
    // Minimax algorithm with alpha-beta pruning.
    // 'game' is the current game state, 'depth' tracks how deep we are in the tree, 
    // 'isMaximizing' indicates if we are maximizing the AI (O) or minimizing (X),
    // 'alpha' and 'beta' are the pruning limits for optimization.
    public int Minimax(TicTacToeGame game, int depth, bool isMaximizing, int alpha, int beta)
    {
        // Base case: check if any player has won or if there's a draw.
        // A win for Player X returns a negative score (-10 + depth), as X is the opponent.
        if (game.CheckWin(TicTacToeGame.Player.X))
            return -10 + depth;  // Favor Player O, hence X winning gives a negative score.
        
        // A win for Player O (the AI) returns a positive score (10 - depth).
        if (game.CheckWin(TicTacToeGame.Player.O))
            return 10 - depth;  // Favor AI winning, hence positive score.
        
        // A draw returns a score of 0 (neutral).
        if (game.IsDraw())
            return 0;

        // If it's the AI's turn (maximizing), we try to get the highest score.
        if (isMaximizing)
        {
            int maxEval = int.MinValue;  // Initialize the best score to the minimum possible.
            
            // Loop through all available moves.
            foreach (var move in game.GetAvailableMoves())
            {
                // Simulate the move for Player O (AI).
                game.MakeMove(move.Row, move.Col, Player.O);
                
                // Recursively call Minimax for the opponent (minimizing player's turn).
                int eval = Minimax(game, depth + 1, false, alpha, beta);
                
                // Undo the move after evaluating it.
                game.UndoMove(move.Row, move.Col);
                
                // Choose the maximum evaluation score.
                maxEval = Mathf.Max(eval, maxEval);
                
                // Apply alpha-beta pruning to cut off unnecessary branches.
                alpha = Mathf.Max(alpha, eval);
                if (beta <= alpha) break;  // Beta cut-off.
            }
            return maxEval;  // Return the maximum evaluation score found.
        }
        else  // If it's the opponent's turn (minimizing), we try to minimize the score.
        {
            int minEval = int.MaxValue;  // Initialize the best score to the maximum possible.
            
            // Loop through all available moves.
            foreach (var move in game.GetAvailableMoves())
            {
                // Simulate the move for Player X (opponent).
                game.MakeMove(move.Row, move.Col, TicTacToeGame.Player.X);
                
                // Recursively call Minimax for the AI's turn (maximizing player's turn).
                int eval = Minimax(game, depth + 1, true, alpha, beta);
                
                // Undo the move after evaluating it.
                game.UndoMove(move.Row, move.Col);
                
                // Choose the minimum evaluation score.
                minEval = Mathf.Min(eval, minEval);
                
                // Apply alpha-beta pruning to cut off unnecessary branches.
                beta = Mathf.Min(beta, eval);
                if (beta <= alpha) break;  // Alpha cut-off.
            }
            return minEval;  // Return the minimum evaluation score found.
        }
    }
}
