using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NOTE:  An interface is like a type of contract that ensures that a class that uses it will always have the same type of public methods.

// an interface our other scripts can implement so that we can switch them on/off when a recycled game object is activated/deactivated
public interface IRecycle {
    void Restart();
    void Shutdown();
}

public class RecycleGameObject : MonoBehaviour {

    // declare list of other scripts that implement the IRecycle interface
    private List<IRecycle> recycleComponents;

    private void Awake() {
        // retrieves all components of the parent object which extend MonoBehavior (i.e. our other attached scripts)
        var components = GetComponents<MonoBehaviour>();

        // instantiate the components list
        recycleComponents = new List<IRecycle>();

        // checks to see which components implement the IRecycle interface, then assigns those to the list
        foreach (var component in components) {
            if(component is IRecycle) {
                recycleComponents.Add(component as IRecycle);
            }
        }

    }

    // Activates and deactivates the game object this script is attached to, as well as any other scripts attached to the G.O. which implement IRecycle interface
    public void Restart() {
        gameObject.SetActive(true);

        foreach (var component in recycleComponents) {
            component.Restart();
        }
    }

    public void Shutdown() {
        gameObject.SetActive(false);

        foreach (var component in recycleComponents) {
            component.Shutdown();
        }
    }
}
