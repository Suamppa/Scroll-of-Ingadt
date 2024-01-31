using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningStory : MonoBehaviour
{
    private void OnEnable()
    {
        // Only specifing the sceneName or sceneBuildIndex will load the Scene with the Single mode
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
}
