using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// InputState, PlayerAnimationManager, and Jump scripts have a Model/View/Controller design pattern
public class PlayerAnimationManager : MonoBehaviour
{
    private Animator animator;
    private InputState inputState;

    void Awake() {
        animator = GetComponent<Animator>();
        inputState = GetComponent<InputState>();
    }

    // Update is called once per frame
    void Update()
    {
        var running = true;

        // if player's x position is moving (occurs when "pushed" by an obstacle) and his vertical velocity is below the standing threshold (he isn't jumping), then he stops running
        if(inputState.absVelX > 0 && inputState.absVelY < inputState.standingThreshold) {
            running = false;
        }

        // the animator component of the game object has its Running parameter updated so that it knows which animation frames to play based on the player's state
        animator.SetBool("Running", running);
    }
}
