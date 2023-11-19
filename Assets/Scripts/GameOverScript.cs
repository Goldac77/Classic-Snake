using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScore;

    private void Start()
    {
        finalScore.text = PlayerPrefs.GetInt("score").ToString();
    }

    //load the main game
    public void TryAgain()
    {
        SceneManager.LoadScene("Main Scene");
    }

    //return to main menu
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
