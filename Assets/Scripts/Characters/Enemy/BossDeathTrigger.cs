using UnityEngine;

public class BossDeathTrigger : MonoBehaviour
{
    public GameObject exitBarrier;

    private void OnEnable()
    {
        if (exitBarrier != null && !exitBarrier.activeSelf)
        {
            exitBarrier.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        if (exitBarrier != null && exitBarrier.activeSelf)
        {
            exitBarrier.SetActive(false);
        }
    }
}
