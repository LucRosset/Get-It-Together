using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerBoost), typeof(PlayerMove), typeof(PlayerFacing))]
public class PlayerMovementControl : MonoBehaviour
{
    // Cached references
    Rigidbody2D myRigidbody;
    PlayerBoost boost;
    PlayerMove move;
    PlayerFacing facing;
    Animator fireAnimator;

    [Tooltip("Speed during the boost")]
    [SerializeField] float boostSpeed = 5f;
    [Tooltip("Breaking acceleration (after boosting)")]
    [SerializeField] float breakAccel = .1f;

    void Start()
    {
        // Cache references
        myRigidbody = GetComponent<Rigidbody2D>();
        boost = GetComponent<PlayerBoost>();
        move = GetComponent<PlayerMove>();
        facing = GetComponent<PlayerFacing>();
        fireAnimator = GameObject.Find("/Player/Body/Fire").GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (boost.boosting) { myRigidbody.velocity = facing.direction * boostSpeed; }
        else
        {
            float accel = 0f;
            Vector2 targetVelocity = myRigidbody.velocity;
            // If above move.maxSpeed, break with boost's accel
            if (move.GetMaxSpeed() < myRigidbody.velocity.magnitude || !move.enabled)
            {
                accel = breakAccel;
                targetVelocity = Vector2.zero;
            }
            // Else, use move's accel (if enabled)
            else if (move.targetSpeed > 0)
            {
                accel = move.GetAcceleration();
                targetVelocity = facing.direction * move.targetSpeed;
            }
            //float accel = (move.GetMaxSpeed() < myRigidbody.velocity.magnitude) ?
            //    breakAccel : move.GetAcceleration();
            myRigidbody.velocity = Vector2.MoveTowards(
                myRigidbody.velocity,
                targetVelocity,
                accel * Time.fixedDeltaTime
            );
            // Set animator flag
            fireAnimator.SetBool("move", (move.enabled && move.targetSpeed != 0));
        }
    }

    void Update()
    {
        // Set facing direction Only when NOT boosting
        if (!boost.boosting) { facing.SetFacingDirection(); }
    }
}
