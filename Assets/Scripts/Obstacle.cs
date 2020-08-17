using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Obstacle : MonoBehaviour, IRecycle {
    // an array of sprites that can be used to provide visuals for our in-game obstacles (filled via the Unity inspector)
    public Sprite[] sprites;

    public Vector2 colliderOffset = Vector2.zero;

    // every time the clone of the Obstacles prefab is reactivated, it is assigned a random sprite
    public void Restart() {

        // get reference to the sprite renderer, the component of the game object that actually displays the chosen sprite
        var renderer = GetComponent<SpriteRenderer>();
        // assigns the renderers "sprite to render" property to be a random sprite drawn from the above array
        renderer.sprite = sprites[Random.Range(0, sprites.Length)];

        // ensure the physics collider resizes to match the visual dimensions of the obstacle's assigned sprite
        // get a reference to the game objects collider component
        var collider = GetComponent<BoxCollider2D>();
        // store a reference to the size of the rendered sprite's bounding box
        var size = renderer.bounds.size;

        // adjust the y dimension of the sprite's bounding box by the amount of the collider's y offset
        size.y += colliderOffset.y;

        // the size of the collider box now matches the size of the sprite box
        collider.size = size;
        // the collider's actual offset property is changed appropriately
        collider.offset = new Vector2 (-colliderOffset.x, collider.size.y / 2 - colliderOffset.y);
    }

    // not actually used at this time; implementation is simply to satisfy the IRecyle interface requirement
    public void Shutdown() {

    }
}
