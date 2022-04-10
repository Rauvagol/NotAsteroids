using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    //for accessing the Large Asteroid
    public GameObject LargeAsteroid;

    //stores values for ui and game mechanic reason
    private int _score;
    private int _highScore;
    private int _asteroidsRemaining;
    private int _lives;
    private int _wave;
    
    //asteroid per wave formula is base+(wave*increase) I intentionally did not make it exponential..... this time
    private const int BaseAsteroidCount = 3;
    private const int IncreaseEachWave = 1;
    
    //variables used for storing and displaying ui elements
    public Text ScoreText;
    public Text LivesText;
    public Text WaveText;
    public Text HighScoreText;
    
    //x and y dimensions of the screen size from center in unity units, the ratio of screen height:width should be the same as the ratio of screenHeight:screenWidth
    private const int ScreenHeight = 18;
    private const int ScreenWidth = 32;

    //runs once
    private void Start(){
        //sets the in-game high score to the saved high score
        _highScore = PlayerPrefs.GetInt("High Score");
        //starts the game
        BeginGame();
    }

    private void BeginGame()
    {
        //start with no score, 3 lives, and on wave 1
        
        _score = 0;
        _lives = 3;
        _wave = 1;

        //assembles the UI
        ScoreText.text = "SCORE: " + _score;
        HighScoreText.text = "HIGH SCORE: " + _highScore;
        LivesText.text = "LIVES: " + _lives;
        WaveText.text = "WAVE: " + _wave;

        //creates the asteroids
        SpawnAsteroids();
    }
    
    private void SpawnAsteroids(){
        //wipes out existing asteroids and bullets
        DestroyExistingAsteroids();
        DestroyExistingBullets();

        //gets the number of asteroids to spawn
        _asteroidsRemaining = BaseAsteroidCount+ _wave * IncreaseEachWave;

        //creates the appropriate number of asteroids
        for (var i = 0; i < _asteroidsRemaining; i++)
        {
            //makes a new large asteroid, with random position within the screen, and a random rotation
            Instantiate(LargeAsteroid,
                new Vector3(Random.Range(-ScreenWidth, ScreenWidth), Random.Range(-ScreenHeight, ScreenHeight)),
                Quaternion.Euler(0, 0, Random.Range(0, 360)));
        }

        //updates the displayed wave text
        WaveText.text = "WAVE: " + _wave;
    }
    
    //runs each frame
    private void Update()
    {
        //escape to quit
        if (Input.GetKey("escape")){
            Application.Quit();
        }

        if (Input.GetKey("k"))
        {
            
        }
        
        //r to restart, intentionally left in the update method for the "shuffling" effect of the asteroids
        if (Input.GetKey("r")){
            BeginGame();
        }
    }

    //run if player gets hit

    public void DecrementLives()
    {
        //reduces lives by one and updates ui
        _lives--;
        LivesText.text = "LIVES: " + _lives;

        //restarts if the player is out of lives
        if (_lives < 1){
            BeginGame();
        }
    }

    //run when a large or medium asteroid is destroyed

    public void SplitAsteroid(){
        //asteroid count goes up by 2 dues to splitting
        _asteroidsRemaining += 2;
    }

    //run when a small asteroid is destroyed

    public void DecrementAsteroids(){
        _asteroidsRemaining--;
    }

    //runs when spawning new asteroids to clean any stragglers
    private static void DestroyExistingAsteroids(){
        
        //makes an array of large asteroids
        var asteroidLargeArray = GameObject.FindGameObjectsWithTag("Large Asteroid");
        //then loops through it, destroying them all
        foreach (var current in asteroidLargeArray){
            Destroy(current);
        }
        
        //makes an array of medium asteroids
        var asteroidMediumArray = GameObject.FindGameObjectsWithTag("Medium Asteroid");
        //then loops through it, destroying them all
        foreach (var current in asteroidMediumArray){
            Destroy(current);
        }
        
        //makes an array of small asteroids
        var asteroidSmallArray = GameObject.FindGameObjectsWithTag("Small Asteroid");
        //then loops through it, destroying them all
        foreach (var current in asteroidSmallArray){
            Destroy(current);
        }
    }
    
    //runs when spawning new asteroids to clean any stragglers
    private static void DestroyExistingBullets(){
        
        //makes an array of bullets
        var bulletArray = GameObject.FindGameObjectsWithTag("Bullet");
        //then loops through it, destroying them all
        foreach (var current in bulletArray){
            Destroy(current);
        }
    }

    //adds the specified amount to the score
    public void IncrementScore(int scoreGained)
    {
        //increases and updates the score
        _score += scoreGained;
        ScoreText.text = "SCORE:" + _score;

        //also updates the high score if the score is higher than it
        if (_score > _highScore){
            _highScore = _score;
            HighScoreText.text = "HIGH SCORE: " + _highScore;

            //saves high score for future retrieval
            PlayerPrefs.SetInt("High Score", _highScore);
        }

        //increases wave count if that was the last asteroid
        Console.WriteLine(_asteroidsRemaining);
        if (_asteroidsRemaining > 1) return;
        _wave++;
        //and makes a new set
        SpawnAsteroids();
    }
}