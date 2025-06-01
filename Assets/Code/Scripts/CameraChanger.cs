using Unity.Cinemachine;
using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    [SerializeField] CinemachineCamera cam1, cam2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        cam1 = GameObject.Find("CloseCamera").GetComponent<CinemachineCamera>();
        cam2 = GameObject.Find("OutCamera").GetComponent<CinemachineCamera>();
    }
    public void CameraChange()
    {
        int temp = cam1.Priority;
        cam1.Priority = cam2.Priority;
        cam2.Priority = temp;
    }
}
