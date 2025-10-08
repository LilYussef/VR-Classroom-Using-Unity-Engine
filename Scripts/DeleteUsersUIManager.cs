using UnityEngine;
using TMPro;

public class DeleteUsersUIManager : MonoBehaviour
{
    public GameObject DeleteUsersPanel;
    public GameObject AdminDeletePanel;
    public GameObject StudentDeletePanel;
    public GameObject LecturerDeletePanel;
    public GameObject LoginPanel;
    public GameObject UpdateDataPanel;
    public GameObject ViewDataPanel;

    public void OnDeleteAdminClick()
    {
        DeleteUsersPanel.SetActive(false);
        AdminDeletePanel.SetActive(true);
    }
    public void OnDeleteStudentClick()
    {
        DeleteUsersPanel.SetActive(false);
        StudentDeletePanel.SetActive(true);
    }
    public void OnDeleteLecturerClick()
    {
        DeleteUsersPanel.SetActive(false);
        LecturerDeletePanel.SetActive(true);
    }
    public void OnLogoutClick()
    {
        DeleteUsersPanel.SetActive(false);
        LoginPanel.SetActive(true);
    }
    public void OnViewDataClick()
    {
        DeleteUsersPanel.SetActive(false);
        ViewDataPanel.SetActive(true);
    }
    public void OnUpdateDataclick()
    {
        DeleteUsersPanel.SetActive(false);
        UpdateDataPanel.SetActive(true);
    }
}
