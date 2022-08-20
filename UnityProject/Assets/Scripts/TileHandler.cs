using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handles the creation and movement of the tiles which make up the geometry of the level.

public class TileHandler : MonoBehaviour
{
    [SerializeField]
    Transform wallPrefab;

    [SerializeField]
    Transform tilePrefab;

    [SerializeField]
    Transform postPrefab;

    [SerializeField]
    GameObject electricWallPrefab;

    [SerializeField]
    Transform archPrefab;

    //Contains the relevant variables for the tiles and walls.
    public class IsingTile
    {
        public Transform tile;
        public bool isMovingUp;
        public int State;
        public bool isMovingDown;
        public Vector3 Velocity;
        public bool stateChanged;
    }

    public class ElectricWall
    {
        public GameObject wall;
        public LineRenderer lineRenderer;

        public Transform arch;
        public bool isArch;
    }

    public IsingTile[,] tileList;
    ElectricWall[] wallList;

    GameObject gameState;

    void Awake()
    {
        gameState = GameObject.Find("Game State");
    }

    public void createGeometry()
    {
        int nX = gameState.GetComponent<GameState>().nX;
        int nY = gameState.GetComponent<GameState>().nY;
        float sizeX = gameState.GetComponent<GameState>().sizeX;
        float sizeY = gameState.GetComponent<GameState>().sizeY;

        tileList = new IsingTile[nX, nY];
        wallList = new ElectricWall[2 * nX + 2 * nY];
        int wallIndex = 0;

        //Create all the tiles, walls and wallposts at the correct positions.
        for (int i = 0; i < nX; i++)
            for (int j = 0; j < nY; j++)
            {
                tileList[i, j] = new IsingTile();

                Vector3 tileCoords = getTileCoords(i, j);

                tileList[i, j].tile = Instantiate(tilePrefab);
                tileList[i, j].tile.localPosition = tileCoords;
                tileList[i, j].tile.name = "tile" + i.ToString() + "-" + j.ToString();

                tileList[i, j].State = 1;
                tileList[i, j].Velocity = Vector3.zero;

                Vector3 scl = Vector3.zero;

                scl.x = sizeX;
                scl.y = 0.1f;
                scl.z = sizeY;

                tileList[i, j].tile.localScale = scl;
           
                if (i == 0)
                {
                    wallIndex = j;

                    wallList[wallIndex] = new ElectricWall();
                    wallList[wallIndex].wall = Instantiate(electricWallPrefab);
                    wallList[wallIndex].wall.name = "wall" + wallIndex.ToString();
                    wallList[wallIndex].lineRenderer = wallList[wallIndex].wall.GetComponentInChildren<LineRenderer>();

                    Vector3 wallCoords = tileCoords;
                    wallCoords.x -= sizeX / 2f + 0.2f;

                    wallList[wallIndex].wall.GetComponent<Transform>().localPosition = wallCoords;
                    wallIndex++;
                }

                if (j == nY - 1)
                {
                    wallIndex = i + nX;

                    wallList[wallIndex] = new ElectricWall();
                    wallList[wallIndex].wall = Instantiate(electricWallPrefab);
                    wallList[wallIndex].wall.name = "wall" + wallIndex.ToString();
                    wallList[wallIndex].lineRenderer = wallList[wallIndex].wall.GetComponentInChildren<LineRenderer>();

                    Vector3 wallCoords = tileCoords;
                    wallCoords.z += sizeY / 2f + 0.2f;

                    wallList[wallIndex].wall.GetComponent<Transform>().localPosition = wallCoords;
                    wallList[wallIndex].wall.GetComponent<Transform>().Rotate(0.0f, 90.0f, 0.0f, Space.Self);
                    wallIndex++;
                }

                if (i == nX - 1)
                {
                    wallIndex = nX + nY + Mathf.Abs(j - nY + 1);

                    wallList[wallIndex] = new ElectricWall();
                    wallList[wallIndex].wall = Instantiate(electricWallPrefab);
                    wallList[wallIndex].wall.name = "wall" + wallIndex.ToString();
                    wallList[wallIndex].lineRenderer = wallList[wallIndex].wall.GetComponentInChildren<LineRenderer>();

                    Vector3 wallCoords = tileCoords;
                    wallCoords.x += sizeX / 2f + 0.2f;

                    wallList[wallIndex].wall.GetComponent<Transform>().localPosition = wallCoords;
                    wallIndex++;
                }

                if (j == 0)
                {
                    wallIndex = nX + nY + nX + Mathf.Abs(i - nX + 1);

                    wallList[wallIndex] = new ElectricWall();
                    wallList[wallIndex].wall = Instantiate(electricWallPrefab);
                    wallList[wallIndex].wall.name = "wall" + wallIndex.ToString();
                    wallList[wallIndex].lineRenderer = wallList[wallIndex].wall.GetComponentInChildren<LineRenderer>();

                    Vector3 wallCoords = tileCoords;
                    wallCoords.z -= sizeY / 2f + 0.2f;
  
                    wallList[wallIndex].wall.GetComponent<Transform>().localPosition = wallCoords;
                    wallList[wallIndex].wall.GetComponent<Transform>().Rotate(0.0f, 90.0f, 0.0f, Space.Self);
                    wallIndex++;
                }
           
                if (i == 0 && j == 0)
                {
                    Transform post = Instantiate(postPrefab);

                    Vector3 postCoords = getTileCoords(i, j);

                    postCoords.x -= sizeX / 2f + 0.2f;
                    postCoords.z -= sizeY / 2f + 0.2f;
 
                    postCoords.y += -1.15f;

                    post.localPosition = postCoords;
                }

                if (i == 0 && j == nY - 1)
                {
                    Transform post = Instantiate(postPrefab);

                    Vector3 postCoords = getTileCoords(i, j);

                    postCoords.x -= sizeX / 2f + 0.2f;
                    postCoords.z += sizeY / 2f + 0.2f;

                    postCoords.y += -1.15f;

                    post.localPosition = postCoords;
                }

                if (i == nX - 1 && j == 0)
                {
                    Transform post = Instantiate(postPrefab);

                    Vector3 postCoords = getTileCoords(i, j);

                    postCoords.x += sizeX / 2f + 0.2f;
                    postCoords.z -= sizeY / 2f + 0.2f;
         
                    postCoords.y += -1.15f;

                    post.localPosition = postCoords;
                }

                if (i == nX - 1 && j == nY - 1)
                {
                    Transform post = Instantiate(postPrefab);

                    Vector3 postCoords = getTileCoords(i, j);

                    postCoords.x += sizeX / 2f + 0.2f;
                    postCoords.z += sizeY / 2f + 0.2f;
           
                    postCoords.y += -1.15f;

                    post.localPosition = postCoords;
                }

            }

        //Flips the Ising model every 10 seconds.
        InvokeRepeating("IsingFlips", 2.5f, 10f);

        }

    //Help function to translate from ising coordinates to world coordinates.
    public Vector3 getTileCoords(int i, int j)
    {
        int nX = gameState.GetComponent<GameState>().nX;
        int nY = gameState.GetComponent<GameState>().nY;
        float sizeX = gameState.GetComponent<GameState>().sizeX;
        float sizeY = gameState.GetComponent<GameState>().sizeY;

        Vector3 topLeftCorner = Vector3.zero;
        topLeftCorner.x = -1 * Mathf.Floor((float)(nX) / 2f) * sizeX;
        topLeftCorner.z = -1 * Mathf.Floor((float)(nY) / 2f) * sizeY;

        Vector3 positionVector = Vector3.zero;
        positionVector.x = topLeftCorner.x + i * sizeX;
        positionVector.z = topLeftCorner.z + j * sizeY;

        return positionVector;
    }

    //These three functions handle the underlying Ising model which dictates how the tiles move.
    float EnergyOneSpin(int xPos, int yPos)
    {
        int nX = gameState.GetComponent<GameState>().nX;
        int nY = gameState.GetComponent<GameState>().nY;

        return -1 * tileList[xPos, yPos].State * (tileList[Mod((xPos - 1), nX), yPos].State + tileList[Mod((xPos + 1), nX), yPos].State + tileList[xPos % nX, Mod((yPos - 1), nY)].State + tileList[xPos, Mod((yPos + 1), nY)].State);
    }

    int Mod(int x, int m)
    {
        return (x % m + m) % m;
    }

    public void IsingFlips()
    {
        int nX = gameState.GetComponent<GameState>().nX;
        int nY = gameState.GetComponent<GameState>().nY;
        int numFlips = gameState.GetComponent<GameState>().numFlips;
        float Temperature = gameState.GetComponent<GameState>().Temperature;


        for (int i = 0; i < numFlips; i++)
        {
            float boltzmannFactor = Random.Range(0f, 1f);

            int flipXPosition = Random.Range(0, nX);
            int flipYPosition = Random.Range(0, nY);

            float energyBefore = EnergyOneSpin(flipXPosition, flipYPosition);

            tileList[flipXPosition, flipYPosition].State *= -1;

            tileList[flipXPosition, flipYPosition].stateChanged = true;

            float energyAfter = EnergyOneSpin(flipXPosition, flipYPosition);

            float energyDelta = energyAfter - energyBefore;

            if (energyDelta > 0 && Mathf.Exp(-1 * energyDelta / Temperature) < boltzmannFactor)
            {
                tileList[flipXPosition, flipYPosition].State *= -1;
                tileList[flipXPosition, flipYPosition].stateChanged = false;
            }

            if (tileList[flipXPosition, flipYPosition].stateChanged)
                if (tileList[flipXPosition, flipYPosition].State == 1)
                    tileList[flipXPosition, flipYPosition].Velocity = new Vector3(0f, 0.5f, 0f);
                else
                    tileList[flipXPosition, flipYPosition].Velocity = new Vector3(0f, -0.5f, 0f);

                tileList[flipXPosition, flipYPosition].stateChanged = false;

        }

    }

    void Update()
    {
        //Move the tiles every frame.
        int nX = gameState.GetComponent<GameState>().nX;
        int nY = gameState.GetComponent<GameState>().nY;

        for (int i = 0; i < nX; i++)
            for (int j = 0; j < nY; j++)
            {
                tileList[i, j].tile.localPosition += tileList[i, j].Velocity * Time.deltaTime;

                if (tileList[i, j].Velocity.y != 0f)
                {
                    if (tileList[i, j].State == 1f)
                    {
                        if (tileList[i, j].tile.localPosition.y > -0.01f)
                        {
                            Vector3 positionVector = tileList[i, j].tile.localPosition;
                            positionVector.y = 0.0f;


                            tileList[i, j].tile.localPosition = positionVector;
                            tileList[i, j].Velocity = Vector3.zero;
                        }
                    }
                    else
                    {
                        if (tileList[i, j].tile.localPosition.y < -1.99f)
                        {
                            Vector3 positionVector = tileList[i, j].tile.localPosition;
                            positionVector.y = -2.0f;

                            tileList[i, j].tile.localPosition = positionVector;
                            tileList[i, j].Velocity = Vector3.zero;
                        }
                    }
                }
            }

        //Make the electric fences move randomly to make the look electric.
        for (int i = 0; i < (2* nX + 2* nY); i++)
            for (int j = 1; j < 5; j++)
                if (!wallList[i].isArch)
                    wallList[i].lineRenderer.SetPosition(j, new Vector3(j * 0.2f, Random.Range(-0.15f, 0.15f), 0f));       
            

        }

    //Set all the tiles back up.
    public void SetAllUp()
    {
        int nX = gameState.GetComponent<GameState>().nX;
        int nY = gameState.GetComponent<GameState>().nY;

        for (int i = 0; i < nX; i++)
            for (int j = 0;j < nY; j++)
                if (tileList[i,j].State != 1)
                {
                    tileList[i, j].State = 1;
                    tileList[i,j].Velocity = new Vector3(0f, 0.5f, 0f);
                }
    }

    //Create a portal pair.
    public void CreatePBCPair(int Position)
    {
        int nX = gameState.GetComponent<GameState>().nX;
        int nY = gameState.GetComponent<GameState>().nY;

        ElectricWall wallNE = wallList[Position];
        Vector3 positionNE = wallNE.wall.transform.localPosition;
        Vector3 positionSW;


        ElectricWall wallSW;

        if (Position < nX)
        {
            int stepsFromEdge = nX - Position;

            wallSW = wallList[Position + stepsFromEdge + nY + stepsFromEdge - 1];
            positionSW = wallSW.wall.transform.localPosition;
        }
        else
        {
            int test = Position - nX;
            int stepsFromEdge = nY - test;

            wallSW = wallList[Position + stepsFromEdge + nX  + stepsFromEdge - 1];
            positionSW = wallSW.wall.transform.localPosition;

        }

        wallNE.isArch = true;
        wallSW.isArch = true;

        Destroy(wallNE.wall);
        Destroy(wallSW.wall);

        wallNE.arch = Instantiate(archPrefab);
        wallSW.arch = Instantiate(archPrefab);

        wallNE.arch.localPosition = positionNE + new Vector3(0f, .5f,0f);
        wallSW.arch.localPosition = positionSW + new Vector3(0f, .5f, 0f);

        if (Position < nX)
        {
            wallNE.arch.Rotate(0f, 90f, 0f);
            wallSW.arch.Rotate(0f, 90f, 0f);

            wallNE.arch.name = "portalXL";
            wallSW.arch.name = "portalXR";
        }
        else
        {
            wallNE.arch.name = "portalYU";
            wallSW.arch.name = "portalYD";
        }

    }


}
