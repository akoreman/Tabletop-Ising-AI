using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handles the placement of the pick-ups.

public class TempPickups : MonoBehaviour
{
    [SerializeField]
    Transform upPrefab;

    [SerializeField]
    Transform downPrefab;

    [SerializeField]
    Transform fieldPrefab;

    [SerializeField]
    Transform pbcPrefab;

    public Transform upPickup;
    public Transform downPickup;
    public Transform fieldPickup;
    public Transform pbcPickup;

    Transform[] pickupArray;

    GameObject gameState;
    GameObject levelGeometry;

    void Awake()
    {
        gameState = GameObject.Find("Game State");
        levelGeometry = GameObject.Find("LevelGeometry");

        pickupArray = new Transform[4];
    }

    public void placeUpPickup(int i, int j)
    {
        if (upPickup)
            Destroy(upPickup.gameObject);

        upPickup = Instantiate(upPrefab);
        upPickup.name = "uppickup";
        pickupArray[0] = upPickup;
        upPickup.localPosition = levelGeometry.GetComponent<TileHandler>().getTileCoords(i, j);
        upPickup.localPosition  += new Vector3(0f,0.06f,0f);

        if (checkCollision(0))
            placeUpPickup(Random.Range(0, gameState.GetComponent<GameState>().nX), Random.Range(0, gameState.GetComponent<GameState>().nY));

        
    }

    public void placeDownPickup(int i, int j)
    {
        if (downPickup)
            Destroy(downPickup.gameObject);

        downPickup = Instantiate(downPrefab);
        downPickup.name = "downpickup";
        pickupArray[1] = downPickup;
        downPickup.localPosition = levelGeometry.GetComponent<TileHandler>().getTileCoords(i, j);
        downPickup.localPosition += new Vector3(0f, 0.06f, 0f);

        if (checkCollision(1))
            placeDownPickup(Random.Range(0, gameState.GetComponent<GameState>().nX), Random.Range(0, gameState.GetComponent<GameState>().nY));

    }

    public void placeFieldPickup(int i, int j)
    {
        if (fieldPickup)
            Destroy(fieldPickup.gameObject);

        fieldPickup = Instantiate(fieldPrefab);
        fieldPickup.name = "fieldpickup";
        pickupArray[2] = fieldPickup;
        fieldPickup.localPosition = levelGeometry.GetComponent<TileHandler>().getTileCoords(i, j);
        fieldPickup.localPosition += new Vector3(0f, 0.3f, 0f);

        if (checkCollision(2))
            placeFieldPickup(Random.Range(0, gameState.GetComponent<GameState>().nX), Random.Range(0, gameState.GetComponent<GameState>().nY));

    }

    public void placePBCPickup(int i, int j)
    {
        if (pbcPickup)
            Destroy(pbcPickup.gameObject);

        pbcPickup = Instantiate(pbcPrefab);
        pbcPickup.name = "pbcpickup";
        pickupArray[3] = pbcPickup;
        pbcPickup.localPosition = levelGeometry.GetComponent<TileHandler>().getTileCoords(i, j);
        pbcPickup.localPosition += new Vector3(0f, 0.3f, 0f);

        if (checkCollision(3))
            placePBCPickup(Random.Range(0, gameState.GetComponent<GameState>().nX), Random.Range(0, gameState.GetComponent<GameState>().nY));

    }

    public bool checkCollision(int checkPickup)
    {
        for (int i = 0; i < 4; i++)
        {
            if (i == checkPickup || pickupArray[i] == null) { continue; }

            if (pickupArray[checkPickup].localPosition == pickupArray[i].localPosition) { return true; }
        }

        return false;
    }

    public void shufflePickupPositions()
    {
        placeUpPickup(Random.Range(0, gameState.GetComponent<GameState>().nX), Random.Range(0, gameState.GetComponent<GameState>().nY));
        placeDownPickup(Random.Range(0, gameState.GetComponent<GameState>().nX), Random.Range(0, gameState.GetComponent<GameState>().nY));
    }
}
