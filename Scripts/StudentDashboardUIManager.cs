using UnityEngine;
using TMPro;

public class StudentDashboardUIManager : MonoBehaviour
{
    public GameObject StudentDashboardPanel;
    public GameObject JoinLecturePanel;
    public GameObject LoginPanel;

    public void OnJoinLectureButtonClick()
    {
        StudentDashboardPanel.SetActive(false);
        JoinLecturePanel.SetActive(true);
        LoginPanel.SetActive(false);
    }
    public void OnLogoutClick()
    {
        StudentDashboardPanel.SetActive(false);
        JoinLecturePanel.SetActive(false);
        LoginPanel.SetActive(true);
    }
}