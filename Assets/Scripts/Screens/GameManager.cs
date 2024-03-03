using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton pattern
    public static GameManager Instance { get; private set; }

    public GameObject PlayerInstance { get => playerInstance; }
    public GameObject HudInstance { get => hudInstance; }

    public GameObject player;
    public GameObject playerCamera;
    public GameObject hud;

    private GameObject playerInstance;
    private GameObject cameraInstance;
    private GameObject hudInstance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        InstantiateCamera();
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)
        {
            ResetGame();
        }
        else if (scene.name == "SampleScene" || scene.name == "LVL2")
        {
            if (playerInstance != null) playerInstance.SetActive(true);
            if (hudInstance != null) hudInstance.SetActive(true);
        }
        else
        {
            if (hudInstance != null) hudInstance.SetActive(false);
            if (playerInstance != null) playerInstance.SetActive(false);
        }
    }

    public void StartGame()
    {
        InstantiatePlayer();
        InstantiateHud();
        hudInstance.SetActive(false);
        playerInstance.SetActive(false);
    }

    public void ResetGame()
    {
        DestroyPlayer();
        DestroyHud();
    }

    public void InstantiatePlayer()
    {
        DestroyPlayer();
        playerInstance = Instantiate(player);
        DontDestroyOnLoad(playerInstance);
    }

    private void DestroyPlayer()
    {
        if (playerInstance != null)
        {
            Destroy(playerInstance);
        }
    }

    public void MovePlayerTo(Vector3 position)
    {
        playerInstance.transform.position = position;
    }

    public void InstantiateCamera()
    {
        DestroyCamera();
        cameraInstance = Instantiate(playerCamera);
        DontDestroyOnLoad(cameraInstance);
    }

    private void DestroyCamera()
    {
        if (cameraInstance != null)
        {
            Destroy(cameraInstance);
        }
    }

    public void InstantiateHud()
    {
        DestroyHud();
        hudInstance = Instantiate(hud);
        DontDestroyOnLoad(hudInstance);
    }

    private void DestroyHud()
    {
        if (hudInstance != null)
        {
            Destroy(hudInstance);
        }
    }
}
