using UnityEngine;
using TMPro;
public class AdminLoginUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_InputField AdminIdField;
    public TMP_InputField PasswordField;
    public TMP_Text DebugText;
    public GameObject AdminLoginPanel;
    public GameObject MainDashboardPanel;
    public GameObject LoginPanel;
    public async void OnLoginButtonClick()
    {
        string idText = AdminIdField.text.Trim();
        string password = PasswordField.text.Trim();

        if (string.IsNullOrEmpty(idText) || string.IsNullOrEmpty(password))
        {
            DebugText.text = "Please Fill All Fields";
            return;
        }
        if (!int.TryParse(idText, out int AdminID))
        {
            DebugText.text = "Admin ID Must Be a Number";
            return;
        }

        FirebaseDataService firebaseDataService = new();
        bool isValid = await firebaseDataService.CheckAdminLoginAsync(AdminID, password);

        if (isValid)
        {
            DebugText.text = "Login success";
            AdminLoginPanel.SetActive(false);
            MainDashboardPanel.SetActive(true);
        }
        else
        {
            DebugText.text = "Incorrect Admin ID Or Password";
        }
    }
    public void OnBackButtonClick()
    {
        AdminLoginPanel.SetActive(false);
        LoginPanel.SetActive(true);
        DebugText.text = "";
        AdminIdField.text = "";
        PasswordField.text = "";
    }
}
