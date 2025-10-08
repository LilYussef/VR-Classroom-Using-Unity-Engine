using UnityEngine;

public class LecturerDashboardUIManager : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject DashboardPanel;
    public GameObject CreateLecturePanel;

    void Start()
    {
        ShowMainDashboard();
    }
    public void ShowMainDashboard()
    {
        DashboardPanel.SetActive(true);
        MainMenuPanel.SetActive(false);
    }
    public void OnCreateLectureClick()
    {
        DashboardPanel.SetActive(false);
        MainMenuPanel.SetActive(false);
        CreateLecturePanel.SetActive(true);
    }
    public void OnLogoutClick()
    {
        DashboardPanel.SetActive(false);
        CreateLecturePanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }
}
