using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager Instance { get; private set; }

    public GameObject CameraInstance { get => cameraInstance; }
    public GameObject PlayerInstance { get => playerInstance; }
    public GameObject HudInstance { get => hudInstance; }

    public GameObject playerCamera;
    public GameObject player;
    public GameObject hud;

    private GameObject cameraInstance;
    private ICinemachineCamera vCam;
    private GameObject playerInstance;
    private GameObject hudInstance;

    private void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        InstantiateCamera();
        InstantiatePlayer();
        InstantiateHud();

        vCam = cameraInstance.GetComponentInChildren<CinemachineVirtualCamera>();
        vCam.Follow = playerInstance.transform;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        bool hasPlayer = false;
        bool hasHud = false;
        GameObject[] objects = scene.GetRootGameObjects();

        if (Debug.isDebugBuild) Debug.Log($"Scene {scene.name} loaded. Checking {objects.Length} objects.");

        for (int i = 0; i < objects.Length; i++)
        {
            GameObject obj = objects[i];

            if (Debug.isDebugBuild) Debug.Log($"Checking {obj.name} in scene {scene.name}.");

            if (obj.CompareTag("Player") && obj != playerInstance)
            {
                hasPlayer = true;

                if (!playerInstance.activeSelf) EnablePlayer();
                playerInstance.transform.SetPositionAndRotation(obj.transform.position, obj.transform.rotation);

                if (Debug.isDebugBuild) Debug.Log($"Destroying {obj.name} in scene {scene.name}.");

                Destroy(obj);
            }
            else if (obj.CompareTag("MainCamera") && obj != cameraInstance)
            {
                cameraInstance.transform.SetPositionAndRotation(obj.transform.position, obj.transform.rotation);

                if (Debug.isDebugBuild) Debug.Log($"Destroying {obj.name} in scene {scene.name}.");

                Destroy(obj);
            }
            else if (obj.CompareTag("HUD") && obj != hudInstance)
            {
                hasHud = true;

                if (!hudInstance.activeSelf) hudInstance.SetActive(true);

                if (Debug.isDebugBuild) Debug.Log($"Destroying {obj.name} in scene {scene.name}.");

                Destroy(obj);
            }
        }

        if (!hasPlayer)
        {
            DisablePlayer();
        }
        if (!hasHud)
        {
            hudInstance.SetActive(false);
        }
    }

    public void ResetGame()
    {
        DestroyHud();
        DestroyPlayer();
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

    private void EnablePlayer()
    {
        if (playerInstance != null && !playerInstance.activeSelf)
        {
            playerInstance.SetActive(true);

            if (cameraInstance != null)
            {
                vCam.Follow = playerInstance.transform;
            }
        }
    }

    private void DisablePlayer()
    {
        if (playerInstance != null && playerInstance.activeSelf)
        {
            playerInstance.SetActive(false);
        }
    }

    // public void MovePlayerTo(Vector3 position)
    // {
    //     playerInstance.transform.position = position;
    // }

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
