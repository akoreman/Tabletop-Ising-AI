using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class MLAgent : Agent
{
    public float forceMultiplier = .2f;

    GameObject gameState;
    GameObject pickups;
    GameObject ball;
    GameObject levelGeometry;

    Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        gameState = GameObject.Find("Game State");
        pickups = GameObject.Find("Pickups");
        ball = GameObject.Find("Ball");
        levelGeometry = GameObject.Find("LevelGeometry");
    }

    public override void OnEpisodeBegin()
    {
        gameState.GetComponent<GameState>().resetGame();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Agent positions
        sensor.AddObservation(this.transform.localPosition.x);
        //sensor.AddObservation(this.transform.localPosition.y);
        sensor.AddObservation(this.transform.localPosition.z);

        // agent velocity
        sensor.AddObservation(rigidBody.velocity.x);
        //sensor.AddObservation(rigidBody.velocity.y);
        sensor.AddObservation(rigidBody.velocity.z);

        // temperature pickup positions
        sensor.AddObservation(pickups.GetComponent<TempPickups>().upPickup.transform.localPosition.x);
        sensor.AddObservation(pickups.GetComponent<TempPickups>().upPickup.transform.localPosition.z);

        sensor.AddObservation(pickups.GetComponent<TempPickups>().downPickup.transform.localPosition.x);
        sensor.AddObservation(pickups.GetComponent<TempPickups>().downPickup.transform.localPosition.z);
        /*
        for (int i = 0; i < gameState.GetComponent<GameState>().nX; i++)
            for (int j = 0; j < gameState.GetComponent<GameState>().nY; j++)
                sensor.AddObservation(levelGeometry.GetComponent<TileHandler>().tileList[i,j].tile.localPosition.y);
        */
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        Vector3 controlSignal = Vector3.zero;

        controlSignal.x = actionBuffers.ContinuousActions[0];
        controlSignal.z = actionBuffers.ContinuousActions[1];
        /*
        if (actionBuffers.ContinuousActions[2] > 0.5)
            ball.GetComponent<BallBehaviour>().pressJumpButton();
        */
        rigidBody.AddForce(controlSignal * forceMultiplier);

        // End after N steps
        if (StepCount > 5000)
        {
            print("ended after too many steps.");
            EndWithReward(1, StepCount);
        }

        if (gameState.GetComponent<GameState>().Score > 20)
        {
            EndWithReward(gameState.GetComponent<GameState>().Score, StepCount);
        }

        if (gameState.GetComponent<GameState>().Score < 10)
        {
            EndWithReward(gameState.GetComponent<GameState>().Score, StepCount);
        }

        // End when fallen off platform
        if (this.transform.localPosition.y < -1.95f)
        {
            EndWithReward(gameState.GetComponent<GameState>().Score, 0 );
        }
    }

    void EndWithReward(float reward, int stepcount)
    {
        SetReward(reward - stepcount/(float) 5000);
        print(reward - stepcount / (float)5000);
        EndEpisode();
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }
}
