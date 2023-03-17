using UnityEngine.InputSystem;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Player : MonoBehaviour
{
    [SerializeField]
    private PlayerControls inputActions;

    [SerializeField]
    private float speed = 50f;
    [SerializeField]
    private float jumpForce = 50f;

    private Rigidbody2D rb;
    private Animator animator;
    public bool isGrounded;

    public string PlayerNumber;

    public bool canJump;

    private void Start()
    {
        inputActions = new PlayerControls();

        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        #region Input Enable
        inputActions.Player1.Movement.Enable();
        inputActions.Player1.Jump.Enable();
        inputActions.Player1.Interact.Enable();

        inputActions.Player2.Movement.Enable();
        inputActions.Player2.Jump.Enable();
        inputActions.Player2.Interact.Enable();
        #endregion
    }

    private void Update()
    {
        if (PlayerNumber == "1")
        {
            if (isGrounded && inputActions.Player1.Jump.triggered)
            {
                rb.AddForce(new Vector2(0f, jumpForce));
            }

            if (!isGrounded && canJump && inputActions.Player1.Jump.triggered)
            {
                rb.AddForce(new Vector2(0f, jumpForce));
                canJump = false;
            }
        }
        if (PlayerNumber == "2")
        {
            if (isGrounded && inputActions.Player2.Jump.triggered)
            {
                rb.AddForce(new Vector2(0f, jumpForce));
            }
        }
    }
    private void FixedUpdate()
    {
        float horizontalInput = 0;

        if (PlayerNumber == "1")
        {
            horizontalInput = inputActions.Player1.Movement.ReadValue<float>();
        }
        if (PlayerNumber == "2")
        {
            horizontalInput = inputActions.Player2.Movement.ReadValue<float>();
        }

        rb.velocity = new Vector2(horizontalInput * speed * Time.deltaTime, rb.velocity.y);
    }
    private void LateUpdate()
    {
        float speedValue = rb.velocity.x;

        if (speedValue < 0f)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;

            speedValue *= -1;
        }
        else if(speedValue > 0f)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        animator.SetFloat("Speed", speedValue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            collision.GetComponent<GetItemActivate>().Active();
        }
        if (collision.tag == "Dialogue")
        {
            collision.GetComponent<DialogueActivate>().Active();
        }
    }
}
