using UnityEngine;

public class AdminDashBoardUIManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject mainDashboardPanel;
    public GameObject addStudentPanel;
    public GameObject addLecturerPanel;
    public GameObject addAdminPanel;
    public GameObject updateDataPanel;
    public GameObject DeleteUsersPanel;

    void Start()
    {
        ShowMainDashboard();
    }
    public void ShowMainDashboard()
    {
        mainDashboardPanel.SetActive(true);
        addStudentPanel.SetActive(false);
        addLecturerPanel.SetActive(false);
        addAdminPanel.SetActive(false);
    }
    public void OnAddStudentPressed()
    {
        mainDashboardPanel.SetActive(false);
        addStudentPanel.SetActive(true);
    }
    public void OnAddLecturerPressed()
    {
        mainDashboardPanel.SetActive(false);
        addLecturerPanel.SetActive(true);
    }
    public void OnUpdateDataPanelPressed()
    {
        mainDashboardPanel.SetActive(false);
        addAdminPanel.SetActive(true);
    }
    public void OnLogoutPressed()
    {
        mainMenuPanel.SetActive(true);
        mainDashboardPanel.SetActive(false);
        addStudentPanel.SetActive(false);
        addLecturerPanel.SetActive(false);
        addAdminPanel.SetActive(false);
    }
    public void OnUpdateDataButtonClick()
    {
        mainMenuPanel.SetActive(false);
        mainDashboardPanel.SetActive(false);
        addStudentPanel.SetActive(false);
        addLecturerPanel.SetActive(false);
        addAdminPanel.SetActive(false);
        updateDataPanel.SetActive(true);
    }
    public void OnDeleteUsersClick()
    {
        mainMenuPanel.SetActive(false);
        mainDashboardPanel.SetActive(false);
        addStudentPanel.SetActive(false);
        addLecturerPanel.SetActive(false);
        addAdminPanel.SetActive(false);
        updateDataPanel.SetActive(false);
        DeleteUsersPanel.SetActive(true);
    }
}
