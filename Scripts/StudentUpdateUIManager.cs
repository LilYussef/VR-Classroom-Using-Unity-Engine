using UnityEngine;

public class StudentUpdateUIManager : MonoBehaviour
{
    public GameObject loginPanel;
    public GameObject SearchAdminPanel;
    public GameObject updateUsersPanel;
    public GameObject SearchStudentPanel;
    public GameObject SearchLecturerPanel;
    public GameObject updateFormPanel;

    public void OnUpdateAdminButtonClick()
    {
        loginPanel.SetActive(false);
        updateUsersPanel.SetActive(false);
        SearchAdminPanel.SetActive(true);
        SearchStudentPanel.SetActive(false);
        SearchLecturerPanel.SetActive(false);
        updateFormPanel.SetActive(false);
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
