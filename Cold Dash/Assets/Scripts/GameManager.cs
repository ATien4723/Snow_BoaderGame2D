using UnityEngine;

public enum GameState
{
    Playing,
    Paused,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState CurrentState { get; private set; } = GameState.Playing;


    private void Awake()
    {
        if ( Instance == null ) {
            Instance = this;
            DontDestroyOnLoad (gameObject);
        } else {
            Destroy (gameObject);
        }
    }

    public void SetGameState(GameState newState)
    {
        CurrentState = newState;

        switch ( newState ) {
            case GameState.Playing:
                Time.timeScale = 1f; // Ensure the game is running
                break;
            case GameState.Paused:
                Time.timeScale = 0f; // Pause the game
                break;
            case GameState.GameOver:
                Time.timeScale = 0f; // Pause the game
                break;
        }
    }
}
