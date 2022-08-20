using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Handles the spacebar press to start.

public class Menu : MonoBehaviour
{
    public Button nextButton;
    public Button easyButton;
    public Button normalButton;
    public Button hardButton;

    public GameObject firstScreen;
    public GameObject secondScreen;


    void Awake()
    {
        nextButton.onClick.AddListener(NextButtonClick);
        easyButton.onClick.AddListener(EasyButtonClick);
        normalButton.onClick.AddListener(NormalButtonClick);
        hardButton.onClick.AddListener(HardButtonClick);

        firstScreen.SetActive(true);
        secondScreen.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene("MainGame");
        }
    }

    void NextButtonClick()
    {
        firstScreen.SetActive(false);
        secondScreen.SetActive(true);
    }

    void EasyButtonClick()
    {
        CrossGameVariables.DIFFICULTY = "easy";
        SceneManager.LoadScene("MainGame");
    }

    void NormalButtonClick()
    {
        CrossGameVariables.DIFFICULTY = "normal";
        SceneManager.LoadScene("MainGame");
    }

    void HardButtonClick()
    {
        CrossGameVariables.DIFFICULTY = "hard";
        SceneManager.LoadScene("MainGame");
    }
}
