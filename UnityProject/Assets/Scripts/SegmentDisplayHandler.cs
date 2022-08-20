using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handles the in-game segment display.

public class SegmentDisplayHandler : MonoBehaviour
{
    [SerializeField]
    public Transform digit11;

    [SerializeField]
    public Transform digit12;

    [SerializeField]
    public Transform digit13;

    [SerializeField]
    public Transform digit14;

    [SerializeField]
    public Transform digit21;

    [SerializeField]
    public Transform digit22;

    [SerializeField]
    public Transform digit23;

    [SerializeField]
    public Transform digit24;

    [SerializeField]
    Material inputBlack;

    [SerializeField]
    Material inputGreen;

    static Material black;
    static Material green;

    SegmentDisplay scoreDisplay;
    SegmentDisplay tempDisplay;

    //Set-up the displays and the digits.
    void Awake()
    {
        green = inputGreen;
        black = inputBlack;

        tempDisplay = new SegmentDisplay(new SegmentDigit(digit21, black, green), new SegmentDigit(digit22, black, green), new SegmentDigit(digit23, black, green), new SegmentDigit(digit24, black, green));
        scoreDisplay = new SegmentDisplay(new SegmentDigit(digit11, black, green), new SegmentDigit(digit12, black, green), new SegmentDigit(digit13, black, green), new SegmentDigit(digit14, black, green));
    }

    //These are called to change the display.
    public void setScoreDisplay()
    {
        scoreDisplay.SetDisplay(GameObject.Find("Game State").GetComponent<GameState>().Score);
    }

    public void setTempDisplay()
    {
        //Format to deal with the period in the temp display.
        string fmt = "00.00";
        string numberString = GameObject.Find("Game State").GetComponent<GameState>().Temperature.ToString(fmt).Remove(2,1);

        tempDisplay.SetDisplay(numberString);
    }
}

//This class handles a display consisting of 4 digits.
public class SegmentDisplay
{
    SegmentDigit[] digitList;

    public SegmentDisplay(SegmentDigit Digit1, SegmentDigit Digit2, SegmentDigit Digit3, SegmentDigit Digit4)
    {
        digitList = new SegmentDigit[4];

        digitList[0] = Digit1;
        digitList[1] = Digit2;
        digitList[2] = Digit3;
        digitList[3] = Digit4;

    }

    //Set the display, either with a string of directly with a float.
    public void SetDisplay(float Number)
    {
        string fmt = "0000.##";
        string numberString = Number.ToString(fmt);

        for (int i = 0; i < 4; i++)
            digitList[i].SetDisplay(numberString[i]);
    }

    public void SetDisplay(string numberString)
    {
        for (int i = 0; i < 4; i++)
            digitList[i].SetDisplay(numberString[i]);
    }
}

//This class handles the segments of a single digit of a display.
public class SegmentDigit
{
    GameObject segment1;
    GameObject segment2;
    GameObject segment3;
    GameObject segment4;
    GameObject segment5;
    GameObject segment6;
    GameObject segment7;

    Material Green;
    Material Black;

    public SegmentDigit(Transform Digit, Material Black, Material Green)
    {
        segment1 = Digit.Find("Segment 1").gameObject;
        segment2 = Digit.Find("Segment 2").gameObject;
        segment3 = Digit.Find("Segment 3").gameObject;
        segment4 = Digit.Find("Segment 4").gameObject;
        segment5 = Digit.Find("Segment 5").gameObject;
        segment6 = Digit.Find("Segment 6").gameObject;
        segment7 = Digit.Find("Segment 7").gameObject;

        this.Green = Green;
        this.Black = Black;
    }

    //Set each segment to the correct color for each value 0 through 9.
    public void SetDisplay(char desiredNumber)
    {
        switch (desiredNumber)
        {
            case '0':
                segment1.GetComponent<Renderer>().material = Green;
                segment2.GetComponent<Renderer>().material = Green;
                segment3.GetComponent<Renderer>().material = Green;
                segment4.GetComponent<Renderer>().material = Black;
                segment5.GetComponent<Renderer>().material = Green;
                segment6.GetComponent<Renderer>().material = Green;
                segment7.GetComponent<Renderer>().material = Green;

                break;

            case '1':
                segment1.GetComponent<Renderer>().material = Black;
                segment2.GetComponent<Renderer>().material = Black;
                segment3.GetComponent<Renderer>().material = Green;
                segment4.GetComponent<Renderer>().material = Black;
                segment5.GetComponent<Renderer>().material = Green;
                segment6.GetComponent<Renderer>().material = Black;
                segment7.GetComponent<Renderer>().material = Black;

                break;

            case '2':
                segment1.GetComponent<Renderer>().material = Green;
                segment2.GetComponent<Renderer>().material = Green;
                segment3.GetComponent<Renderer>().material = Black;
                segment4.GetComponent<Renderer>().material = Green;
                segment5.GetComponent<Renderer>().material = Green;
                segment6.GetComponent<Renderer>().material = Black;
                segment7.GetComponent<Renderer>().material = Green;

                break;

            case '3':
                segment1.GetComponent<Renderer>().material = Green;
                segment2.GetComponent<Renderer>().material = Black;
                segment3.GetComponent<Renderer>().material = Green;
                segment4.GetComponent<Renderer>().material = Green;
                segment5.GetComponent<Renderer>().material = Green;
                segment6.GetComponent<Renderer>().material = Black;
                segment7.GetComponent<Renderer>().material = Green;

                break;

            case '4':
                segment1.GetComponent<Renderer>().material = Black;
                segment2.GetComponent<Renderer>().material = Black;
                segment3.GetComponent<Renderer>().material = Green;
                segment4.GetComponent<Renderer>().material = Green;
                segment5.GetComponent<Renderer>().material = Green;
                segment6.GetComponent<Renderer>().material = Green;
                segment7.GetComponent<Renderer>().material = Black;

                break;

            case '5':
                segment1.GetComponent<Renderer>().material = Green;
                segment2.GetComponent<Renderer>().material = Black;
                segment3.GetComponent<Renderer>().material = Green;
                segment4.GetComponent<Renderer>().material = Green;
                segment5.GetComponent<Renderer>().material = Black;
                segment6.GetComponent<Renderer>().material = Green;
                segment7.GetComponent<Renderer>().material = Green;

                break;

            case '6':
                segment1.GetComponent<Renderer>().material = Green;
                segment2.GetComponent<Renderer>().material = Green;
                segment3.GetComponent<Renderer>().material = Green;
                segment4.GetComponent<Renderer>().material = Green;
                segment5.GetComponent<Renderer>().material = Black;
                segment6.GetComponent<Renderer>().material = Green;
                segment7.GetComponent<Renderer>().material = Green;

                break;

            case '7':
                segment1.GetComponent<Renderer>().material = Green;
                segment2.GetComponent<Renderer>().material = Black;
                segment3.GetComponent<Renderer>().material = Green;
                segment4.GetComponent<Renderer>().material = Black;
                segment5.GetComponent<Renderer>().material = Green;
                segment6.GetComponent<Renderer>().material = Black;
                segment7.GetComponent<Renderer>().material = Black;

                break;

            case '8':
                segment1.GetComponent<Renderer>().material = Green;
                segment2.GetComponent<Renderer>().material = Green;
                segment3.GetComponent<Renderer>().material = Green;
                segment4.GetComponent<Renderer>().material = Green;
                segment5.GetComponent<Renderer>().material = Green;
                segment6.GetComponent<Renderer>().material = Green;
                segment7.GetComponent<Renderer>().material = Green;

                break;

            case '9':
                segment1.GetComponent<Renderer>().material = Green;
                segment2.GetComponent<Renderer>().material = Black;
                segment3.GetComponent<Renderer>().material = Green;
                segment4.GetComponent<Renderer>().material = Green;
                segment5.GetComponent<Renderer>().material = Green;
                segment6.GetComponent<Renderer>().material = Green;
                segment7.GetComponent<Renderer>().material = Green;

                break;
        }



    }
}
