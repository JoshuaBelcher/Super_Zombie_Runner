using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOffscreen : MonoBehaviour
{
    // represents how far off the screen a game object needs to be before it is destroyed
    public float offset = 16f;
    //NOTE: delegate is like connecting a property to a method, so that when property is called we are calling the other method
    // declared delegate is named OnDestroy and encapsulates a method of type OnDestroy and return type void which takes no arguments;
    // this represents the signature of the methods that may be attached to the event below (i.e., it defines possible event handlers)
    public delegate void OnDestroy();
    // event named DestroyCallback is handled by an OnDestroy delegate (event handler)
    // DestroyCallback does nothing in and of itself at this point; other scripts must tie methods of "OnDestroy" type to it as event handlers;
    // this allows other scripts to "respond" to the event since the methods they tied to the event will be executed when DestroyCallback is called by this script
    public event OnDestroy DestroyCallback; 

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

        if (DestroyCallback != null) {
            DestroyCallback();
        }
    }
}
