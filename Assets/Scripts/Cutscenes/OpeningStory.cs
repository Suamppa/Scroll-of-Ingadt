using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class OpeningStory : MonoBehaviour
{
    public string nextSceneName = "SampleScene";
    public PlayableDirector director;
    public GameObject[] textObjects;

    private SignalReceiver signalReceiver;

    private void OnEnable()
    {
        signalReceiver = GetComponent<SignalReceiver>();
    }

    private void Start()
    {
        int index = 0;

        // Track bindings need to be reset every time the scene is loaded.
        // Otherwise the sequence won't play after the first scene load.
        if (Debug.isDebugBuild) Debug.Log("Starting Playable Director binding...");
        foreach (var track in director.playableAsset.outputs)
        {
            director.ClearGenericBinding(track.sourceObject);

            // Bind text objects to activation tracks
            if (index < textObjects.Length && track.streamName.StartsWith("Activation Track"))
            {
                director.SetGenericBinding(track.sourceObject, textObjects[index]);
                if (Debug.isDebugBuild) Debug.Log("Activation track found and bound to text object.");
                index++;
            }
            // Bind signal receiver to signal track
            else if (track.streamName == "Signal Track")
            {
                director.SetGenericBinding(track.sourceObject, signalReceiver);
                if (Debug.isDebugBuild) Debug.Log("Signal track found and bound to signal receiver.");
            }
        }

        ResetDirector();

        if (Debug.isDebugBuild) Debug.Log("Starting director...");
        director.Play();
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    private void ResetDirector()
    {
        director.time = 0f;
        director.Stop();
        director.Evaluate();
    }
}
