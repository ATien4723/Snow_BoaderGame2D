using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float moveRange = 5f;
    [SerializeField] float leftLimit;
    [SerializeField] float rightLimit;

    private bool movingRight = true;
    private Animator animator;

    void Start()
    {
        leftLimit = transform.position.x - moveRange;
        rightLimit = transform.position.x + moveRange;
        animator = GetComponent<Animator> ();
    }

    void Update()
    {
        MoveLeftRight ();
    }

    void MoveLeftRight()
    {
        if ( movingRight ) {
            transform.Translate (Vector2.right * moveSpeed * Time.deltaTime);
            animator.Play ("EnemyB"); // Play right animation
            if ( transform.position.x >= rightLimit ) {
                movingRight = false;
            }
        } else {
            transform.Translate (Vector2.left * moveSpeed * Time.deltaTime);
            animator.Play ("EnemyA"); // Play left animation
            if ( transform.position.x <= leftLimit ) {
                movingRight = true;
            }
        }
    }
}
