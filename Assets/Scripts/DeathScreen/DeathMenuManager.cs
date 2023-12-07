using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{

    // Build number of scene to start when start button is pressed

    public void Retry()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}