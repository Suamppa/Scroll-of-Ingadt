using UnityEngine;

public class AIChase : MonoBehaviour
{
    // This is the target that the AI will chase
    public GameObject target;
    // This is the distance how close the enemy will get to the target
    public float minDistanceBetween;
    // This is the distance how far the enemy will start following target
    public float maxDistanceBetween;
    // Add Animator for animations
    public Animator animator;

    // Walking speed of the AI
    private float Speed { get => stats.MoveSpeed; }

    // Distance between the AI and the target
    private float distance;
    private Stats stats = null;
    // Reference to the Rigidbody2D component, movement is physics-based
    private Rigidbody2D rb = null;
    // This is the vector that will be used to move the AI
    private Vector2 moveVector = Vector2.zero;
    private CanAttack canAttack = null;

    private void OnEnable()
    {
        stats = GetComponent<Stats>();
        // Make sure rotation is 0
        transform.rotation = Quaternion.identity;
        rb = GetComponent<Rigidbody2D>();
        canAttack = GetComponent<CanAttack>();
    }

    // Use FixedUpdate to avoid spamming Time.deltaTime
    void FixedUpdate()
    {
        // Reset the moveVector
        moveVector = Vector2.zero;
        animator.SetFloat("Speed", 0);
        // Only move is target is not null
        if (target != null)
        {
            distance = Vector2.Distance(transform.position, target.transform.position);
            // If distance is between minDistanceBetween and maxDistanceBetween, then move towards target
            if (distance < maxDistanceBetween)
            {
                animator.SetFloat("Speed", Mathf.Abs(Speed));
                // If able, attack ravenously while chasing
                if (canAttack != null) canAttack.Attack();
                if (distance > minDistanceBetween)
                {
                    moveVector = (target.transform.position - transform.position).normalized;
                    UpdateAnimatorDirection(moveVector);
                }
            }
        }
        // Use the moveVector to move the AI, no movement if moveVector is zero
        rb.velocity = moveVector * Speed;
    }
    private void UpdateAnimatorDirection(Vector2 moveVector)
    {
        // Rotate according to the direction of movement
        if (moveVector != Vector2.zero)
        {
            float angle = Vector2.SignedAngle(Vector2.down, moveVector);
            float moveDirection = angle;
            angle -= canAttack.AttackCollider.transform.rotation.eulerAngles.z;
            canAttack.AttackCollider.transform.RotateAround(transform.position, Vector3.forward, angle);

            // if (Debug.isDebugBuild)
            // {
            //     Debug.Log("moveDirection is: " + moveDirection);
            // }

             if (moveDirection >= 45 && moveDirection < 135) {
            // Moving right
                animator.SetInteger("Direction", 1); // Set animator parameter for right animation
            } else if (moveDirection >= 135 || moveDirection < -135) {
                // Moving up
                animator.SetInteger("Direction", 2); // Set animator parameter for up animation
            } else if (moveDirection >= -135 && moveDirection < -45) {
                // Moving left
                animator.SetInteger("Direction", -1); // Set animator parameter for left animation
            } else {
                // Moving down
                animator.SetInteger("Direction", 0); // Set animator parameter for down animation
            }
        }
    }
}
