using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public PlayerControls inputActions;

    [Header("Stats")]
    [SerializeField]
    private float speed = 50f;
    [SerializeField]
    private float jumpForce = 50f;
    [SerializeField]
    private float dashSpeed = 10f;

    private Animator animator;

    [Space]
    [Header("Booleans")]
    public bool onWall;
    public bool canMove;
    public bool canJump;
    public bool wallJump;
    public bool hasDashed;
    public bool isDashing;
    public bool isGrounded;
    public bool canInteract;
    public bool interacting;

    [Space]

    public PlayerType type;

    [HideInInspector]
    public Transform respawnPosition;

    [SerializeField]
    private GameObject InteractionIcon;

    private Vector3 startPosition;

    private bool onTrigger = false;
    private ActivateMovePlatform platform;
    private ActivateColorPlatform platformColor;

    private void Start()
    {
        inputActions = new PlayerControls();

        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        startPosition = transform.position;

        #region Input Enable
        if (type == PlayerType.Player1)
        {
            inputActions.Player1.Movement.Enable();
            inputActions.Player1.Jump.Enable();
            inputActions.Player1.Interact.Enable();
            inputActions.Player1.Pause.Enable();
        }
        if (type == PlayerType.Player2)
        {
            inputActions.Player2.Movement.Enable();
            inputActions.Player2.Jump.Enable();
            inputActions.Player2.Interact.Enable();
            inputActions.Player2.Dash.Enable();
        }
        #endregion
    }

    private void Update()
    {
        if (inputActions.Player1.Pause.triggered)
        {
            Menu script = GameObject.FindObjectOfType<Menu>();

            script.onMenu = !script.onMenu;

            Time.timeScale = script.onMenu ? 0f : 1f;
        }

        if (!canInteract)
        {
            interacting = false;
        }
        InteractionIcon.SetActive(canInteract);

        float x = (type == PlayerType.Player1) ? inputActions.Player1.Movement.ReadValue<float>() :
            inputActions.Player2.Movement.ReadValue<float>();

        Vector2 dir = new Vector2(x, 0);

        Walk(dir, type);

        #region Player 1 Zone
        if (type == PlayerType.Player1)
        {
            if (inputActions.Player1.Interact.triggered && onTrigger)
            {
                if (platform != null)
                {
                    platform.Active();
                }
                if (platformColor != null)
                {
                    platformColor.Active();
                }
            }

            if (inputActions.Player1.Interact.triggered && canInteract)
            {
                interacting = !interacting;
            }

            if (isGrounded || wallJump)
            {
                if (inputActions.Player1.Jump.triggered)
                {
                    wallJump = false;

                    interacting = false;

                    Jump(Vector2.up);
                }
            }
            else if (!isGrounded && !wallJump && canJump)
            {
                if (inputActions.Player1.Jump.triggered)
                {
                    Jump(Vector2.up);
                    canJump = false;
                }
            }
        }
        #endregion
        #region Player 2 Zone
        if (type == PlayerType.Player2)
        {
            if (inputActions.Player2.Interact.triggered && onTrigger)
            {
                if (platform != null)
                {
                    platform.Active();
                }
                if (platformColor != null)
                {
                    platformColor.Active();
                }
            }

            if (inputActions.Player2.Interact.triggered && canInteract)
            {
                interacting = !interacting;
            }

            if (isGrounded || wallJump)
            {
                if (inputActions.Player2.Jump.triggered)
                {
                    wallJump = false;

                    interacting = false;

                    Jump(Vector2.up);
                }
            }
            /*
            else if (!isGrounded && !wallJump && canJump)
            {
                if (inputActions.Player2.Jump.triggered)
                {
                    rb.AddForce(new Vector2(0f, jumpForce));
                    canJump = false;
                }
            }
            */
            if (inputActions.Player2.Dash.triggered)
            {
                Dash(dir.x, dir.y);
            }
        }
        #endregion
    }
 
    private void FixedUpdate()
    {
        if (interacting)
        {
            rb.gravityScale = 0f;

            wallJump = true;
            canJump = true;

            if (type == PlayerType.Player1)
            {
                //rb.velocity = new Vector2(0, horizontalInput * speed * Time.deltaTime);
            }

            if (type == PlayerType.Player2)
            {
                onWall = true;

                rb.velocity = Vector2.zero;
            }
        }
        else
        {
            rb.gravityScale = 3f;
        }
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
        animator.SetBool("onGround", isGrounded);

        if (inputActions.Player1.Jump.triggered)
        {
            animator.SetTrigger("Jump");
        }
        if (inputActions.Player2.Dash.triggered)
        {
            animator.SetTrigger("Dash");
        }
        if (inputActions.Player2.Jump.triggered)
        {
            animator.SetTrigger("Jump");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (type == PlayerType.Player1)
        {
            if (collision.tag == "Item")
            {
                collision.GetComponent<GetItemActivate>().Active();
            }
        }

        if (collision.tag == "Button")
        {
            collision.GetComponent<ButtonActivate>().Active();
        }

        if (collision.tag == "PlatformButton")
        {
            onTrigger = true;

            platform = collision.GetComponent<ActivateMovePlatform>();
        }

        if (collision.tag == "Checkpoint")
        {
            respawnPosition = collision.transform;
        }
        if (collision.tag == "PlatformColor")
        {
            onTrigger = true;

            platformColor = collision.GetComponent<ActivateColorPlatform>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Button")
        {
            collision.GetComponent<ButtonActivate>().Desactivate();
        }

        if (collision.tag == "PlatformButton")
        {
            onTrigger = false;

            platform = null;
        }
        if (collision.tag == "PlatformColor")
        {
            onTrigger = false;

            platformColor = null;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "PlatformButton")
        {
            Debug.Log("!");

            if (inputActions.Player1.Interact.triggered)
            {
                collision.GetComponent<ActivateMovePlatform>().Active();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "GroundPlatform")
        {
            isGrounded = true;
            canJump = true;

            //speed *= 4;

            gameObject.transform.SetParent(collision.transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "GroundPlatform")
        {
            isGrounded = false;

            //speed /= 4;

            gameObject.transform.SetParent(null);
        }
    }

    private void Walk(Vector2 dir, PlayerType playerType)
    {
        if (!canMove)
        {
            return;
        }
        if (onWall)
        {
            return;
        }
        if (type != playerType)
        {
            return;
        }

        rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * speed, rb.velocity.y)), 10 * Time.deltaTime);
    }

    private void Jump(Vector2 dir)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpForce;
    }

    private void Dash(float x, float y)
    {
        hasDashed = true;

        rb.velocity = Vector2.zero;

        Vector2 dir = new Vector2(x, y);

        dashSpeed = (dir.normalized.x != 0) ? 25 : 10;

        rb.velocity += dir.normalized * dashSpeed;
        StartCoroutine(DashWait());
    }

    IEnumerator DashWait()
    {
        rb.gravityScale = 0f;

        isDashing = true;

        yield return new WaitForSeconds(1f);

        rb.gravityScale = 3f;

        isDashing = false;
    }
}

public enum PlayerType
{
    Player1,
    Player2
}