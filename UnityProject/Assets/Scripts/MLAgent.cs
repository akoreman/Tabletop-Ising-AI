using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class MLAgent : Agent
{
    public float forceMultiplier = 1f;

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
        sensor.AddObservation(this.transform.localPosition);

        // agent velocity
        sensor.AddObservation(rigidBody.velocity.x);
        sensor.AddObservation(rigidBody.velocity.y);
        sensor.AddObservation(rigidBody.velocity.z);

        // temperature pickup positions
        sensor.AddObservation(pickups.GetComponent<TempPickups>().upPickup.transform.position.x);
        sensor.AddObservation(pickups.GetComponent<TempPickups>().upPickup.transform.position.z);

        sensor.AddObservation(pickups.GetComponent<TempPickups>().downPickup.transform.position.x);
        sensor.AddObservation(pickups.GetComponent<TempPickups>().downPickup.transform.position.z);

        for (int i = 0; i < gameState.GetComponent<GameState>().nX; i++)
            for (int j = 0; j < gameState.GetComponent<GameState>().nY; j++)
                sensor.AddObservation(levelGeometry.GetComponent<TileHandler>().tileList[i,j].tile.localPosition.y);
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

        if (StepCount > 1000)
        {
            EndWithReward(gameState.GetComponent<GameState>().Score);
            //EndWithReward(StepCount);
        }

        // when fall off platform
        if (this.transform.localPosition.y < -1.95f)
        {
            EndWithReward(gameState.GetComponent<GameState>().Score);
            //EndWithReward(StepCount);
        }
    }

    void EndWithReward(float reward)
    {
        SetReward(reward);
        print(reward);
        EndEpisode();
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }
}
