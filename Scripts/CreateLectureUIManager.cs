using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;
using System.Collections;

public class CreateLectureUIManager : MonoBehaviour
{
    [Header("Fields")]
    public TMP_InputField TitleInput;
    public TMP_InputField PasswordInput;
    public TMP_InputField StartTimeInput;
    public TMP_InputField GroupInput;
    public TMP_InputField SectionInput;
    public TMP_InputField DurationInput;
    public TMP_InputField AcademicYearInput;
    public TMP_InputField DepartmentInput;

    [Header("UI")]
    public GameObject LoadingPanel;
    public GameObject LoginPanel;
    public GameObject CreateLecturePanel;
    public GameObject AddUsersPanel;
    public GameObject SuccessAlert;
    public TMP_Text CodeText;
    public TMP_Text SuccessText;
    public GameObject ErrorAlert;
    public TMP_Text ErrorText;
    public Button CopyButton;
    public TMP_Text copyButtonLable;
    private IDataService dataService;
    string GeneratedCode;

    public async void OnCreateButtonClick()
    {
        SuccessAlert.SetActive(false);
        LoadingPanel.SetActive(true);

        if (string.IsNullOrWhiteSpace(TitleInput.text) ||
            string.IsNullOrWhiteSpace(PasswordInput.text) ||
            string.IsNullOrWhiteSpace(StartTimeInput.text) ||
            string.IsNullOrWhiteSpace(AcademicYearInput.text) ||
            string.IsNullOrWhiteSpace(DepartmentInput.text) ||
            string.IsNullOrWhiteSpace(GroupInput.text) ||
            string.IsNullOrWhiteSpace(SectionInput.text) ||
            string.IsNullOrWhiteSpace(DurationInput.text))
        {
            ErrorAlert.SetActive(true);
            ErrorText.text = "Please Fill All Fields.";
            
            return;
        }

        if (!int.TryParse(DurationInput.text, out int Duration) || int.TryParse(GroupInput.text, out int Group) || int.TryParse(SectionInput.text, out int Section))
        {
            ErrorAlert.SetActive(true);
            ErrorText.text = "Wrong Input Format.";
            
            return;
        }

        string lecturerName = CurrentUser.LecturerName;
        string LecturerEmail = CurrentUser.LecturerEmail;

        string code = await DBManager.Service.AddLectureAsync(
            TitleInput.text.Trim(),
            StartTimeInput.text.Trim(),
            PasswordInput.text.Trim(),
            DepartmentInput.text.Trim(),
            LecturerEmail,
            lecturerName,
            Group,
            Section,
            AcademicYearInput.text.Trim(),
            Duration
        );

        if (string.IsNullOrEmpty(code))
        {
            ErrorAlert.SetActive(true);
            ErrorText.text = "Something Went Wrong, Try Again.";
            return;
        }

        LoadingPanel.SetActive(false);
        SuccessAlert.SetActive(true);
        SuccessText.text = "Lecture Created Successfuly With Code:";
        CodeText.text = GeneratedCode;
        copyButtonLable.text = "Copy Code";

        CopyButton.onClick.RemoveAllListeners();
        CopyButton.onClick.AddListener(CopyCodeToClipboard);
    }

    void CopyCodeToClipboard()
    {
        GUIUtility.systemCopyBuffer = GeneratedCode;
        StopAllCoroutines();

        StartCoroutine(ShowCopiedRoutine());
    }
    IEnumerator ShowCopiedRoutine()
    {
        copyButtonLable.text = "Copied!!";
        yield return new WaitForSeconds(2f);
        copyButtonLable.text = "Copy Code";
    }
}
