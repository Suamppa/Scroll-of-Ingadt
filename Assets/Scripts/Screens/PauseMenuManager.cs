using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    public int MainMenuScene;

    private PlayerActions Input
    {
        get => GameManager.Instance.PlayerInstance.GetComponent<PlayerInputHandler>().Input;
    }

    private void Awake()
    {
        // Add listener to the resume button
        // This is necessary since the HUD is instantiated at runtime
        GetComponentInChildren<Button>().onClick.AddListener(ResumeGame);
    }

    private void OnEnable()
    {
        Input.MenuControls.Enable();
        Input.MenuControls.Back.performed += ctx => ResumeGame();
    }

    private void OnDisable()
    {
        if (Input != null)
        {
            Input.MenuControls.Back.performed -= ctx => ResumeGame();
            Input.MenuControls.Disable();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        Input.PlayerControls.Disable();
        gameObject.SetActive(true);
    }

    public void ResumeGame()
    {

        Input.PlayerControls.Enable();
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
