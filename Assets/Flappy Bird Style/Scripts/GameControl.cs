﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour 
{
	public static GameControl instance;			//A reference to our game control script so we can access it statically.
	public Text scoreText;						//A reference to the UI text component that displays the player's score.
	public GameObject gameOvertext;
    public Text missileText;

    private int missileNumber = 10;
	private int score = 0;						//The player's score.
	public bool gameOver = false;				//Is the game over?
    private float timeStillOver = 0f;

	void Awake()
	{
		//If we don't currently have a game control...
		if (instance == null)
			//...set this one to be it...
			instance = this;
		//...otherwise...
		else if(instance != this)
			//...destroy this one because it is a duplicate.
			Destroy (gameObject);
        missileText.text = missileNumber.ToString();
    }

    void Update()
	{
		//If the game is over and the player has pressed some input...
		if (gameOver && (Input.GetMouseButtonDown(0) || (timeStillOver > 0.2f && GameControl.instance.GetComponent<SerialInputs>().getValues(GameControl.instance.GetComponent<SerialInputs>().JUMP)))) 
		{
			//...reload the current scene.
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
        if (gameOver)
            timeStillOver += Time.deltaTime;
	}

    public int GetMissileNumber()
    {
        return missileNumber;
    }

    public void DecreaseMissile()
    {
        if (missileNumber > 0)
            missileNumber -= 1;
        missileText.text = missileNumber.ToString();
    }

    public void BirdScored()
	{
		//The bird can't score if the game is over.
		if (gameOver)	
			return;
		//If the game is not over, increase the score...
		score++;
		//...and adjust the score text.
		scoreText.text = "Score: " + score.ToString();
	}

	public void BirdDied()
	{
		//Activate the game over text.
		gameOvertext.SetActive (true);
		//Set the game to be over.
		gameOver = true;
	}

    void OnApplicationQuit()
    {
        GetComponent<SerialInputs>().closeDevice();
        Debug.Log("Quit");
    }
}
