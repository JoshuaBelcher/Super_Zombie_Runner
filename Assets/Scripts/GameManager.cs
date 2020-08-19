using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    // field to hold the text game object we will drag into here in the Unity inspector
    public Text continueText;

    private float blinkTime = 0f;
    private bool blink;
    private bool gameStarted;
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

        Time.timeScale = 0;

        continueText.text = "PRESS ANY BUTTON TO START";
    }

    // Update is called once per frame
    void Update()
    {
        // checking to see if game has ended, then any keypress will restart the game
        if (!gameStarted && Time.timeScale ==0) {
            if(Input.anyKeyDown) {
                timeManager.ManipulateTime(1, 1f);
                ResetGame();
            }
        }

        // since time is stopped when game ends, we have to manually increment and switch blinking on/off to make the visual effect
        if (!gameStarted) {
            blinkTime++;

            if(blinkTime % 40 == 0) {
                blink = !blink;
            }

            // we access the canvas renderer component of the Text game object we drug in to actually make the text blink via controlling alpha (opacity)
            continueText.canvasRenderer.SetAlpha(blink ? 0 : 1);
        }
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
        gameStarted = false;

        // modify continue text for restart rather than first game
        continueText.text = "PRESS ANY BUTTON TO RESTART";
    }

    void ResetGame() {
        // turn on obstacle spawner
        spawner.active = true;

        // have GameObjectUtil script instantiate a new prefab at designated position (in this case, Player prefab is assigned via the Unity Inspector)
        player = GameObjectUtil.Instantiate(playerPrefab, new Vector3(0, (Screen.height / PixelPerfectCamera.pixelsToUnits) / 2 + 100, 0));

        // NOTE from Murach C#: To handle an event from another class, you create an instance of the class that raises the event and assign it to a class variable.
        // Then, you declare an event handler with a signature that matches the delegate's signature. Finally, you wire the event handler to the event...

        // store reference to the player game object's DestroyOffscreen component
        var playerDestroyScript = player.GetComponent<DestroyOffscreen>();
        // add the OnPlayerKilled method from this Game Manager script to the DestroyCallback event in the DestroyOffscreen script
        playerDestroyScript.DestroyCallback += OnPlayerKilled;

        gameStarted = true;

        // hides continue message text once game has started
        continueText.canvasRenderer.SetAlpha(0);

    }
}
