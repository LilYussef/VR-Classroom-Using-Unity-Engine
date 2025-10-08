using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class AddStudentUIManager : MonoBehaviour
{
    public TMP_InputField academicIDField;
    public TMP_InputField passwordField;
    public TMP_InputField nameField;
    public TMP_InputField phoneField;
    public TMP_InputField addressField;
    public TMP_InputField academicYearField;
    public TMP_Text SuccessText;
    public TMP_Text ErrorText;
    public GameObject SuccessAlert;
    public GameObject ErrorAlert;
    public GameObject AdminDashboardPanel;
    public GameObject AddStudentPanel;
    public GameObject LoginPanel;

    public async Task OnSubmittClick()
    {
        int academicID = int.Parse(academicIDField.text);
        string password = passwordField.text;
        string name = nameField.text;
        string phone = phoneField.text;
        string address = addressField.text;
        string academicYear = academicYearField.text;
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(phone) ||
            string.IsNullOrEmpty(address) || string.IsNullOrEmpty(academicYear) || academicID <= 0)
        {
            ErrorText.text = "Please fill all fields correctly.";
            SuccessAlert.SetActive(false);
            ErrorAlert.SetActive(true);
            return;
        }

        FirebaseDataService firebaseDataService = new();
        bool success = await firebaseDataService.AddStudentAsync(academicID, password, name, phone, address, academicYear);

        if (success)
        {
            SuccessText.text = "Student Added Successfully";
            SuccessAlert.SetActive(true);
            ErrorAlert.SetActive(false);
        }
        else
        {
            ErrorText.text = "Error Adding Student";
            SuccessAlert.SetActive(false);
            ErrorAlert.SetActive(true);
        }
    }
    public void OnClearButtonClick()
    {
        academicIDField.text = "";
        passwordField.text = "";
        nameField.text = "";
        phoneField.text = "";
        addressField.text = "";
        academicYearField.text = "";
    }
    public void OnHideAlertClick()
    {
        SuccessAlert.SetActive(false);
        ErrorAlert.SetActive(false);
    }
    public void OnBackButtonClick()
    {
        AdminDashboardPanel.SetActive(true);
        AddStudentPanel.SetActive(false);
        LoginPanel.SetActive(false);
    }
    public void OnLogoutClick()
    {
        AdminDashboardPanel.SetActive(false);
        AddStudentPanel.SetActive(false);
        LoginPanel.SetActive(true);
    }
}
