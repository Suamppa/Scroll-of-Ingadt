using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class OpeningStory : MonoBehaviour
{
    public PlayableDirector director;

    private void OnEnable()
    {
        // Set the Timeline asset to start
        director.Stop();
        director.time = 0;

        GameManager.Instance.StartGame();
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
}
