using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    public AudioClip song;

    private AudioClip previousSong;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Debug.isDebugBuild)
            {
                Debug.Log($"Player entered {gameObject.name}.");
            }

            AudioClip currentSong = MusicManager.Instance.CurrentSong;
            if (currentSong != song)
            {
                previousSong = currentSong;
                MusicManager.Instance.ChangeSong(song);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            MusicManager.Instance.ChangeSong(previousSong);
        }
    }
}
