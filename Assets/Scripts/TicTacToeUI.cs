using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static TicTacToeGame;

public class TicTacToeUI : MonoBehaviour
{
    private GameObject[,] buttons;
    private TicTacToeGame game;

    void Start()
    {
        game = new TicTacToeGame();
        CreateUI();
    }

    void CreateUI()
    {
        // Create a Canvas
        GameObject canvasGO = new GameObject("Canvas");
        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasGO.AddComponent<CanvasScaler>();
        canvasGO.AddComponent<GraphicRaycaster>();

        buttons = new GameObject[3, 3];

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                // Create a Button
                GameObject buttonGO = new GameObject($"Button_{i}_{j}");
                buttonGO.transform.SetParent(canvasGO.transform);

                Button button = buttonGO.AddComponent<Button>();
                RectTransform rectTransform = buttonGO.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(100, 100);
                rectTransform.anchoredPosition = new Vector2(i * 110, j * 110);

                // Add TextMeshPro to Button
                GameObject textGO = new GameObject("Text");
                textGO.transform.SetParent(buttonGO.transform);

                TextMeshProUGUI tmp = textGO.AddComponent<TextMeshProUGUI>();
                tmp.text = "";
                tmp.fontSize = 36;
                tmp.alignment = TextAlignmentOptions.Center;
                RectTransform textRect = textGO.GetComponent<RectTransform>();
                textRect.sizeDelta = new Vector2(100, 100);
                textRect.anchoredPosition = Vector2.zero;

                // Save reference to button
                buttons[i, j] = buttonGO;

                // Add onClick listener
                int row = i, col = j;
                button.onClick.AddListener(() => OnButtonClick(row, col));
            }
        }
    }

    void OnButtonClick(int row, int col)
    {
        // Handle button click logic, updating game state and UI
        game.MakeMove(row, col, Player.X);
        UpdateUI(row, col, "X");

        // After player move, AI responds
        // Call AI move with Minimax
    }

    void UpdateUI(int row, int col, string playerSymbol)
    {
        TextMeshProUGUI tmp = buttons[row, col].GetComponentInChildren<TextMeshProUGUI>();
        tmp.text = playerSymbol;
    }
}
