using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PixelPerfectCamera : MonoBehaviour
{
    // setting up some scale and resolution default reference properties
    public static float pixelsToUnits = 1f; // this represents the ratio defined in our individual visual elements
    public static float scale = 1f; // baseline scale

    public Vector2 nativeResolution = new Vector2(240, 160); //native resolution of Gameboy Advance

    private void Awake() {
        // reference to the retrieved Camera component of the game object that this PixelPerfectCamera script is attached to
        var camera = GetComponent<Camera>();

        // Depending on the size of the display, the number of units viewed by the Unity camera will scale accordingly. Since the number of pixels per unit in the visual elements does not change,
        // the end result is that the visual elements are effectively scaled to match their original intent
        if (camera.orthographic) {                          // is the project correctly set to orthographic projection for a 2D game?
            scale = Screen.height / nativeResolution.y;     // scale ratio will equal the height of the user's display divided by the height of the native resolution
            pixelsToUnits *= scale;                         // pixelsToUnits ratio will be multiplied by the scale ratio
            camera.orthographicSize = (Screen.height / 2.0f) / pixelsToUnits;   // camera's ortho size (half-height) is equal to half the display height divided by the pixelsToUnits ratio
        }
    }
}
