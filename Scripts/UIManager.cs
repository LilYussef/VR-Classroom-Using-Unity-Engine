using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject loginPanel;
    public GameObject UICamera;

    void Start()
    {
        loginPanel.SetActive(true);
        UICamera.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ClosePanel()
    {
        loginPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
