using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject successPanel;
    [SerializeField] GameObject pausePanel;    // Thêm Pause Panel
    [SerializeField] TextMeshProUGUI scoreText; // Reference to score text in the Game Over panel
    [SerializeField] TextMeshProUGUI successScoreText; // Reference to score text in the Success panel


    [SerializeField] Button pauseButton;
    [SerializeField] Button retryButton;
    [SerializeField] Button continueButton;
    [SerializeField] Button menuButton;
    [SerializeField] Button homeButton;  // Nút Home
    [SerializeField] Button nextButton;  // Nút Next
    [SerializeField] Button homepauseButton;
    private Animator retryButtonAnimator;
    private Animator menuButtonAnimator;
    private Animator homeButtonAnimator;
    private Animator nextButtonAnimator;
    private Animator continueButtonAnimator;
    private Animator homepauseButtonAnimator;

    private bool isPaused = false;

    private void Start()
    {
        retryButton.onClick.AddListener (OnRetryButtonPressed);
        menuButton.onClick.AddListener (OnMenuButtonPressed);
        continueButton.onClick.AddListener (ContinueGame);
        homeButton.onClick.AddListener (OnHomeButtonPressed);
        nextButton.onClick.AddListener (OnNextButtonPressed);
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Lấy Animator của menuButton, retryButton, homeButton, và nextButton
        menuButtonAnimator = menuButton.GetComponent<Animator> ();
        retryButtonAnimator = retryButton.GetComponent<Animator> ();
        homeButtonAnimator = homeButton.GetComponent<Animator> ();
        nextButtonAnimator = nextButton.GetComponent<Animator> ();
        continueButtonAnimator = continueButton.GetComponent<Animator> ();
        homepauseButtonAnimator = homepauseButton.GetComponent<Animator> ();

        // Đặt updateMode là UnscaledTime để các nút hoạt động với Time.timeScale = 0f
        if ( menuButtonAnimator != null )
            menuButtonAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        if ( retryButtonAnimator != null )
            retryButtonAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        if ( homeButtonAnimator != null )
            homeButtonAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        if ( nextButtonAnimator != null )
            nextButtonAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        if ( continueButtonAnimator != null )
            continueButtonAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        if ( homepauseButtonAnimator != null )
            homepauseButtonAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void ShowGameOverPanel()
    {
        HideAllPanels ();
        gameOverPanel.SetActive (true);
        int currentScore = GameSession.Instance.GetScore (); // Assuming GetScore() fetches the current score
        scoreText.text = "Score: " + currentScore.ToString ();


        retryButton.gameObject.SetActive (true);
        retryButton.interactable = true;

        EventSystem.current.SetSelectedGameObject (retryButton.gameObject);

        GameManager.Instance.SetGameState (GameState.GameOver);
    }

    public void ShowSuccessPanel()
    {
        HideAllPanels ();
        successPanel.SetActive (true);
        int currentScore = GameSession.Instance.GetScore (); // Assuming GetScore() fetches the current score
        successScoreText.text = "Score: " + currentScore.ToString ();

        // Hiển thị và kích hoạt Animator cho nút Next và Home trong panel thành công
        if ( nextButtonAnimator != null )
            nextButtonAnimator.SetTrigger ("Show");
        if ( homeButtonAnimator != null )
            homeButtonAnimator.SetTrigger ("Show");

        GameManager.Instance.SetGameState (GameState.GameOver);
    }

    private void HideAllPanels()
    {
        gameOverPanel.SetActive (false);
        successPanel.SetActive (false);
    }

    private void OnRetryButtonPressed()
    {
        GameManager.Instance.SetGameState (GameState.Playing);
        GameSession.Instance.Retry ();
        gameOverPanel.SetActive (false);
    }

    public void Play()
    {
        SceneManager.LoadScene ("ChooseLevel");
    }


    public void TogglePausePanel()
    {
        isPaused = !isPaused;

        if ( isPaused ) {
            pausePanel.SetActive (true);
            Time.timeScale = 0f;  // Dừng game
        } else {
            ContinueGame ();
        }
    }

    public void ContinueGame()
    {
        isPaused = false;
        pausePanel.SetActive (false);
        Time.timeScale = 1f;  // Tiếp tục game
    }

    public void OnMenuButtonPressed()
    {
        GameManager.Instance.SetGameState (GameState.Playing);
        Time.timeScale = 1f;
        SceneManager.LoadScene (0);
    }

    public void OnHomeButtonPressed()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene (0);
    }

    public void OnNextButtonPressed()
    {
        Time.timeScale = 1f;

        int nextSceneIndex = SceneManager.GetActiveScene ().buildIndex + 1;

        if ( nextSceneIndex == 5 ) {
            GameSession.Instance.SetCurrentLevel (2); // Thiết lập cấp độ 2 cho scene 5
        } else if ( nextSceneIndex == 6 ) {
            GameSession.Instance.SetCurrentLevel (3); // Thiết lập cấp độ 3 cho scene 6
        } else {
            GameSession.Instance.SetCurrentLevel (1); // Mặc định là cấp độ 1 cho các scene khác
        }

        SceneManager.LoadScene (nextSceneIndex); // Chuyển sang màn tiếp theo
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        HideAllPanels ();
    }
}
