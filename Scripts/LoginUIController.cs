using UnityEngine;

public class LoginUIController : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject adminLoginPanel;
    public GameObject studentLoginPanel;
    public GameObject lecturerLoginPanel;
    public GameObject aboutUsPanel;

    void Start(){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ShowAdminLogin()
    {
        mainMenuPanel.SetActive(false);
        adminLoginPanel.SetActive(true);
    }
    public void ShowStudentLogin()
    {
        mainMenuPanel.SetActive(false);
        studentLoginPanel.SetActive(true);
    }
    public void ShowLecturerLogin()
    {
        mainMenuPanel.SetActive(false);
        lecturerLoginPanel.SetActive(true);
    }
    public void AboutUsButtonClick()
    {
        mainMenuPanel.SetActive(false);
        aboutUsPanel.SetActive(true);
    }
    public void BackToMainMenu()
    {
        mainMenuPanel.SetActive(true);
        adminLoginPanel.SetActive(false);
        studentLoginPanel.SetActive(false);
        lecturerLoginPanel.SetActive(false);
    }
}
