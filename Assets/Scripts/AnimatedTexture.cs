using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedTexture : MonoBehaviour
{   
    // create a public property to represent the speed that our texture will be animated in
    public Vector2 speed = Vector2.zero;

    // default offset of the texture before we start scrolling
    private Vector2 offset = Vector2.zero;
    private Material material;

    // Start is called before the first frame update
    void Start()
    {
        // accessing the texture material attached to the background quad to be stored in the reference above
        material = GetComponent<Renderer>().material;

        // set the initial offset to the actual offset of the texture material's offset
        offset = material.GetTextureOffset("_MainTex");
    }

    // Update is called once per frame
    void Update()
    {
        // increment the offset by the speed value multipled by the time that has elapsed since the last frame was rendered
        // (this helps keep offset consistent despite framerate)
        offset += speed * Time.deltaTime;

        // set the texture material's offset to match the new calculated offset value
        material.SetTextureOffset("_MainTex", offset);
    }
}
