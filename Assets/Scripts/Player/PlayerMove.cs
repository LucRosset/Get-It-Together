using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Tooltip("Max speed for movement")]
    [SerializeField] float maxSpeed = 5f;
    [Tooltip("Acceleration for the movement (move or stop)")]
    [SerializeField] public float accel = 10f;

    // Cached references
    Rigidbody2D myRigidbody;
    Animator fireAnimator;

    public float targetSpeed { get; private set; } = 0f;

    void Start()
    {
        // Cache references
        myRigidbody = GetComponent<Rigidbody2D>();
        fireAnimator = GameObject.Find("/Player/Body/Fire").GetComponent<Animator>();
    }

    void Update()
    {
        // Get inputs
        bool horzPressed = Input.GetButton("Horizontal");
        bool vertPressed = Input.GetButton("Vertical");

        // Set target speed
        targetSpeed = (horzPressed || vertPressed) ? maxSpeed : 0f;
    }

    void OnDisable()
    {
        myRigidbody.velocity = Vector2.zero;
        targetSpeed = 0f;
    }

    public void SetAcceleration(float acceleration) { accel = acceleration; }
    public float GetAcceleration() { return accel; }

    public void SetMaxSpeed(float speed) { maxSpeed = speed; }
    public float GetMaxSpeed() { return maxSpeed; }
}
