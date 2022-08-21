using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This handles the startup of a new game, sets all proper variables and sets up the creation of the level geometry.

public class CreateGeometry : MonoBehaviour
{
    [SerializeField]
    int nX = 10;

    [SerializeField]
    int nY = 10;

    [SerializeField]
    float sizeX = 1f;

    [SerializeField]
    float sizeY = 1f;

    [SerializeField]
    int numFlips = 3;

    [SerializeField]
    float startTemperature = 5f;

    GameObject gameState;
    GameObject foregroundGeometry;
    GameObject pickups;

    // Connect variables to the relevant gameObjects at awake.
    void Awake()
    {
        gameState = GameObject.Find("Game State");
        foregroundGeometry = GameObject.Find("Foreground Geometry");
        pickups = GameObject.Find("Pickups");
    }

    // At start, retrieve difficulty, set global variables and create the geometry.
    void Start()
    {
        SetNumFlipsDifficulty();

        gameState.GetComponent<GameState>().setGlobalParams(nX, nY, sizeX, sizeY, numFlips, startTemperature);

        foregroundGeometry.GetComponent<SegmentDisplayHandler>().setScoreDisplay();
        foregroundGeometry.GetComponent<SegmentDisplayHandler>().setTempDisplay();

        GetComponent<TileHandler>().createGeometry();

        // Place the initial temperature pickups.
        pickups.GetComponent<TempPickups>().placeUpPickup(Random.Range(0,nX), Random.Range(0, nY));
        pickups.GetComponent<TempPickups>().placeDownPickup(Random.Range(0, nX), Random.Range(0, nY));
    }

    
    void Update()
    {
        if (Input.GetKeyDown("x") && gameState.GetComponent<GameState>().hasUpPickup)
        {
            GetComponent<TileHandler>().SetAllUp();
            gameState.GetComponent<GameState>().hasUpPickup = false;
            gameState.GetComponent<GameState>().fieldOnScreen = false;

            foregroundGeometry.GetComponent<BackgroundHandler>().foregroundFieldButtonPress();
        }
    }   
    
    // Retrieve the difficulty from the crossgamevariables and set the number of flips accordingly.
    void SetNumFlipsDifficulty()
    {
        if (CrossGameVariables.DIFFICULTY == "easy")
            numFlips = 10;

        if (CrossGameVariables.DIFFICULTY == "normal")
            numFlips = 100;

        if (CrossGameVariables.DIFFICULTY == "hard")
            numFlips = 1000;
    }
}
