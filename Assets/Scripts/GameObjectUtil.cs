using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a single utility class that can handle the functions of creating and destroying prefab instances
public class GameObjectUtil {
    // dictionary keyed to the type of recycled game object with a value representing the corresponding object pool
    private static Dictionary<RecycleGameObject, ObjectPool> pools = new Dictionary<RecycleGameObject, ObjectPool>();

    // pass in the type of game object we want to instantiate and the position it should appear
    public static GameObject Instantiate(GameObject prefab, Vector3 pos) {
        GameObject instance = null;

        // check to see if the prefab has a recycle script
        var recycledScript = prefab.GetComponent<RecycleGameObject>();
        if (recycledScript != null) {
            var pool = GetObjectPool(recycledScript);
            instance = pool.NextObject(pos).gameObject;
        } else {
            // setting a reference to a game object we will instantiate by calling the static instantitate method of the GameObject Unity class held by our prefab
            instance = GameObject.Instantiate(prefab);
            instance.transform.position = pos;
        }


        return instance;
    }

    // passing in an object to be recycled/destroyed
    public static void Destroy(GameObject gameObject) {

        var recycleGameObject = gameObject.GetComponent<RecycleGameObject>();

        // if a RecycleGameObject component was actuall attached to this object, then it will be recycled instead of destroyed
        // NOTE: "Recycled" means the object is rendered inactive but remains in the scene, ready to be re-activated at a future time
        // (less overhead than creating new items and destroying them repeatedly)
        if (recycleGameObject != null) {
            recycleGameObject.Shutdown();
        } else {
            // using GameObject class's static Destroy method to destroy the passed object
            GameObject.Destroy(gameObject);
        }
    }

    // returns an object pool from the pools dictionary based on the type of recycled game object referenced, assuming the dictionary contains it
    private static ObjectPool GetObjectPool(RecycleGameObject reference) {
        ObjectPool pool = null;

        if (pools.ContainsKey(reference)) {
            pool = pools[reference];
        } else {
            // if pool does not already exist in dictionary, we create a new one in a container and give it a name based on the game object type referenced
            var poolContainer = new GameObject(reference.gameObject.name + "ObjectPool");
            // adds ObjectPool script component to the new container
            pool = poolContainer.AddComponent<ObjectPool>();
            // set pool prefab property to reference so that the pool knows what type of objects to create
            pool.prefab = reference;
            // add to pool dictionary
            pools.Add(reference, pool);

        }

        return pool;
    }
}
