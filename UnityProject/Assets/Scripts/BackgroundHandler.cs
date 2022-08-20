using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handles the in-game control panel, minus the segment display, see SegmentDispalyHandles for that.

public class BackgroundHandler : MonoBehaviour
{
    [SerializeField]
    Transform stickPivot;

    [SerializeField]
    Transform jumpButton;

    [SerializeField]
    Transform fieldButton;

    [SerializeField]
    public Transform fieldLight;

    [SerializeField]
    float rotationSpeed = 1f;

    [SerializeField]
    float maxRotation = 30f;

    [SerializeField]
    float springBack = 0.5f;

    [SerializeField]
    float buttonSpeed = 0.01f;

    float angleX = 0f;
    float angleZ = 0f;

    GameObject gameState;

    void Awake()
    {
        gameState = GameObject.Find("Game State");
    }

    
    void Update()
    {
        //Move the in-game joystick in sync with the player input.
        Vector2 Rotation = Vector3.zero;

        Rotation.x = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        Rotation.y = Input.GetAxis("Vertical") * rotationSpeed * Time.deltaTime;

        float paddedX = angleX + maxRotation;
        float paddedZ = angleZ + maxRotation;

        if (Rotation.y == 0f)
        {
            angleX = Mathf.MoveTowards(paddedX, maxRotation, springBack * Time.deltaTime) - maxRotation;        
        }
        else
        {
            if (Mathf.Sign(Rotation.y) == 1)
                angleX = Mathf.MoveTowards(paddedX, 2f * maxRotation, Mathf.Abs(Rotation.y) * Time.deltaTime * rotationSpeed) - maxRotation;
            else
                angleX = Mathf.MoveTowards(paddedX, 0f, Mathf.Abs(Rotation.y) * Time.deltaTime * rotationSpeed) - maxRotation;
        }

        if (Rotation.x == 0f)
        {
            angleZ = Mathf.MoveTowards(paddedZ, maxRotation, springBack * Time.deltaTime) - maxRotation;
        }
        else
        {
            if (Mathf.Sign(Rotation.x) == 1)
                angleZ = Mathf.MoveTowards(paddedZ, 0f, Mathf.Abs(Rotation.x) * Time.deltaTime * rotationSpeed) - maxRotation;
            else
                angleZ = Mathf.MoveTowards(paddedZ, 2f * maxRotation, Mathf.Abs(Rotation.x) * Time.deltaTime * rotationSpeed) - maxRotation;
        }

        Vector3 eulerRotation = stickPivot.localEulerAngles;
        stickPivot.localEulerAngles = new Vector3(angleX, 0f, angleZ);

        //If the player pushes the jump button move the in-game button up and down.
        if (Input.GetKeyDown("space"))
            StartCoroutine(moveButton(jumpButton, 0.05f));
    }

    //Function handles the field power-up button to be depressed.
    public void foregroundFieldButtonPress()
    {
        StartCoroutine(moveButton(fieldButton, 0.1f));
        StartCoroutine(rotateLight(fieldLight, false));
    }

    IEnumerator moveButton(Transform Button, float upPosition)
    {
        Vector3 Position = Button.localPosition;
        bool hasBottomed = false;

        while (!hasBottomed | Position.y != upPosition)
        {
            if (!hasBottomed)
            {
                Position.y = Mathf.MoveTowards(Position.y, 0f, buttonSpeed * Time.deltaTime);

                if (Position.y == 0)
                    hasBottomed = true;
            }
            else
                Position.y = Mathf.MoveTowards(Position.y, upPosition, buttonSpeed * Time.deltaTime);

            Button.localPosition = Position;

            yield return null;
        }
    }

    //Function which rotates the indicator to the correct color.
    public void rotateLightGreen()
    {
        StartCoroutine(rotateLight(fieldLight, true));
    }

    public IEnumerator rotateLight(Transform Light, bool buttonIsGreen)
    {
        float totalRotation = 0f;

        while(totalRotation < 180f)
        {
            Light.Rotate(new Vector3(rotationSpeed * Time.deltaTime, 0f,0f));

            totalRotation += rotationSpeed * Time.deltaTime;

            yield return null;
        }

        if (buttonIsGreen)
            Light.localEulerAngles = new Vector3(270f, 0f, 0f);
        else
            Light.localEulerAngles = new Vector3(90f, 0f, 0f);
    }

}
