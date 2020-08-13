using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantVelocity : MonoBehaviour
{
    public Vector2 velocity = Vector2.zero;

    public Rigidbody2D body2d;

    private void Awake() {
        // store a reference to the actual Rigidbody2D component of the game object this script is applied to
        body2d = GetComponent<Rigidbody2D>();
    }

    // a special update, called a certain number of times per frame, reserved for our physics calculations
    private void FixedUpdate() {
        body2d.velocity = velocity;
    }
}
