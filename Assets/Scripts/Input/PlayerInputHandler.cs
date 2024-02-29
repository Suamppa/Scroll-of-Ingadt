using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

// Implementations for player input
public class PlayerInputHandler : MonoBehaviour
{
    // This is the speed of the player
    public float MoveSpeed { get => stats.MoveSpeed; }

    // Add Animator for animations
    public Animator animator;
    // Reference to the pause menu object
    public PauseMenuManager pauseMenu; // Yes, this is lazy

    // Reference to the attack collider of the player
    private GameObject attackCollider;
    // Reference to the PlayerActions asset
    private PlayerActions input = null;
    // This is the vector that will be used to move the player
    private Vector2 moveVector = Vector2.zero;
    // Reference to the Rigidbody2D component, movement is physics-based
    private Rigidbody2D rb = null;
    // Reference to the Stats component
    private Stats stats = null;
    // Direction of the player (0 = down, 1 = right, 2 = up, -1 = left)
    private int direction = 0;

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
        attackCollider = GetComponentInChildren<CapsuleCollider2D>().gameObject;

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

    private void Update()
    {
        // Set the direction for the animator
        animator.SetInteger("Direction", direction);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 movement = context.ReadValue<Vector2>();
        animator.SetFloat("Speed", Mathf.Abs(movement.magnitude * MoveSpeed));

        if (movement != Vector2.zero)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;

            direction = angle switch
            {
                >= -45 and < 45 => 1, // Moving right
                >= 45 and < 135 => 2, // Moving up
                >= 135 or < -135 => -1, // Moving left
                _ => 0, // Moving down
            };
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

    private void OnDrawGizmos()
    {
        // Draw a line to show the direction of the player
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3) moveVector);
    }
}
