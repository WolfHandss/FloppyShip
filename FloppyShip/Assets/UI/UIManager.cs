using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private Rocket Player;
    [SerializeField] private ObstacleManager OM;
    //score
    private int Score;
    [SerializeField] private Text ScoreDisplay;
    //Buttons
    [SerializeField] private GameObject StartScreen;
    [SerializeField] private GameObject PauseButton;
    [SerializeField] private GameObject GameOverScreen;
    [SerializeField] private GameObject PauseScreen;

    private int FPSTarget = 30;
    private void Awake()
    {
        //FrameRate
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = FPSTarget;
    }

    //private void Update()
    //{
    //    if (Application.targetFrameRate != FPSTarget)
    //        Application.targetFrameRate = FPSTarget;
    //}

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rocket>();

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        Time.timeScale = 1f;

        //if stargame scene is started
        if (sceneName == "StartGame")
        {
            StartScreen.SetActive(true);
            ScoreDisplay.enabled = false;
            GameOverScreen.SetActive(false);
            PauseScreen.SetActive(false);
            PauseButton.SetActive(false);
        }
        //if game scene is started
        else if (sceneName == "Game")
        {
            GameOverScreen.SetActive(false);
            PauseScreen.SetActive(false);
            PauseButton.SetActive(true);
            StartScreen.SetActive(false);
            ScoreDisplay.enabled = true;
        }   
    }

    private void Update()
    {
        //Pause Button
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
    //Death function
    public void Dead()
    {
        Player.Death();
        OM.PlayerDied();
        GameOverScreen.SetActive(true);
        PauseButton.SetActive(false);
    }
    //pause button
    public void Pause()
    {
        Time.timeScale = 0f;
        PauseButton.SetActive(false);
        PauseScreen.SetActive(true);
    }
    //resume button
    public void Resume()
    {
        Time.timeScale = 1f;
        PauseButton.SetActive(true);
        PauseScreen.SetActive(false);
    }
    //main menu button
    public void MainMenu()
    {
        SceneManager.LoadScene("StartGame");
    }
    //restart button
    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }
    //start button
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
    //quit button
    public void Quit()
    {
        Quit();
    }
    //update text score
    private void ShowScore()
    {
        ScoreDisplay.text = Score.ToString();
    }
    //add to score
    public void AddToScore()
    {
        Score++;
        ShowScore();
    }
}