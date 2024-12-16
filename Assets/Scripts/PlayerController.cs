
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [Header("Ground Check")]
    public Transform groundCheckPoint;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Boomerang")]
    public GameObject boomerangPrefab;
    public Transform boomerangSpawnPoint;

    [Header("Respawn")]
    public Transform respawnPoint;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded;
    public GameObject currentBoomerang;

    private Vector2 moveInput;
    private bool jumpPressed;
    private bool throwPressed;

    private PlayerInput playerInput;

    // Unity Events
    public UnityEvent onMove;
    public UnityEvent onJump;
    public UnityEvent onThrow;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();

        ConfigureInputScheme();
    }

    void Update()
    {
        // Ground Check
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);

        // Actualizar eventos de Unity
        if (onMove != null) onMove.Invoke();
        if (onJump != null) onJump.Invoke();
        if (onThrow != null) onThrow.Invoke();

        // Flip Character Based on Direction
        if (moveInput.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput.x), 1, 1);
        }

        // Animation Updates
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetBool("Grounded", isGrounded);

        // Handle Jumping
        if (jumpPressed && isGrounded)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        // Movement
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
    }

    private void ConfigureInputScheme()
    {
        // Detectar el esquema de control automáticamente
        if (playerInput.currentControlScheme == "Keyboard")
        {
            Debug.Log($"Player {playerInput.playerIndex + 1} usando teclado.");
        }
        else if (playerInput.currentControlScheme == "Gamepad")
        {
            Debug.Log($"Player {playerInput.playerIndex + 1} usando Gamepad {playerInput.devices[0].deviceId}.");
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            jumpPressed = true;
        }
        else if (context.canceled)
        {
            jumpPressed = false;
        }
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            throwPressed = true;
        }
        else if (context.canceled)
        {
            throwPressed = false;
        }
    }

    public void Respawn()
    {
        transform.position = respawnPoint.position;
        rb.velocity = Vector2.zero;
        isGrounded = false;
        anim.SetFloat("Speed", 0);
        anim.SetBool("Grounded", false);
    }

    // Lanzar boomerang
    public void ThrowBoomerang()
    {
        if (currentBoomerang == null)
        {
            currentBoomerang = Instantiate(boomerangPrefab, boomerangSpawnPoint.position, boomerangSpawnPoint.rotation);
            currentBoomerang.GetComponent<Boomerang>().ThrowingPlayer = boomerangSpawnPoint.gameObject;
        }
    }

    // Método para manejar el salto
    private void Jump()
    {
        // Aplicar una fuerza de salto
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        jumpPressed = false;  // Restablecer la variable de salto
    }
}
