using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //private Animator animator;
    private Rigidbody rigidbody;

    public float rotationSpeed = 13f;
    public float walkSpeed = 1f;
    public float runSpeed = 4f;
    public float speedSmoothTimeStanding = 0.05f;
    public float speedSmoothTimeMoving = 0.2f;

    private float currentSpeed;
    private float speedVelocity;

    void Start()
    {
        //animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float move = Input.GetAxis("Vertical");  // (W/S) 
        float turn = Input.GetAxis("Horizontal"); // (A/D)

        bool isRunning = Input.GetKey(KeyCode.LeftShift);


        move = Mathf.Clamp(move, 0, 1);
        float targetSpeed = /*Mathf.Abs(move)*/ move * (isRunning ? runSpeed : walkSpeed);


        // smooth speed
        float smoothTime = currentSpeed < 0.1f ? speedSmoothTimeStanding : speedSmoothTimeMoving;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedVelocity, smoothTime);

        // move
        var movement = transform.forward * move * currentSpeed;
        rigidbody.linearVelocity = new(movement.x, rigidbody.linearVelocity.y, movement.z);

        // rotate
        if (Mathf.Abs(turn) > 0.1f)
        {
            var targetRotation = Quaternion.Euler(0, turn * rotationSpeed * 100 * Time.deltaTime, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation,
                transform.rotation * targetRotation,
                Time.deltaTime * rotationSpeed);
        }

        // animate
        //animator.SetFloat("speed", currentSpeed);
    }
}

