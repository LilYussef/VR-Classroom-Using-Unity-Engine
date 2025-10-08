using UnityEngine;

public class AdminUpdateUIManager : MonoBehaviour
{
    public GameObject loginPanel;
    public GameObject SearchAdminPanel;
    public GameObject updateUsersPanel;
    public GameObject SearchStudentPanel;
    public GameObject SearchLecturerPanel;
    public GameObject updateFormPanel;

    public void OnUpdateAdminButtonClick()
    {
        updateUsersPanel.SetActive(false);
        updateFormPanel.SetActive(false);
        SearchAdminPanel.SetActive(true);
    }
    public void OnUpdateStudentButtonClick()
    {
        loginPanel.SetActive(false);
        updateUsersPanel.SetActive(false);
        SearchAdminPanel.SetActive(false);
        SearchStudentPanel.SetActive(true);
        SearchLecturerPanel.SetActive(false);
        updateFormPanel.SetActive(false);
    }
    public void OnUpdateLecturerButtonClick()
    {
        loginPanel.SetActive(false);
        updateUsersPanel.SetActive(false);
        SearchAdminPanel.SetActive(false);
        SearchStudentPanel.SetActive(false);
        SearchLecturerPanel.SetActive(true);
        updateFormPanel.SetActive(false);
    }
    public void OnBackButtonClick()
    {
        loginPanel.SetActive(false);
        updateUsersPanel.SetActive(true);
        SearchAdminPanel.SetActive(false);
        SearchStudentPanel.SetActive(false);
        SearchLecturerPanel.SetActive(false);
        updateFormPanel.SetActive(false);
    }
    public void OnLogoutButtonClick()
    {
        loginPanel.SetActive(true);
        updateUsersPanel.SetActive(false);
        SearchAdminPanel.SetActive(false);
        SearchStudentPanel.SetActive(false);
        SearchLecturerPanel.SetActive(false);
        updateFormPanel.SetActive(false);
    }
}
