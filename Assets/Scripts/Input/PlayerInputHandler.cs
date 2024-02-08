using UnityEngine;
using UnityEngine.InputSystem;

// Implementations for player input
public class PlayerInputHandler : MonoBehaviour
{
    // This is the speed of the player
    public float MoveSpeed { get => stats.MoveSpeed; }
    
    // Reference to the attack collider of the player
    public GameObject attackCollider;
    // Add Animator for animations
    public Animator animator;
    // Reference to the pause menu object
    public PauseMenuManager pauseMenu; // Yes, this is lazy

    // Reference to the PlayerActions asset
    private PlayerActions input = null;
    // This is the vector that will be used to move the player
    private Vector2 moveVector = Vector2.zero;
    // Reference to the Rigidbody2D component, movement is physics-based
    private Rigidbody2D rb = null;
    // Reference to the Stats component
    private Stats stats = null;

    private void Awake()
    {
        // Make sure rotation is 0
        transform.rotation = Quaternion.identity;
        input = new PlayerActions();
        rb = GetComponent<Rigidbody2D>();
        // Unity is able to find the PlayerStats component due to inheritance
        stats = GetComponent<Stats>();
    }

    private void OnEnable()
    {
        input.PlayerControls.Enable();
        // Subscribe to the Move action
        input.PlayerControls.Move.performed += OnMove;
        input.PlayerControls.Move.canceled += OnMoveCanceled;
        // Subscribe to the Attack action
        input.PlayerControls.Attack.performed += ctx => GetComponent<CanAttack>().Attack();
        // Subscribe to the WeaponPickup action
        input.PlayerControls.PickUpWeapon.performed += ctx => GetComponent<WeaponPickupHandler>().PlayerWeaponPick();
        // Subscribe to the Pause action
        input.PlayerControls.PauseGame.performed += OnPause;
    }

    private void OnDisable()
    {
        // Unsubscribe from the Move action
        input.PlayerControls.Move.performed -= OnMove;
        input.PlayerControls.Move.canceled -= OnMoveCanceled;
        // Unsubscribe from the Attack action
        input.PlayerControls.Attack.performed -= ctx => GetComponent<CanAttack>().Attack();
        // Unsubscribe from WeaponPickup action
        input.PlayerControls.PickUpWeapon.performed += ctx => GetComponent<WeaponPickupHandler>().PlayerWeaponPick();
        // Unsubscribe from the Pause action
        input.PlayerControls.PauseGame.performed -= OnPause;
        input.PlayerControls.Disable();
    }

    // Use FixedUpdate to avoid spamming Time.deltaTime
    private void FixedUpdate()
    {
        rb.velocity = moveVector * MoveSpeed;
        // Rotate according to the direction of movement
        if (moveVector != Vector2.zero)
        {
            float angle = Vector2.SignedAngle(Vector2.down, moveVector) - attackCollider.transform.rotation.eulerAngles.z;
            // Rotate the attack collider around the player
            attackCollider.transform.RotateAround(transform.position, Vector3.forward, angle);
        }
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 movement = context.ReadValue<Vector2>();
        float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
        animator.SetFloat("Speed", Mathf.Abs(movement.magnitude * MoveSpeed));

        if (angle >= -45 && angle < 45) {
            // Moving right
            animator.SetInteger("Direction", 1); // Set animator parameter for right animation
        } else if (angle >= 45 && angle < 135) {
            // Moving up
            animator.SetInteger("Direction", 2); // Set animator parameter for up animation
        } else if (angle >= 135 || angle < -135) {
            // Moving left
            animator.SetInteger("Direction", -1); // Set animator parameter for left animation
        } 
        else {
            // Moving down
            animator.SetInteger("Direction", 0); // Set animator parameter for down animation
        }
        moveVector = movement;
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        // Reset the movement vector
        animator.SetFloat("Speed", 0);
        moveVector = Vector2.zero;
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        pauseMenu.PauseGame(input);
    }
}
