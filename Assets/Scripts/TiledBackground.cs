using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiledBackground : MonoBehaviour
{
    // represents the value of the Transform.scale of our background quad gameobject
    public int textureSize = 32;
    public float test;
    // Start is called before the first frame update
    void Start()
    {
        var newWidth = Mathf.Ceil(Screen.width / (textureSize * PixelPerfectCamera.scale)); // use of Ceil prevents rounding down so that we cannot possibly be left with a value that will leave untiled gaps at the edge of screen
        var newHeight = Mathf.Ceil(Screen.height / (textureSize * PixelPerfectCamera.scale)); // PixelPerfectCamera.scale is the ratio of the screen size to the native resolution. This is used since te original size of the texture is being amplified when scaled up to meet screen size.

        // the background quad is now scaled up to fill the entire camera view, plus a little extra (thanks to the rounding up)
        transform.localScale = new Vector3(newWidth * textureSize, newHeight * textureSize, 1); // since Unity is a 3D engine, it must have a Vector3 with scale, lest Z value equal 0 and object disappear
 
        // the texture material displayed by the background quad's Renderer component has its texture scaled to be suitable for the new quad size established above
        GetComponent<Renderer>().material.mainTextureScale = new Vector3(newWidth, newHeight, 1); // in Inspector mainTextureScale is "Particle Texture : Tiling" property
    }

}
