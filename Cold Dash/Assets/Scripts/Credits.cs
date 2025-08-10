using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;



public class Credits : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText; // Reference to the TextMesh Pro component
    void Start()
    {
        if ( GameSession.Instance != null ) {
            int finalScore = GameSession.Instance.GetScore ();
            scoreText.text = "Final Score: " + finalScore.ToString ();
        }
    }

    public void OnHomeButtonPressed()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene (0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
