using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public int levelToLoad;
    public Vector3 spawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // StartCoroutine(LoadLevel());
            SceneManager.LoadScene(levelToLoad);
            // Scene level2 = SceneManager.GetSceneByBuildIndex(levelToLoad);
            // SceneManager.MoveGameObjectToScene(collision.gameObject, level2);
            GameManager.Instance.MovePlayerTo(spawnPoint);
        }
    }

    // private IEnumerator LoadLevel()
    // {
    //     AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelToLoad.buildIndex);

    //     while (!asyncLoad.isDone)
    //     {
    //         yield return null;
    //     }
    // }
}
