using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( collision.gameObject.CompareTag ("Rock") ) {
            StartCoroutine (HandlePlayerDeath ());
            UnityEngine.Debug.Log ("Player hit a rock and will lose one life!");
        }

        if ( collision.gameObject.CompareTag ("Enemy") ) {
            StartCoroutine (HandlePlayerDeath ());
            UnityEngine.Debug.Log ("Player hit an enemy and will lose one life !");
        }
    }

    private IEnumerator HandlePlayerDeath() { 
        // Đợi 2 giây
        yield return new WaitForSeconds (0.5f);

        GameSession.Instance.ProcessPlayerDeath ();

        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
    }

}
