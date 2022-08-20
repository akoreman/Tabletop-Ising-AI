using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]

//Controls the camera to lazily follow the player and to rotate up when entering the area below the field.

public class Camera : MonoBehaviour
{
    [SerializeField]
    Transform focus = default;

    [SerializeField, Range(1f, 40f)]
    float distance = 5f;

    [SerializeField, Min(0f)]
    float focusRadius = 1f;

    [SerializeField]
    float maxCameraRotation;

    [SerializeField]
    float cameraMovementSpeed;

    [SerializeField]
    float cameraTiltSpeed;

    Vector3 focusPoint;
    float ballHeight;
    float angle;

    GameObject gameState;

    // Final camera position and rotation focused on the score display.
    Vector3 finalCameraPosition = new Vector3(-0.36f, 5.3f, -8.43f);
    Vector3 finalCameraAngles = new Vector3(90f, 0f, 0f);

    void Awake()
    {
        focusPoint = focus.position;
        gameState = GameObject.Find("Game State");
    }

    //Move and rotate the camera after the rest of each frame has been done.
    void LateUpdate()
    {
        if (gameState.GetComponent<GameState>().gameAlive)
        {
            UpdateFocusPoint();
            Vector3 lookDirection = transform.forward;
            transform.localPosition = focusPoint - lookDirection * distance;

            UpdateCameraTilt();
        }
        else
        {
            MoveCameraScorePosition();

            // Enable the game over overlay when the camera reaches the final position.
            if (this.transform.localPosition == finalCameraPosition && this.transform.localEulerAngles == finalCameraAngles)
            {
                this.GetComponentInChildren<Canvas>().enabled = true;
            }
        }
    }

    //Make the cameras focus lazily follow the ball.
    void UpdateFocusPoint()
    {
        Vector3 targetPoint = focus.position;

        if (focusRadius > 0f)
        {
            float distance = Vector3.Distance(targetPoint, focusPoint);

            if (distance > focusRadius)
                focusPoint = Vector3.Lerp(targetPoint, focusPoint, focusRadius / distance);
        }
        else
            focusPoint = targetPoint;
    }

    void UpdateCameraTilt()
    {
        ballHeight = focus.localPosition.y;

        if (ballHeight < 0f)
            angle = 90f;
        else
            angle = 30f;

        float currentAngle = transform.localEulerAngles.x;
        currentAngle = Mathf.MoveTowards(currentAngle, angle, maxCameraRotation * Time.deltaTime);

        transform.localEulerAngles = new Vector3(currentAngle, 0f, 0f);
    }

    // Lerp the camera to the score screen.
    void MoveCameraScorePosition()
    {
        Vector3 currentCameraPosition = this.transform.localPosition;
        Vector3 currentCameraAngles = this.transform.localEulerAngles;

        currentCameraPosition.x = Mathf.MoveTowards(currentCameraPosition.x, finalCameraPosition.x, cameraMovementSpeed * Time.deltaTime);
        currentCameraPosition.y = Mathf.MoveTowards(currentCameraPosition.y, finalCameraPosition.y, cameraMovementSpeed * Time.deltaTime);
        currentCameraPosition.z = Mathf.MoveTowards(currentCameraPosition.z, finalCameraPosition.z, cameraMovementSpeed * Time.deltaTime);

        currentCameraAngles.x = Mathf.MoveTowards(currentCameraAngles.x, finalCameraAngles.x, cameraTiltSpeed * Time.deltaTime);
        currentCameraAngles.y = Mathf.MoveTowards(currentCameraAngles.y, finalCameraAngles.y, cameraTiltSpeed * Time.deltaTime);
        currentCameraAngles.z = Mathf.MoveTowards(currentCameraAngles.z, finalCameraAngles.z, cameraTiltSpeed * Time.deltaTime);

        this.transform.localPosition = currentCameraPosition;
        this.transform.localEulerAngles = currentCameraAngles;
    }
}

