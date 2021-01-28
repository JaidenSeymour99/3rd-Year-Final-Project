using UnityEngine;
using UnityEngine.SceneManagement;

//Main menu class, play game, quit game.
public class MainMenu : MonoBehaviour
{
    public void PlayGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    //Quits the game.
    public void QuitGame()
    {
        Application.Quit();
    }

}