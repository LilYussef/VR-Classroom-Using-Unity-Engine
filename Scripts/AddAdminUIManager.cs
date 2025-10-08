using UnityEngine;
using TMPro;

public class AddAdminUIManager : MonoBehaviour
{
    public TMP_InputField AdminIdField;
    public TMP_InputField PasswordField;
    public TMP_Text SuccessText;
    public TMP_Text ErrorText;
    public GameObject SuccessAlert;
    public GameObject ErrorAlert;
    public GameObject AdminDashboardPanel;
    public GameObject AddAdminPanel;
    public GameObject LoginPanel;

    public async void OnSubmitClick()
    {
        int adminID = int.Parse(AdminIdField.text);
        string password = PasswordField.text;

        bool success = await DBManager.Service.AddAdminAsync(adminID, password);

        if (success)
        {
            SuccessText.text = "Admin Added Successfully";
            SuccessAlert.SetActive(true);
            ErrorAlert.SetActive(false);
        }
        else
        {
            ErrorText.text = "Error Adding Admin";
            SuccessAlert.SetActive(false);
            ErrorAlert.SetActive(true);
        }
    }
    public void OnHideAlertClick()
    {
        SuccessAlert.SetActive(false);
        ErrorAlert.SetActive(false);
    }
    public void OnClearButtonClick()
    {
        AdminIdField.text = "";
        PasswordField.text = "";
    }
    public void OnBackButtonClick()
    {
        AdminDashboardPanel.SetActive(true);
        AddAdminPanel.SetActive(false);
    }
    public void OnLogoutClick()
    {
        AdminDashboardPanel.SetActive(false);
        AddAdminPanel.SetActive(false);
        LoginPanel.SetActive(true);
    }
}
