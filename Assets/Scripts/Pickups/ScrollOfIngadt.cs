using UnityEngine;
using UnityEngine.SceneManagement;

public class ScrollOfIngadt : MonoBehaviour
{
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadSceneAsync("EndingScene");
            Destroy(gameObject);
        }
    }
}
