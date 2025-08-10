using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Make sure to include this

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torqueAmount = 25f;
    [SerializeField] float boostSpeed = 35f;
    [SerializeField] float baseSpeed = 20f;
    [SerializeField] float maxAngularVelocity = 10f;
    [SerializeField] float jumpForce = 10f; // Lực nhảy



    private float initialSpeed;
    private bool isGrounded = false; // Kiểm tra trạng thái trên mặt đất
    Rigidbody2D rb2d;
    SurfaceEffector2D ground1speed, ground2speed;
    GameObject ground, ground2;
    bool canMove = true;

    [SerializeField] TMP_Text speedText; // Reference to the TextMesh Pro component

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D> ();

        ground = GameObject.FindGameObjectWithTag ("Ground");
        ground2 = GameObject.FindGameObjectWithTag ("Ground2");

        if ( ground != null ) {
            ground1speed = ground.GetComponent<SurfaceEffector2D> ();
        }

        if ( ground2 != null ) {
            ground2speed = ground2.GetComponent<SurfaceEffector2D> ();
        }

        initialSpeed = baseSpeed;

        rb2d.drag = 1f;
    }

    public void ResetToInitialSpeed()
    {
        if ( rb2d != null ) {
            if ( ground1speed != null ) ground1speed.speed = initialSpeed;
            if ( ground2speed != null ) ground2speed.speed = initialSpeed;

            rb2d.velocity = new Vector2 (initialSpeed, rb2d.velocity.y); // Reset velocity
            UpdateSpeedDisplay (); // Use UpdateSpeedDisplay instead of UpdateSpeedText
        } else {
            UnityEngine.Debug.LogWarning ("Rigidbody2D has been destroyed. Cannot reset speed.");
        }
    }

    void Update()
    {
        if ( canMove ) {
            RotatePlayer ();
            RespondToBoost ();
            StopPlayerOnDownArrow ();
            if ( Input.GetKeyDown (KeyCode.Space) && isGrounded ) // Nhảy khi nhấn Space và nhân vật đang trên mặt đất
            {
                Jump ();
            }
        }

        LimitAngularVelocity ();
        UpdateSpeedDisplay (); // Update speed display continuously
    }


    private void StopPlayerOnDownArrow()
    {
        if ( Input.GetKey (KeyCode.DownArrow) ) {
            rb2d.velocity = Vector2.zero; // Stop all movement
            rb2d.angularVelocity = 0f;    // Stop any rotation
        }
    }

    public void DisableControls()
    {
        canMove = false;
    }

    void RespondToBoost()
    {
        if ( Input.GetKey (KeyCode.UpArrow) ) {
            if ( ground1speed != null ) ground1speed.speed = boostSpeed;
            if ( ground2speed != null ) ground2speed.speed = boostSpeed;
        } else {
            if ( ground1speed != null ) ground1speed.speed = baseSpeed;
            if ( ground2speed != null ) ground2speed.speed = baseSpeed;
        }
    }

    void RotatePlayer()
    {
        if ( Input.GetKey (KeyCode.LeftArrow) ) {
            rb2d.AddTorque (torqueAmount); // Rotate left
        }
        if ( Input.GetKey (KeyCode.RightArrow) ) {
            rb2d.AddTorque (-torqueAmount); // Rotate right
        }
    }

    private void LimitAngularVelocity()
    {
        if ( rb2d.angularVelocity > maxAngularVelocity ) {
            rb2d.angularVelocity = maxAngularVelocity; // Clamp to max angular velocity
        } else if ( rb2d.angularVelocity < -maxAngularVelocity ) {
            rb2d.angularVelocity = -maxAngularVelocity; // Clamp to min angular velocity
        }
    }

    private void Jump()
    {
        rb2d.velocity = new Vector2 (rb2d.velocity.x, jumpForce); // Nhảy lên
        isGrounded = false; // Đặt lại trạng thái không ở trên mặt đất
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( collision.gameObject.CompareTag ("Ground") ) {
            isGrounded = true; // Đặt trạng thái đang ở trên mặt đất khi va chạm với mặt đất
        }
    }

    private void UpdateSpeedDisplay()
    {
        if ( rb2d != null && speedText != null ) {
            float currentSpeed = rb2d.velocity.x; // Assuming speed is on the x-axis
            speedText.text = $"Speed: {currentSpeed:F2}"; // Display the speed with two decimal places
        }
    }
}
