using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public AudioClip CurrentSong => musicPlayer.GetComponent<AudioSource>().clip;

    private GameObject musicPlayer;

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
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // If the loaded scene contains a GameObject with an audio source and the tag "BGMusic",
        // update musicPlayer and delete the old one
        List<GameObject> musicObjects = new(GameObject.FindGameObjectsWithTag("BGMusic"));
        musicObjects.Remove(musicPlayer);

        if (musicObjects.Count > 0)
        {
            for (int i = 1; i < musicObjects.Count; i++)
            {
                Destroy(musicObjects[i]);
            }

            if (musicObjects[0] != musicPlayer && musicObjects[0].GetComponent<AudioSource>() != null)
            {
                if (musicPlayer != null) Destroy(musicPlayer);
                musicPlayer = musicObjects[0];
                DontDestroyOnLoad(musicPlayer);

                if (Debug.isDebugBuild)
                {
                    Debug.Log($"Music player set to {musicPlayer.name} in {scene.name}.");
                }
            }
        }
    }

    public void ChangeSong(AudioClip newSong)
    {
        AudioSource audioSource = musicPlayer.GetComponent<AudioSource>();
        audioSource.clip = newSong;
        audioSource.Play();

        if (Debug.isDebugBuild)
        {
            Debug.Log($"Changed song to {newSong.name}.");
        }
    }
}
