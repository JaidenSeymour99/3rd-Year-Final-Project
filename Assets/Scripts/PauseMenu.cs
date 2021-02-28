using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool GameIsOver = false;
    public GameObject pauseMenuUI;
    public GameObject gameoverMenuUI;

    void Start()
    {
        //fixing a bug that made it show the game overscene again once you respawn.
        GameIsOver = false;
    }
    void Update()
    {   
        // if the game is not over
        if(!GameIsOver){     
            // if the player hit esc
            if(Input.GetKeyDown(KeyCode.Escape))
            {   
                // if the game is paused and esc is pressed, continue .
                if(GameIsPaused)
                {
                    Resume();
                } 
                // else pause the game.
                else 
                {
                    Pause();
                }
            }
        }
        else 
        {
            // pausing the game when you get to 0 health.
            GameOverPause();
        }
    }

// while the game is playing this will be the state, it will have the pause menu hidden and will have time as normal, and setting the bool to false so we know the game is not paused.
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

// while the game is paused time stops so nothign can move, it displays the pause menu and it changes the bool to let everything know it is paused.
    void Pause()
    {   
        
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void GameOverPause()
    {
        //not sure why but the gameover paues palyes twice 
        gameoverMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        GameIsOver = false;
        Cursor.lockState = CursorLockMode.None;

    }
    public void Restart()
    {   
        gameoverMenuUI.SetActive(false);
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.name);
        Time.timeScale = 1f;
        GameIsOver = false;
        GameIsPaused = false;
    }
    //public so that the buttons can access it
    //goes to the menu and turns time back on so everything isnt frozen when you press play again.
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    
    //public so that the buttons can access it
    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }
}
