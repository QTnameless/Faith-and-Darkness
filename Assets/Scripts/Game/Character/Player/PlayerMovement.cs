using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public IdleState idleState;
    public WalkState walkState;
    public AttackState attackState;
    public State currentState;

    public AudioSource audioSource;
    public Animator animator;
    public Rigidbody2D rigidBody;
   
    public InputAction playerControls;


    public Vector2 movementInput { get; private set; }
    public Vector2 aimInput { get; private set; }
    public float speed;
    private bool canMoveAndRotate = true;
    public bool canAttack = true;

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    void Start()
    {
        // Setup initial states with references
        idleState.Setup(rigidBody, animator, audioSource , this);
        walkState.Setup(rigidBody, animator,  audioSource , this);
        attackState.Setup(rigidBody, animator, audioSource, this);
        // Start with IdleState
        currentState = idleState;
        currentState.Initialise();
        currentState.Enter();
    }

    void Update()
    {
        if (canMoveAndRotate)
        {
            CheckInputVector();
            //CheckMouseVector();
            HandleMovementInput();
        }

        currentState.Do();

        // adding if CurrentState.iscomplete cause error here
        SelectState();
        



    }

    void FixedUpdate()
    {
        // Call FixedDo for physics-related state logic
        currentState.FixedDo();
    }

    public void SetState(State newState)
    {
        if (currentState != newState)
        {
            currentState.Exit();
            currentState = newState;
            currentState.Initialise();
            currentState.Enter();
        }
    }


    private void SelectState()
    {

        if (Mouse.current.leftButton.wasPressedThisFrame && canAttack)
        {
            SetState(attackState); // Immediately enter the AttackState
            return; // Exit the function to avoid other transitions
        }

        // Transition back to Idle or Walk if the current state is complete
        if (currentState.isComplete)
        {
            State targetState = movementInput == Vector2.zero ? idleState : walkState;
            SetState(targetState);
        }
    }

    public void EnableMovementAndRotation(bool enable)
    {
        canMoveAndRotate = enable;
        if (!enable)
        {
            rigidBody.velocity = Vector2.zero;
        }
    }
    public void EnableAttack(bool enable)
    {
        canAttack = enable; 
    }

    private void CheckInputVector()
    {
        movementInput = playerControls.ReadValue<Vector2>().normalized;

    }

    private void HandleMovementInput()
    {

        //sets the velocity of the _rigidbody to a new Vector2
        Vector2 targetVelocity = movementInput * speed;

        // Interpolate between the current velocity and the target velocity
        rigidBody.velocity = Vector2.Lerp(rigidBody.velocity, targetVelocity, 0.1f); // Adjust smooth time as needed

    }

  
    /*
    private void CheckMouseVector()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the direction from the player to the mouse
        aimInput = (mousePosition - (Vector2)transform.position).normalized;
    }
    
    public float GetAngle(Vector2 from, Vector2 to)
    {
        return Vector2.Angle(from, to);
    }
    */


}
