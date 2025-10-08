using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class DeleteAdminUIManager : MonoBehaviour
{
    public GameObject DeleteAdminPanel;
    public GameObject ConfirmAlert;
    public GameObject SuccessAlert;
    public GameObject ErrorAlert;
    public GameObject DeleteUsersPanel;
    public TMP_InputField AdminIDText;
    public TMP_InputField PasswordText;
    public TMP_Text SuccessText;
    public TMP_Text ErrorText;

    public void OnDeleteButtonClick()
    {
        ConfirmAlert.SetActive(true);
    }
    public async Task OnConfirmDeleteAlertButton()
    {
        string idText = AdminIDText.text.Trim();
        string passText = PasswordText.text.Trim();

        if (int.TryParse(idText, out int AdminID))
        {
            FirebaseDataService firebaseDataService = new();
            bool Delete = await firebaseDataService.DeleteAdminAsync(AdminID, passText);

            if (Delete)
            {
                SuccessText.text = "Admin Deleted Successfully!";
                ConfirmAlert.SetActive(false);
                SuccessAlert.SetActive(true);
            }
            else
            {
                ErrorText.text = "Admin Not Found.";
                ConfirmAlert.SetActive(false);
                ErrorAlert.SetActive(true);
            }
        }
        else
        {
            ErrorText.text = "Please Enter Correct Admin ID";
            ConfirmAlert.SetActive(false);
            ErrorAlert.SetActive(true);
        }
    }
    public void OnBackButtonClick()
    {
        DeleteAdminPanel.SetActive(false);
        DeleteUsersPanel.SetActive(true);
    }
    public void OnHideAlertClick()
    {
        SuccessAlert.SetActive(false);
        ErrorAlert.SetActive(false);
        ConfirmAlert.SetActive(false);
    }
}
