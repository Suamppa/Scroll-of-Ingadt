using UnityEngine;
using UnityEngine.InputSystem;

// Implementations for player input
public class PlayerInputHandler : MonoBehaviour
{
    // Reference to the attack collider of the player
    public GameObject attackCollider;
    // Add Animator for animations
    public Animator animator;
    // Reference to the PlayerActions asset
    private PlayerActions input = null;
    // This is the vector that will be used to move the player
    private Vector2 moveVector = Vector2.zero;
    // Reference to the Rigidbody2D component, movement is physics-based
    private Rigidbody2D rb = null;
    // This is the speed of the player
    private float moveSpeed;

    private void Awake() {
        // Make sure rotation is 0
        transform.rotation = Quaternion.identity;
        input = new PlayerActions();
        rb = GetComponent<Rigidbody2D>();
        // Unity is able to find the PlayerStats component due to inheritance
        moveSpeed = GetComponent<Stats>().moveSpeed;
    }

    private void OnEnable() {
        input.Enable();
        // Subscribe to the Move action
        input.PlayerControls.Move.performed += OnMove;
        input.PlayerControls.Move.canceled += OnMoveCanceled;
        // Subscribe to the Attack action
        input.PlayerControls.Attack.performed += ctx => GetComponent<CanAttack>().Attack();
    }

    private void OnDisable() {
        input.Disable();
        // Unsubscribe from the Move action
        input.PlayerControls.Move.performed -= OnMove;
        input.PlayerControls.Move.canceled -= OnMoveCanceled;
        // Unsubscribe from the Attack action
        input.PlayerControls.Attack.performed -= ctx => GetComponent<CanAttack>().Attack();
    }

    // Use FixedUpdate to avoid spamming Time.deltaTime
    private void FixedUpdate() {
        rb.velocity = moveVector * moveSpeed;
        // Rotate according to the direction of movement
        if (moveVector != Vector2.zero) {
            float angle = Vector2.SignedAngle(Vector2.down, moveVector) - attackCollider.transform.rotation.eulerAngles.z;
            // Rotate the attack collider around the player
            attackCollider.transform.RotateAround(transform.position, Vector3.forward, angle);
        }
    }

    private void OnMove(InputAction.CallbackContext context) {
        // Save the movement vector from input
        animator.SetFloat("Speed", Mathf.Abs(moveSpeed));
        animator.SetBool("IsAttacking", false);
        animator.SetBool("isWounding", false);
        moveVector = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context) {
        // Reset the movement vector
        animator.SetFloat("Speed", 0);
        moveVector = Vector2.zero;
    }
}
