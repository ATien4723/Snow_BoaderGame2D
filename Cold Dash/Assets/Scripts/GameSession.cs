using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // For TextMesh Pro

public class GameSession : MonoBehaviour
{
    public static GameSession Instance { get; private set; }

    [SerializeField] int playerLives = 3; // Default lives
    [SerializeField] int scores = 0; // Default scores

    private int currentLevel = 1; // Current level

    [SerializeField] TextMeshProUGUI levelText; // Text display for current level

    [SerializeField] TextMeshProUGUI livesText;  // Lives display
    [SerializeField] TextMeshProUGUI scoresText; // Scores display

    private void Awake()
    {
        if ( Instance == null ) {
            Instance = this;
            DontDestroyOnLoad (gameObject);
        } else {
            Destroy (gameObject);
        }
    }

    public void SetCurrentLevel(int level)
    {
        currentLevel = level;
        UpdateLevelText (); // Update the level display
    }

    public void UpdateLevelText()
    {
        if ( levelText != null ) {
            string levelName;

            // Xác định tên cấp độ dựa trên số cấp độ hiện tại
            switch ( currentLevel ) {
                case 1:
                    levelName = "Easy";
                    break;
                case 2:
                    levelName = "Medium";
                    break;
                case 3:
                    levelName = "Hard";
                    break;
                default:
                    levelName = "Unknown"; // Mặc định nếu không có cấp độ nào khớp
                    break;
            }

            // Cập nhật hiển thị cấp độ
            levelText.text = "Level:"  /*+currentLevel.ToString ()*/ + levelName;
        }
    }



    private void Start()
    {
        UpdateUI (); // Update UI when the game starts
        UpdateLevelText (); // Update level text on start
    }

    public void ProcessPlayerDeath()
    {
        if ( playerLives > 1 ) {
            playerLives--;
            UpdateUI ();
        } else {
            ShowGameOver ();
        }
    }

    private void ShowGameOver()
    {
        GameManager.Instance.SetGameState (GameState.GameOver);
        // Show game over panel
        GameObject.FindObjectOfType<GameUIManager> ().ShowGameOverPanel ();
    }

    public void AddScores(int score)
    {
        scores += score;
        UpdateUI ();
    }

    private void UpdateUI()
    {
        if ( livesText != null ) {
            livesText.text = "Lives: " + playerLives.ToString (); // Update lives display
        }

        if ( scoresText != null ) {
            scoresText.text = "Scores: " + scores.ToString (); // Update scores display
        }
    }

    public void ResetGameSession()
    {
        ResetScoreAndLives ();  // Reset điểm và mạng trước khi chuyển cảnh
        SceneManager.LoadScene (0);  // Load menu chính
        Time.timeScale = 1f;
        Destroy (gameObject); // Hủy đối tượng GameSession hiện tại
    }

    public void ResetScoreAndLives()
    {
        scores = 0;
        playerLives = 3;
        UpdateUI ();
    }

    public int GetScore()
    {
        return scores;
    }

    public void Retry()
    {
        ResetScoreAndLives ();  // Reset điểm và mạng khi nhấn nút retry
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);  // Tải lại màn chơi hiện tại
        Time.timeScale = 1f;
    }

}
