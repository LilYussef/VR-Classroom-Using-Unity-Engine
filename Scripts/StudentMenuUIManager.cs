using UnityEngine;

public class StudentMenuUIManager : MonoBehaviour
{
    public GameObject VolumeSlider;
    public void OnVolumeButtonClick()
    {
        VolumeSlider.SetActive(true);
    }
}
