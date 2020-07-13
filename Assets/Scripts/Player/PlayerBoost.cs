using System.Collections;
using UnityEngine;

public class PlayerBoost : MonoBehaviour
{
    [Tooltip("Speed during the boost")]
    [SerializeField] float boostSpeed = 5f;
    [Tooltip("Boosting time")]
    [SerializeField] float boostTime = .1f;
    [Tooltip("Breaking acceleration (after boosting)")]
    [SerializeField] float breakAccel = .1f;
    [Tooltip("Cooldown between a boost and the next warmup")]
    [SerializeField] float cooldownTime = 1f;

    // Cached references
    Rigidbody2D myRigidbody;
    PlayerMove move;

    Vector2 facing = Vector2.right;

    bool idle = true;
    bool boosting = false;

    void Start()
    {
        // Cache references
        myRigidbody = GetComponent<Rigidbody2D>();
        move = GetComponent<PlayerMove>();

        idle = true;
        boosting = false;
    }

    void FixedUpdate()
    {
        if (boosting) { myRigidbody.velocity = facing * boostSpeed; }
        else
        {
            float accel = (move.GetMaxSpeed() < myRigidbody.velocity.magnitude) ?
                breakAccel : move.GetAcceleration();
            myRigidbody.velocity = Vector2.MoveTowards(
                myRigidbody.velocity,
                move.GetTargetSpeed(),
                accel * Time.fixedDeltaTime
            );
        }
    }

    void Update()
    {
        if (idle && Input.GetButton("Boost"))
        {
            idle = false;
            StartCoroutine(StartBoost());
        }
        // Set facing direction Only when NOT boosting
        if (!boosting) { SetFacingDirection(); }
    }

    private void SetFacingDirection()
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
            facing = new Vector2(horzDirection, vertDirection);
            facing.Normalize();
            SetFacing(facing);
        }
    }

    private void SetFacing(Vector2 facing)
    {
        float angle = Mathf.Rad2Deg * Mathf.Atan2(facing.y, facing.x);
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            Quaternion.AngleAxis(angle, Vector3.forward),
            720f * Time.deltaTime
        );
    }

    IEnumerator StartBoost()
    {
        // Start propelling the ship
        boosting = true;
        yield return new WaitForSeconds(boostTime);
        // Stop the ship
        boosting = false;
        yield return new WaitForSeconds(cooldownTime);
        // Allow a new boost
        idle = true;
    }
}
