using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] float loadDelay = 2f;  // 2-second delay before reloading
    [SerializeField] ParticleSystem crashEffect;
    [SerializeField] AudioClip crashSFX;
    bool notSecendTime = true;

    void OnCollisionEnter2D(Collision2D other)
    {
        if ( other.gameObject.tag == "Ground" || other.gameObject.tag == "Ground2" || other.gameObject.tag == "Enemy" || other.gameObject.tag == "Rock" ) {
            if ( notSecendTime ) {
                notSecendTime = false;
                FindObjectOfType<PlayerController> ().DisableControls ();  // Disable player controls
                crashEffect.Play ();  // Play crash effect
                FindObjectOfType<GameSession> ().ProcessPlayerDeath ();  
                if ( crashSFX != null ) {
                    GetComponent<AudioSource> ().PlayOneShot (crashSFX);  // Play crash sound
                }

                // Delay reloading the scene by 2 seconds
                Invoke ("ReloadScene", loadDelay);
            }
        }
    }

    void ReloadScene()
    {
        var currentSceneIndex = SceneManager.GetActiveScene ().buildIndex;
        SceneManager.LoadScene (currentSceneIndex);
    }
}
