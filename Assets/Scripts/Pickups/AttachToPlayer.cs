using UnityEngine;
using UnityEngine.SceneManagement;

public class AttachToPlayer : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject player = GameManager.Instance.PlayerInstance;
        if (player != null)
        {
            transform.SetParent(player.transform);
            transform.localPosition = Vector3.zero;
        }
    }
}
