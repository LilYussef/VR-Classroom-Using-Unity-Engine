using UnityEngine;
using TMPro;
using Oculus.Platform.Models;
using System.Threading.Tasks;

public class DeleteStudentUIManager : MonoBehaviour
{
    public GameObject DeleteUsersPanel;
    public GameObject DeleteStudentPanel;
    public GameObject ConfirmDeleteAlert;
    public GameObject SuccessDeleteAlert;
    public GameObject ErrorDeleteAlert;
    public TMP_InputField AcademicIDText;
    public TMP_InputField PasswordText;
    public TMP_Text SuccessText;
    public TMP_Text ErrorText;

    public void OnDeleteButtonClick()
    {
        ConfirmDeleteAlert.SetActive(true);
    }
    public async Task OnConfirmDeleteAlertButton()
    {
        string idText = AcademicIDText.text.Trim();
        string passText = PasswordText.text.Trim();

        if (int.TryParse(idText, out int AcademicID))
        {
            FirebaseDataService firebaseDataService = new();
            bool Delete = await firebaseDataService.DeleteStudentAsync(AcademicID, passText);

            if (Delete)
            {
                SuccessText.text = "Student Deleted Successfully!";
                ConfirmDeleteAlert.SetActive(false);
                SuccessDeleteAlert.SetActive(true);
            }
            else
            {
                ErrorText.text = "Incorrect Academic ID or Password";
                ConfirmDeleteAlert.SetActive(false);
                ErrorDeleteAlert.SetActive(true);
            }
        }
        else
        {
            ErrorText.text = "Please Enter Correct Academic Number";
            ConfirmDeleteAlert.SetActive(false);
            ErrorDeleteAlert.SetActive(true);
        }
    }
    public void OnBackButtonClick()
    {
        DeleteStudentPanel.SetActive(false);
        DeleteUsersPanel.SetActive(true);
    }
    public void OnHideAlertButtonClick()
    {
        ConfirmDeleteAlert.SetActive(false);
        SuccessDeleteAlert.SetActive(false);
        ErrorDeleteAlert.SetActive(false);
    }
}
