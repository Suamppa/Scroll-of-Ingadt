using Cinemachine;
using UnityEngine;

// Assigns the player as the Cinemachine virtual camera's follow target
public class PlayerCamera : MonoBehaviour
{
    private void Start()
    {
        ICinemachineCamera vCam = Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera;
        vCam.Follow = transform;
    }
}
