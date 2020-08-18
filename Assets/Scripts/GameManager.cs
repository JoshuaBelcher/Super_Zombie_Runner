using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;

    private TimeManager timeManager;
    private GameObject player;
    private GameObject floor;
    private Spawner spawner;

    private void Awake() {
        // locate (by name) and return the game objects for the floor and spawner (return the actual attached script component for spawner) and time manager
        floor = GameObject.Find("Foreground");
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
        timeManager = GetComponent<TimeManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // code to ensure the floor is positioned properly regardless of screen size
        var floorHeight = floor.transform.localScale.y;

        var pos = floor.transform.position;
        pos.x = 0;
        pos.y = -((Screen.height / PixelPerfectCamera.pixelsToUnits) / 2) + (floorHeight / 2);
        floor.transform.position = pos;

        spawner.active = false;

        ResetGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // event handler for player death (DestroyOffscreen) that matches signature of OnDestroy delegate
    void OnPlayerKilled() {
        // spawner is turned off when the player dies
        spawner.active = false;

        // when player dies, the link between the DestroyCallback delegate and the OnPlayerKilled method must be broken to prevent possible memory leak
        var playerDestroyScript = player.GetComponent<DestroyOffscreen>();
        playerDestroyScript.DestroyCallback -= OnPlayerKilled;

        // reset player's velocity on death so that when he respawns, he'll still fall straight down
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        // transition time to zero to stop the game gracefully
        timeManager.ManipulateTime(0, 5.5f);
    }

    void ResetGame() {
        // turn on obstacle spawner
        spawner.active = true;

        // have GameObjectUtil script instantiate a new prefab at designated position (in this case, Player prefab is assigned via the Unity Inspector)
        player = GameObjectUtil.Instantiate(playerPrefab, new Vector3(0, (Screen.height / PixelPerfectCamera.pixelsToUnits) / 2, 0));

        // NOTE from Murach C#: To handle an event from another class, you create an instance of the class that raises the event and assign it to a class variable.
        // Then, you declare an event handler with a signature that matches the delegate's signature. Finally, you wire the event handler to the event...

        // store reference to the player game object's DestroyOffscreen component
        var playerDestroyScript = player.GetComponent<DestroyOffscreen>();
        // add the OnPlayerKilled method from this Game Manager script to the DestroyCallback event in the DestroyOffscreen script
        playerDestroyScript.DestroyCallback += OnPlayerKilled;
    }
}
