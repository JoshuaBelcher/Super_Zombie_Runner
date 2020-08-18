using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// InputState, PlayerAnimationManager, and Jump scripts have a Model/View/Controller design pattern

public class InputState : MonoBehaviour
{
    public bool actionButton; // is action button pressed down or not?
    public float absVelX = 0f; // does the player's rigidbody have any x or y velocity at a given frame?
    public float absVelY = 0f; 
    public bool standing; // is the player standing? (for these purposes, is his Y velocity <= our threshold of 1) NOTE: a more complex game would check for collision box contact
    public float standingThreshold = 1;

    // store a reference to the player game object's rigidbody2D component
    private Rigidbody2D body2d;

    private void Awake() {
        body2d = GetComponent<Rigidbody2D>();    
    }


    // Update is called once per frame
    void Update()
    {
        // actionButton boolean is flagged true on any frame that any key is pressed down
        actionButton = Input.anyKeyDown;        
    }

    //NOTE: always use FixedUpdate for physics
    private void FixedUpdate() {
        // math converts the rigidbody's velocities to absolute values since we only care if the body is in motion or not
        absVelX = System.Math.Abs(body2d.velocity.x);
        absVelY = System.Math.Abs(body2d.velocity.y);

        standing = absVelY <= standingThreshold;
    }
}
