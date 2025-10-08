using UnityEngine;
using TMPro;
using Oculus.Platform.Models;
using System.Threading.Tasks;

public class DeleteLecturerUIManager : MonoBehaviour
{
    public GameObject DeleteUsersPanel;
    public GameObject DeleteLecturerPanel;
    public GameObject ConfirmDeleteAlert;
    public GameObject SuccessDeleteAlert;
    public GameObject ErrorDeleteAlert;
    public TMP_InputField EmailText;
    public TMP_InputField PasswordText;
    public TMP_Text SuccessText;
    public TMP_Text ErrorText;

    public void OnDeleteButtonClick()
    {
        ConfirmDeleteAlert.SetActive(true);
    }
    public async Task OnConfirmDeleteAlertButton()
    {
        string emailText = EmailText.text.Trim();
        string passText = PasswordText.text.Trim();

            FirebaseDataService firebaseDataService = new();
            bool Delete = await firebaseDataService.DeleteLecturerAsync(emailText, passText);

            if (Delete)
            {
                SuccessText.text = "Lecturer Deleted Successfully!";
                ConfirmDeleteAlert.SetActive(false);
                SuccessDeleteAlert.SetActive(true);
            }
            else
            {
                ErrorText.text = "Incorrect Email or Password";
                ConfirmDeleteAlert.SetActive(false);
                ErrorDeleteAlert.SetActive(true);
            }
    }
    public void OnBackButtonClick()
    {
        DeleteLecturerPanel.SetActive(false);
        DeleteUsersPanel.SetActive(true);
    }
    public void OnHideAlertButtonClick()
    {
        ConfirmDeleteAlert.SetActive(false);
        SuccessDeleteAlert.SetActive(false);
        ErrorDeleteAlert.SetActive(false);
    }
}
