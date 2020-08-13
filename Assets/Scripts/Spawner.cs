using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // an array containing all the types of prefabs our spawner script can create
    public GameObject[] prefabs;

    // the delay time between each spawned game object
    public float delay = 2.0f;

    // determines whether spawner is active, allowing it to be shut down when the game is over
    public bool active = true;

    // uses x and y values of Vector2 to represent the min and max of a time delay range
    public Vector2 delayRange = new Vector2(1, 2);

    // Start is called before the first frame update
    void Start()
    {
        ResetDelay();
        // this is a "co-routine," a way to run a script independent of the normal loop, usually coupled with a timer or wait
        StartCoroutine(EnemyGenerator());

        IEnumerator EnemyGenerator() {
            // delays the execution of the co-routine
            yield return new WaitForSeconds(delay);

            if (active) {
                // stores reference to the game object's transform component
                var newTransform = transform;

                // creates an instance of a randomly selected prefab from the above array at the position of the game object
                // this script is attached to (Quaternion.idenity is a zeroed rotation)
                GameObjectUtil.Instantiate(prefabs[Random.Range(0, prefabs.Length)], newTransform.position);
                ResetDelay(); // set new delay each time a new instantion happens

            }

            StartCoroutine(EnemyGenerator());

        }
    }

    // generates a new time delay within the range
    void ResetDelay() {
        delay = Random.Range(delayRange.x, delayRange.y);
    }
}
