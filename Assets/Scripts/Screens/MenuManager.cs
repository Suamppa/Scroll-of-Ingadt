using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Build number of scene to start when start button is pressed
    public int gameStartScene;

    private void OnEnable()
    {
        GameManager.Instance.ResetGame();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(gameStartScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
