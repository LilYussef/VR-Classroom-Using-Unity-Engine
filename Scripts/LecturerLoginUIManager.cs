using UnityEngine;
using TMPro;

public class LecturerLoginUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject LectLoginPanel;
    public GameObject MainLoginPanel;
    public GameObject LecturerDashboardPanel;
    public TMP_InputField LecturerEmailField;
    public TMP_InputField PasswordField;
    public TMP_Text DebugText;

    public async void OnLoginButtonClick()
    {
        string email = LecturerEmailField.text.Trim();
        string password = PasswordField.text.Trim();

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            DebugText.text = "Please fill all fields!";
            return;
        }

        FirebaseDataService firebaseDataService = new();
        bool isValid = await firebaseDataService.CheckLecturerLoginAsync(email, password);

        if (isValid)
        {
            DebugText.text = "Login Success";
            LectLoginPanel.SetActive(false);
            LecturerDashboardPanel.SetActive(true);
        }
        else
        {
            DebugText.text = "Incorrect Email or Password";
        }
    }

    public void OnBackButtonClick()
    {
        LectLoginPanel.SetActive(false);
        MainLoginPanel.SetActive(true);
        DebugText.text = "";
        LecturerEmailField.text = "";
        PasswordField.text = "";
    }
}