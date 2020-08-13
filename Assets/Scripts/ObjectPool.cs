using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // reference to the type of game object prefab this object pool will manage
    public RecycleGameObject prefab;

    // a list of the recycled game objects that form this object pool
    private List<RecycleGameObject> poolInstances = new List<RecycleGameObject>();

    // create a new instance of a prefab to go into this pool
    private RecycleGameObject CreateInstance (Vector3 pos) {
        // We call the game object's own static instantiate method
        var clone = GameObject.Instantiate(prefab);
        // we set the new instance's position to the one passed in
        clone.transform.position = pos;
        // ensures instances are nested within the object pool in hierarchy view
        clone.transform.parent = transform; // essentially "ObjectPools.transform," this makes ObjectPools gameobject the parent of the instance clones

        // new instance is added to the list of RecycleGameObjects
        poolInstances.Add(clone);

        return clone;
    }

    public RecycleGameObject NextObject(Vector3 pos) {
        RecycleGameObject instance = null;

        foreach (var go in poolInstances) {
            if(go.gameObject.activeSelf != true) {
                instance = go;
                instance.transform.position = pos;
            }
        }

        if (instance == null) {
            instance = CreateInstance(pos);
        }


        instance.Restart();

        return instance;
    }
}
