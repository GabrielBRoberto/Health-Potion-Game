using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
    [SerializeField]
    private PlayerControls inputActions;

    [SerializeField]
    private float speed = 50f;
    [SerializeField]
    private float jumpForce = 50f;
    [SerializeField]
    private float dashForce = 10f;

    private Rigidbody2D rb;
    //private Animator animator;
    public bool isGrounded;
    public bool onWall;

    private bool wallJump;

    public PlayerType type;

    public bool canJump;
    public bool canInteract;
    public bool interacting;

    private bool isDashing;

    [SerializeField]
    private GameObject InteractionIcon;

    private Vector3 startPosition;

    private void Start()
    {
        inputActions = new PlayerControls();

        rb = gameObject.GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();

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

        #region Player 1 Zone
        if (type == PlayerType.Player1)
        {
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

                    rb.AddForce(new Vector2(0f, jumpForce));
                }
            }
            else if (!isGrounded && !wallJump && canJump)
            {
                if (inputActions.Player1.Jump.triggered)
                {
                    rb.AddForce(new Vector2(0f, jumpForce));
                    canJump = false;
                }
            }
        }
        #endregion
        #region Player 2 Zone
        if (type == PlayerType.Player2)
        {
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

                    rb.AddForce(new Vector2(0f, jumpForce));
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
                float speedValue = rb.velocity.x;

                float dash = 0;

                if (speedValue < 0f)
                {
                    dash = -3f;
                }
                else
                {
                    dash = 3f;
                }
                transform.position = Vector3.Lerp(transform.position , new Vector3(transform.position.x + dash, transform.position.y, transform.position.z), 2f);
            }
        }
        #endregion
    }
    private void FixedUpdate()
    {
        float horizontalInput = 0;

        if (type == PlayerType.Player1)
        {
            horizontalInput = inputActions.Player1.Movement.ReadValue<float>();
        }
        if (type == PlayerType.Player2)
        {
            horizontalInput = inputActions.Player2.Movement.ReadValue<float>();
        }

        if (interacting)
        {
            rb.gravityScale = 0f;

            wallJump = true;
            canJump = true;

            if (type == PlayerType.Player1)
            {
                rb.velocity = new Vector2(0, horizontalInput * speed * Time.deltaTime);
            }

            if (type == PlayerType.Player2)
            {
                onWall = true;

                rb.velocity = Vector2.zero;
            }
        }
        else
        {
            rb.gravityScale = 1f;

            rb.velocity = new Vector2(horizontalInput * speed * Time.deltaTime, rb.velocity.y);
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

        //animator.SetFloat("Speed", speedValue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (type == PlayerType.Player1)
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

        if (collision.tag == "Button")
        {
            collision.GetComponent<ButtonActivate>().Active();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Button")
        {
            collision.GetComponent<ButtonActivate>().Desactivate();
        }
    }

    public void OnWaterHit()
    {
        transform.position = startPosition;
    }
}

public enum PlayerType
{
    Player1,
    Player2
}