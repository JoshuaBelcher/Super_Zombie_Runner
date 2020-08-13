using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOffscreen : MonoBehaviour
{
    // represents how far off the screen a game object needs to be before it is destroyed
    public float offset = 16f;

    private bool offscreen;
    private float offscreenX = 0;
    private Rigidbody2D body2d;

    // stores a reference to the actual game object's rigidbody component
    private void Awake() {
        body2d = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        // determines where the X-position of the object will be when it is no longer on the screen
        offscreenX = (Screen.width / PixelPerfectCamera.pixelsToUnits) / 2 + offset;
    }

    // Update is called once per frame
    void Update()
    {
        var posX = transform.position.x;
        var dirX = body2d.velocity.x;

        // determines if an object is currently offscreen, left or right side, based on its X-position and velocity direction
        if (Mathf.Abs(posX) > offscreenX) {
            if (dirX < 0 && posX < -offscreenX) {
                offscreen = true;
            } else if (dirX > 0 && posX > offscreenX) {
                offscreen = true;
            }
        } else {
            offscreen = false;
        }

        if (offscreen) {
            OnOutOfBounds();
        }
    }

    public void OnOutOfBounds() {
        offscreen = false;
        // destroys the game object to which this script is attached
        GameObjectUtil.Destroy(gameObject);
    }
}
