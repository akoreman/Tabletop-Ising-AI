using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class MLAgent : Agent
{
    public float forceMultiplier = 10;

    GameObject gameState;
    GameObject pickups;

    Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        gameState = GameObject.Find("Game State");
        pickups = GameObject.Find("Pickups");
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
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        Vector3 controlSignal = Vector3.zero;

        controlSignal.x = actionBuffers.ContinuousActions[0];
        controlSignal.z = actionBuffers.ContinuousActions[1];
        gameState.GetComponent<GameState>().wantJump = actionBuffers.ContinuousActions[2] > 0.5 ? true : false;

        rigidBody.AddForce(controlSignal * forceMultiplier);

        // when fall off platform
        if (this.transform.localPosition.y < -1.95f)
        {
            EndWithReward(gameState.GetComponent<GameState>().Score);
        }
    }

    void EndWithReward(float reward)
    {
        SetReward(reward);

        EndEpisode();
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }
}
