using UnityEngine;
using TMPro;

public class LookATCamera : MonoBehaviour
{
    private GameObject MainCamera;

    private void Awake()
    {
        MainCamera = Camera.main.gameObject;
    }
    private void Update()
    {
        transform.LookAt(MainCamera.transform);
    }
}