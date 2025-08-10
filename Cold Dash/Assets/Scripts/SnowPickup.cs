using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] int pointPerCoin = 100;
    bool wasCollected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.tag == "Player" && !wasCollected ) {
            wasCollected = true;  // Đánh dấu rằng đồng xu đã được nhặt

            FindObjectOfType<GameSession> ().AddScores (pointPerCoin);

            if ( coinPickupSFX != null ) {
                AudioSource.PlayClipAtPoint (coinPickupSFX, Camera.main.transform.position);
            }

            Destroy (gameObject);
        }
    }


    //[SerializeField] TextMeshProUGUI starText;
    //private int starCount = 0;

    //public void AddStar()
    //{
    //    starCount++;
    //    UpdateStarText ();
    //}

    //private void UpdateStarText()
    //{
    //    starText.text = starCount.ToString (); // Update UI with current star count
    //}

}
