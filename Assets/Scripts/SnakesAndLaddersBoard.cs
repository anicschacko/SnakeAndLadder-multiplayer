using UnityEngine;
using System.Collections.Generic;

public class SnakesAndLaddersBoard : MonoBehaviour
{
    public GameObject tilePrefab; // The prefab representing a single tile on the board
    public int boardSizeX = 10; // Number of tiles along the X-axis
    public int boardSizeY = 10; // Number of tiles along the Y-axis

    // Define the positions of snakes and ladders
    // A snake/ladder goes from "startPosition" to "endPosition"
    public List<Vector2Int> snakeStartPositions;
    public List<Vector2Int> snakeEndPositions;
    public List<Vector2Int> ladderStartPositions;
    public List<Vector2Int> ladderEndPositions;

    private void Start()
    {
        CreateBoard();
        AddSnakesAndLadders();
    }

    private void CreateBoard()
    {
        for (int y = 0; y < boardSizeX; y++)
        {
            // Check if the row is even or odd to create a zigzag pattern
            int xStart = (y % 2 == 0) ? 0 : boardSizeY - 1;
            int xEnd = (y % 2 == 0) ? boardSizeY : -1;

            // Determine the step direction based on the row being even or odd
            int step = (y % 2 == 0) ? 1 : -1;

            for (int x = xStart; x != xEnd; x += step)
            {
                // Calculate the position of the tile based on grid coordinates
                Vector3 tilePosition = new Vector3(x, y, 0f);
                // Instantiate the tile prefab at the calculated position
                GameObject tile = Instantiate(tilePrefab, tilePosition, Quaternion.identity);
                tile.transform.SetParent(transform); // Set the board as the parent of the tile
            }
        }
    }

    private void AddSnakesAndLadders()
    {
        // Make sure that the number of start and end positions for snakes and ladders match
        if (snakeStartPositions.Count != snakeEndPositions.Count ||
            ladderStartPositions.Count != ladderEndPositions.Count)
        {
            Debug.LogError("Number of snake/ladder start and end positions do not match!");
            return;
        }

        // Add snakes
        for (int i = 0; i < snakeStartPositions.Count; i++)
        {
            Vector2Int startPos = snakeStartPositions[i];
            Vector2Int endPos = snakeEndPositions[i];
            GameObject snake = CreateSnakeOrLadder(startPos, endPos, Color.red);
            snake.transform.SetParent(transform); // Set the board as the parent of the snake
        }

        // Add ladders
        for (int i = 0; i < ladderStartPositions.Count; i++)
        {
            Vector2Int startPos = ladderStartPositions[i];
            Vector2Int endPos = ladderEndPositions[i];
            GameObject ladder = CreateSnakeOrLadder(startPos, endPos, Color.green);
            ladder.transform.SetParent(transform); // Set the board as the parent of the ladder
        }
    }

    private GameObject CreateSnakeOrLadder(Vector2Int startPos, Vector2Int endPos, Color color)
    {
        // Calculate the world positions of the start and end positions
        Vector3 startWorldPos = new Vector3(startPos.x, startPos.y, 0f);
        Vector3 endWorldPos = new Vector3(endPos.x, endPos.y, 0f);

        // Create a line renderer (snake/ladder) between the start and end positions
        GameObject snakeOrLadder = new GameObject("SnakeOrLadder");
        LineRenderer lineRenderer = snakeOrLadder.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPositions(new Vector3[] { startWorldPos, endWorldPos });

        return snakeOrLadder;
    }
}
