using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Tooltip("Max speed for movement")]
    [SerializeField] float maxSpeed = 5f;
    [Tooltip("Acceleration for the movement (move or stop)")]
    [SerializeField] float accel = 10f;

    // Cached references
    Rigidbody2D myRigidbody;

    Vector2 targetSpeed = Vector2.zero;

    void Start()
    {
        // Cache references
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MoveHorizontally();
    }

    void OnDisable()
    {
        myRigidbody.velocity = Vector2.zero;
        targetSpeed = Vector2.zero;
    }

    // Sets up velocity for FixedUpdate
    private void MoveHorizontally()
    {
        // Get inputs
        float horzDirection = Input.GetAxis("Horizontal");
        bool horzPressed = Input.GetButton("Horizontal");
        float vertDirection = Input.GetAxis("Vertical");
        bool vertPressed = Input.GetButton("Vertical");

        // Set target speed
        if (horzPressed || vertPressed)
        {
            horzDirection = (horzPressed) ? Mathf.Sign(horzDirection) : 0f;
            vertDirection = (vertPressed) ? Mathf.Sign(vertDirection) : 0f;
            targetSpeed = new Vector2(horzDirection, vertDirection);
            targetSpeed.Normalize();
            targetSpeed *= maxSpeed;
            //SetFacing(targetSpeed);
        }
        else { targetSpeed = Vector2.zero; }
    }

    public void SetAcceleration(float acceleration) { accel = acceleration; }
    public float GetAcceleration() { return accel; }

    public void SetMaxSpeed(float speed) { maxSpeed = speed; }
    public float GetMaxSpeed() { return maxSpeed; }

    public Vector2 GetTargetSpeed() { return targetSpeed; }
}
