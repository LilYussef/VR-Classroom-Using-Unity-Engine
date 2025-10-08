using UnityEngine;
using TMPro;

public class StudentLoginUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject LoginPanel;
    public GameObject StudentLoginPanel;
    public GameObject StudentDashboardPanel;
    public TMP_InputField AcademicIdInput;
    public TMP_InputField PasswordInput;
    public TMP_Text DebugText;

    public async void OnLoginButtonClick()
    {
        string idText = AcademicIdInput.text;
        string Password = PasswordInput.text;

        if (string.IsNullOrEmpty(idText) || string.IsNullOrEmpty(Password))
        {
            DebugText.text = "Please fill in all fields";
            return;
        }

        if (!int.TryParse(idText, out int AcademicID))
        {
            DebugText.text = "Please Enter Correct Academic Number";
            return;
        }

        FirebaseDataService firebaseDataService = new();
        bool isValid = await firebaseDataService.CheckStudentLoginAsync(AcademicID, Password);

        if (isValid)
        {
            DebugText.text = "Login Successful!";
            StudentLoginPanel.SetActive(false);
            StudentDashboardPanel.SetActive(true);
        }
        else
        {
            DebugText.text = "Incorrect Academic ID or Password";
        }
            

    }
    public void OnBackButtonClick()
    {
        StudentLoginPanel.SetActive(false);
        LoginPanel.SetActive(true);
    }
}
