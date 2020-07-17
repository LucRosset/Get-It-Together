using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFacing : MonoBehaviour
{

    public Vector2 direction { get; private set; } = Vector2.right;

    public void SetFacingDirection()
    {
        // Get inputs
        float horzDirection = Input.GetAxis("Horizontal");
        bool horzPressed = Input.GetButton("Horizontal");
        float vertDirection = Input.GetAxis("Vertical");
        bool vertPressed = Input.GetButton("Vertical");

        // Set target speed direction
        if (horzPressed || vertPressed)
        {
            horzDirection = (horzPressed) ? Mathf.Sign(horzDirection) : 0f;
            vertDirection = (vertPressed) ? Mathf.Sign(vertDirection) : 0f;
            direction = new Vector2(horzDirection, vertDirection);
            direction.Normalize();
            
            // Set facing direction
            float angle = Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.AngleAxis(angle, Vector3.forward),
                720f * Time.deltaTime
            );
        }
    }
}
