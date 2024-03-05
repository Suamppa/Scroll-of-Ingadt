using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class OpeningStory : MonoBehaviour
{
    // Workaround to prevent getting stuck in the intro scene due to a likely Timeline bug
    public static bool played = false;

    public GameObject playableDirector;

    private PlayableDirector director;
    private SignalReceiver signalReceiver;

    private void OnEnable()
    {
        director = Instantiate(playableDirector).GetComponent<PlayableDirector>();
        signalReceiver = GetComponent<SignalReceiver>();
    }

    private void Start()
    {
        if (played)
        {
            LoadLevel();
            return;
        }

        foreach (var track in director.playableAsset.outputs)
        {
            if (track.streamName == "Signal Track")
            {
                director.SetGenericBinding(track.sourceObject, signalReceiver);
                if (Debug.isDebugBuild) Debug.Log("Signal track found and bound to signal receiver.");
                break;
            }
        }
        ResetDirector();

        if (Debug.isDebugBuild) Debug.Log("Playing intro scene...");
        director.Resume();
    }

    public void LoadLevel()
    {
        played = true;
        // ResetDirector();
        SceneManager.LoadScene("SampleScene");
    }

    private void ResetDirector()
    {
        director.time = 0f;
        director.Evaluate();
    }
}
