using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] float loadtime = 1f;
    [SerializeField] ParticleSystem finishEffect;

    void OnTriggerEnter2D(Collider2D other)
    {
        if ( other.tag == "Player" ) {
            finishEffect.Play ();
            GetComponent<AudioSource> ().Play ();

            // Call the method to show success panel and next button
            GameUIManager uiManager = GameObject.FindObjectOfType<GameUIManager> ();
            if ( uiManager != null ) {
                uiManager.ShowSuccessPanel ();  // Display success panel
            }

            Invoke ("LoadNextScene", loadtime);
        }
    }

    void LoadNextScene()
    {
        // Optional: You can load the next scene here or handle it via the button
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
    }
}
