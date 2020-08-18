using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// InputState, PlayerAnimationManager, and Jump scripts have a Model/View/Controller design pattern

public class Jump : MonoBehaviour
{
    public float jumpSpeed = 240f;
    public float forwardSpeed = 20; // forward velocity added when player falls behind halfway point of screen

    // store references to the player game object's rigidbody2D and InputState components
    private Rigidbody2D body2D;
    private InputState inputState;

    private void Awake() {
        body2D = GetComponent<Rigidbody2D>();
        inputState = GetComponent<InputState>();
    }

    // Update is called once per frame
    void Update()
    {
    // if the player's input state component is flagged true for standing, and the actionButton is being pressed, we will add vertical velocity to the player's rigidbody (i.e. we'll make him jump)
     if (inputState.standing) {
            if(inputState.actionButton) {
                body2D.velocity = new Vector2(transform.position.x < 0 ? forwardSpeed : 0, jumpSpeed); // if player game object's position is past the x-axis midpoint of our screen, then we'll add forward velocity to the jump to make him catch back up
            }
        }   
    }
}
