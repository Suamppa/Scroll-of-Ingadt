using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public int MainMenuScene;

    private PlayerActions input;

    // private void Awake()
    // {
    //     input = new PlayerActions();
    // }

    private void OnEnable()
    {
        input.MenuControls.Enable();
        input.MenuControls.Back.performed += ctx => ResumeGame();
    }

    private void OnDisable()
    {
        input.MenuControls.Back.performed -= ctx => ResumeGame();
        input.MenuControls.Disable();
    }

    public void PauseGame(PlayerActions playerInput)
    {
        Time.timeScale = 0;
        input = playerInput;
        input.PlayerControls.Disable();
        gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        input.PlayerControls.Enable();
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(MainMenuScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
