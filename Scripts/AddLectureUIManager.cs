using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Threading.Tasks;
public class AddLectureUIManager : MonoBehaviour
{
    public TMP_InputField LectureNameField;
    public TMP_InputField LecturePasswordField;
    public TMP_InputField StartTimeField;
    public TMP_InputField GroupField;
    public TMP_InputField DurationField;
    public TMP_InputField DepartmentField;
    public TMP_InputField AcademicYearField;
    public TMP_InputField SectionField;
    public GameObject LecturerDashboardPanel;
    public GameObject CreateLecturePanel;
    public GameObject LoginMenuPanel;
    public GameObject SuccessAlert;
    public GameObject ErrorAlert;
    public TMP_Text FeedbackText;
    public TMP_Text EFeedbackText;
    public TMP_Text LectureCodeText;
    public TMP_Text CopyCodeButtonText;
    public Button CopyCodeButton;

    public async void OnSubmitClick()
    {
        string lectureName = LectureNameField.text;
        string password = LecturePasswordField.text;
        string startTime = StartTimeField.text;
        string department = DepartmentField.text;
        string academicYear = AcademicYearField.text;

        if (string.IsNullOrEmpty(lectureName) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(startTime) ||
            string.IsNullOrEmpty(department) || string.IsNullOrEmpty(academicYear))
        {
            ErrorAlert.SetActive(true);
            EFeedbackText.text = "Please fill all fields correctly.";

            return;
        }

        if (!int.TryParse(GroupField.text, out int group) || !int.TryParse(SectionField.text, out int section) || !int.TryParse(DurationField.text, out int duration) || group <= 0 || section <= 0 || duration <= 0)
        {
            ErrorAlert.SetActive(true);
            EFeedbackText.text = "Wrong Input Format.";

            return;
        }

        string generateCode = await DBManager.Service.AddLectureAsync(lectureName, startTime, password, department, CurrentUser.LecturerEmail, CurrentUser.LecturerName, group, section, academicYear, duration);

        if (!string.IsNullOrEmpty(generateCode))
        {
            SuccessAlert.SetActive(true);
            ErrorAlert.SetActive(false);
            FeedbackText.text = "Lecture Created With Code:";
            LectureCodeText.text = generateCode;
        }
        else
        {
            SuccessAlert.SetActive(false);
            ErrorAlert.SetActive(true);
            EFeedbackText.text = "Error Adding Lecture";

            return;
        }
    }

    public void OnClearClick()
    {
        LectureNameField.text = "";
        LecturePasswordField.text = "";
        StartTimeField.text = "";
        GroupField.text = "";
        DurationField.text = "";
        SectionField.text = "";
        DepartmentField.text = "";
        AcademicYearField.text = "";
    }

    public void OnBackButtonClick()
    {
        LecturerDashboardPanel.SetActive(true);
        CreateLecturePanel.SetActive(false);
        LoginMenuPanel.SetActive(false);
    }

    public void OnLogoutClick()
    {
        LecturerDashboardPanel.SetActive(false);
        CreateLecturePanel.SetActive(false);
        LoginMenuPanel.SetActive(true);
    }

    public void CopyCode()
    {
        string Code = LectureCodeText.text;
        GUIUtility.systemCopyBuffer = Code;

        CopyCodeButtonText.text = "Copied!!";

        StartCoroutine(ResetCopyButtonText());
    }
    private IEnumerator ResetCopyButtonText()
    {
        yield return new WaitForSeconds(3f);
        CopyCodeButtonText.text = "Code";
    }

    public void OnHideButtonClick()
    {
        SuccessAlert.SetActive(false);
        ErrorAlert.SetActive(false);
        CreateLecturePanel.SetActive(true);
    }
}
