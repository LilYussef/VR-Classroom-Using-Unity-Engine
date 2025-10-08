using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class AddLecturerUIManager : MonoBehaviour
{
    public TMP_InputField EmailField;
    public TMP_InputField PasswordField;
    public TMP_InputField nameField;
    public TMP_InputField phoneField;
    public TMP_InputField addressField;
    public TMP_InputField subjectField;
    public TMP_Text successText;
    public TMP_Text errorText;
    public GameObject successAlert;
    public GameObject errorAlert;
    public GameObject AdminDashboardPanel;
    public GameObject AddLecturerPanel;
    public GameObject LoginPanel;

    public async void OnSubmitClick()
    {
        string Email = EmailField.text;
        string Password = PasswordField.text;
        string Name = nameField.text;
        string Phone = phoneField.text;
        string Address = addressField.text;
        string Subject = subjectField.text;

        bool success = await DBManager.Service.AddLecturerAsync(Email, Password, Name, Address, Phone, Subject);

        if (success)
        {
            successText.text = "Lecturer Added Successfully";
            successAlert.SetActive(true);
            errorAlert.SetActive(false);
        }
        else
        {
            successText.text = "Error Adding A Lecturer";
            successAlert.SetActive(false);
            errorAlert.SetActive(true);
        }
    }

    public void OnHideAlertClick()
    {
        successAlert.SetActive(false);
        errorAlert.SetActive(false);
    }

    public void OnClearClick()
    {
        EmailField.text = "";
        PasswordField.text = "";
        nameField.text = "";
        phoneField.text = "";
        addressField.text = "";
        subjectField.text = "";
    }

    public void OnBackButtonClick()
    {
        AdminDashboardPanel.SetActive(true);
        AddLecturerPanel.SetActive(false);
        LoginPanel.SetActive(false);
    }
    public void OnLogoutClick()
    {
        AdminDashboardPanel.SetActive(false);
        AddLecturerPanel.SetActive(false);
        LoginPanel.SetActive(true);
    }
}
