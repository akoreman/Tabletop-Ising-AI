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

    GameObject gameState;
    GameObject levelGeometry;

    void Awake()
    {
        gameState = GameObject.Find("Game State");
        levelGeometry = GameObject.Find("LevelGeometry");
    }

    public void placeUpPickup(int i, int j)
    {
        upPickup = Instantiate(upPrefab);
        upPickup.localPosition = levelGeometry.GetComponent<TileHandler>().getTileCoords(i, j) + new Vector3(0f,0.06f,0f);
        upPickup.name = "uppickup";
    }

    public void placeDownPickup(int i, int j)
    {
        downPickup = Instantiate(downPrefab);
        downPickup.localPosition = levelGeometry.GetComponent<TileHandler>().getTileCoords(i, j) + new Vector3(0f, 0.06f, 0f); ;
        downPickup.name = "downpickup";
    }

    public void placeFieldPickup(int i, int j)
    {
        fieldPickup = Instantiate(fieldPrefab);
        fieldPickup.localPosition = levelGeometry.GetComponent<TileHandler>().getTileCoords(i, j);
        fieldPickup.localPosition += new Vector3(0f, 0.3f, 0f);

        fieldPickup.name = "fieldpickup";
    }

    public void placePBCPickup(int i, int j)
    {
        pbcPickup = Instantiate(pbcPrefab);
        pbcPickup.localPosition = levelGeometry.GetComponent<TileHandler>().getTileCoords(i, j);
        pbcPickup.localPosition += new Vector3(0f, 0.3f, 0f);
 
        pbcPickup.name = "pbcpickup";
    }

}
