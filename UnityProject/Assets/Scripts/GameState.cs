using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Class to keep track of the global gamestate.

public class GameState : MonoBehaviour
{
    public bool hasUpPickup = false;
    public bool pbcOnScreen = false;
    public bool fieldOnScreen = false;

    public int nX;
    public int nY;

    public float sizeX;
    public float sizeY;

    public int numFlips;

    public float Temperature;

    public int Score;

    public int numPBC;
    public int[] availablePairs;

    public bool gameAlive;
    public int consectUp;

    public bool wantJump;

    public void setGlobalParams(int nX, int nY, float sizeX, float sizeY, int numFlips, float startTemperature)
    {
        this.nX = nX;
        this.nY = nY;
        this.sizeX = sizeX;
        this.sizeY = sizeY;
        this.numFlips = numFlips;
        this.Temperature = startTemperature;
        this.Score = 0;
        this.numPBC = 0;
        this.gameAlive = true;
        this.consectUp = 0;

        availablePairs = new int[nX + nY];

        for (int i = 0; i < nX + nY; i++)
        {
            this.availablePairs[i] = i;
        }
    }

    public void resetGame()
    {
        SceneManager.LoadScene("MainGame");
    }

}
