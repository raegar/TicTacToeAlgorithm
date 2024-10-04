using UnityEngine;
using TMPro;

public class TicTacToeManager : MonoBehaviour
{
    public GameObject buttonPrefab;  // Prefab for buttons (this can be a basic button with TextMeshPro component)
    public Canvas canvas;
    private TicTacToeGame game;
    private GameObject[,] buttons;
    private AIPlayer aiPlayer;
    private bool isPlayerTurn = true;

    void Start()
    {
        game = new TicTacToeGame();
        aiPlayer = new AIPlayer();

        if (canvas == null)
        {
            canvas = FindObjectOfType<Canvas>();
        }

        CreateBoard();
    }

    void CreateBoard()
    {
        buttons = new GameObject[3, 3];

        // Get the RectTransform of the Canvas to calculate its size
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();

        // Dynamically set button size as a percentage of the screen width
        float buttonSize = canvasRect.rect.width * 0.2f;  // 20% of the screen width
        float spacing = buttonSize * 0.05f;               // Spacing as 5% of button size

        // Calculate the total size of the board
        float totalWidth = (3 * buttonSize) + (2 * spacing);
        float totalHeight = (3 * buttonSize) + (2 * spacing);

        // Calculate the starting position to center the grid on the canvas
        float startX = -(totalWidth / 2) + 120;
        float startY = -(totalHeight / 2);

        // Create the 3x3 grid of buttons
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                GameObject button = Instantiate(buttonPrefab, canvas.transform);
                buttons[row, col] = button;

                RectTransform rectTransform = button.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(buttonSize, buttonSize);
                rectTransform.pivot = new Vector2(0.5f, 0.5f);

                float posX = startX + col * (buttonSize + spacing);
                float posY = startY + row * (buttonSize + spacing);
                rectTransform.anchoredPosition = new Vector2(posX, posY);

                int r = row, c = col;
                button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnButtonClick(r, c));
                button.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }



    void OnButtonClick(int row, int col)
    {
        if (game.GetPlayerAtPosition(row, col) == TicTacToeGame.Player.None && isPlayerTurn)
        {
            // Player's move
            game.MakeMove(row, col, TicTacToeGame.Player.X);
            UpdateButtonUI(row, col, "X");

            if (game.CheckWin(TicTacToeGame.Player.X))
            {
                Debug.Log("Player X wins!");
                EndGame();
                return;
            }

            if (game.IsDraw())
            {
                Debug.Log("It's a draw!");
                EndGame();
                return;
            }

            // Switch to AI's turn
            isPlayerTurn = false;
            AITurn();
        }
    }

    void AITurn()
    {
        // AI calculates its best move using Minimax with alpha-beta pruning
        var bestMove = GetBestMoveForAI();
        game.MakeMove(bestMove.Row, bestMove.Col, TicTacToeGame.Player.O);
        UpdateButtonUI(bestMove.Row, bestMove.Col, "O");

        if (game.CheckWin(TicTacToeGame.Player.O))
        {
            Debug.Log("Player O (AI) wins!");
            EndGame();
            return;
        }

        if (game.IsDraw())
        {
            Debug.Log("It's a draw!");
            EndGame();
            return;
        }

        // Switch back to player's turn
        isPlayerTurn = true;
    }

    (int Row, int Col) GetBestMoveForAI()
    {
        var availableMoves = game.GetAvailableMoves();
        int bestScore = int.MinValue;
        (int Row, int Col) bestMove = availableMoves[0];

        foreach (var move in availableMoves)
        {
            game.MakeMove(move.Row, move.Col, TicTacToeGame.Player.O);
            int moveScore = aiPlayer.Minimax(game, 0, false, int.MinValue, int.MaxValue);
            game.UndoMove(move.Row, move.Col);

            if (moveScore > bestScore)
            {
                bestScore = moveScore;
                bestMove = move;
            }
        }
        return bestMove;
    }

    void UpdateButtonUI(int row, int col, string playerSymbol)
    {
        buttons[row, col].GetComponentInChildren<TextMeshProUGUI>().text = playerSymbol;
    }

    void EndGame()
    {
        // Disable all buttons to stop the game
        foreach (var button in buttons)
        {
            button.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
    }
}
