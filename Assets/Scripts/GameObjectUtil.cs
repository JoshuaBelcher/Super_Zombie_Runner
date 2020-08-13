using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a single utility class that can handle the functions of creating and destroying prefab instances
public class GameObjectUtil {
    // pass in the type of game object we want to instantiate and the position it should appear
    public static GameObject Instantiate(GameObject prefab, Vector3 pos) {
        GameObject instance = null;

        // setting a reference to a game object we will instantiate by calling the static instantitate method of the GameObject Unity class held by our prefab
        instance = GameObject.Instantiate(prefab);
        instance.transform.position = pos;

        return instance;
    }

    // passing in an object to be destroyed, then using GameObject class's static Destroy method to destroy the passed object
    public static void Destroy(GameObject gameObject) {
        GameObject.Destroy(gameObject);
    }

}
