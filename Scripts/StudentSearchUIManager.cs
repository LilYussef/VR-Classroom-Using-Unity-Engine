using UnityEngine;
using TMPro;
using System.Collections.Generic;
using Firebase.Firestore;
using System.Threading.Tasks;
using BCrypt.Net;

public class StudentSearchUIManager : MonoBehaviour
{
    public TMP_InputField SearchStudentIDField;
    public TMP_InputField IDText;
    public TMP_InputField PasswordText;
    public TMP_InputField NameText;
    public TMP_InputField GroupText;
    public TMP_InputField SectionText;
    public TMP_InputField DepartmentText;
    public TMP_InputField PhoneText;
    public TMP_InputField AdderssText;
    public TMP_InputField AcademicYearText;
    public TMP_Text ResultText;
    public GameObject StudentDataPanel;
    public GameObject SearchStudentPanel;
    public GameObject UpdateDataPanel;

    public void OnSearchButtonClick()
    {
        string idText = SearchStudentIDField.text.Trim();

        if (string.IsNullOrEmpty(idText))
        {
            ResultText.text = "Please Enter Academic ID.";
            return;
        }
        if (!int.TryParse(idText, out int AcademicID))
        {
            ResultText.text = "Academic ID Must Be A Number.";
            return;
        }

        SearchStudentById(AcademicID);
    }

    private async void SearchStudentById(int academicId)
    {
        var db = FirebaseFirestore.DefaultInstance;
        var doc = await db.Collection("Student").Document(academicId.ToString()).GetSnapshotAsync();

        if (!doc.Exists)
        {
            ResultText.text = "Student Not Found.";
            SearchStudentPanel.SetActive(true);
            StudentDataPanel.SetActive(false);
            return;
        }

        IDText.text = doc.TryGetValue("AcademicID", out int aid) ? aid.ToString() : "";
        NameText.text = doc.GetValue<string>("Name");
        GroupText.text = doc.TryGetValue("Group", out int g) ? g.ToString() : "";
        SectionText.text = doc.TryGetValue("Section", out int s) ? s.ToString() : "";
        DepartmentText.text = doc.GetValue<string>("Department");
        PhoneText.text = doc.GetValue<string>("Phone");
        AdderssText.text = doc.GetValue<string>("Address");
        AcademicYearText.text = doc.GetValue<string>("AcademicYear");
        PasswordText.text = "";
        PasswordText.placeholder.GetComponent<TMP_Text>().text = "Leave Blank To Keep Current";

        ResultText.text = "Student Found.";
        SearchStudentPanel.SetActive(false);
        StudentDataPanel.SetActive(true);
        UpdateDataPanel.SetActive(false);
        StudentData.CurrentStudentId = academicId;


    }

    public void OnConfirmButtonClick()
    {
        if (!int.TryParse(IDText.text.Trim(), out int newId))
        {
            ResultText.text = "Academic ID must be a numeric.";
            return;
        }

        string newPassword = PasswordText.text.Trim();
        string newName = NameText.text.Trim();
        string newGroup = GroupText.text.Trim();
        string newSection = SectionText.text.Trim();
        string newDepartment = DepartmentText.text.Trim();
        string newPhone = PhoneText.text.Trim();
        string newAddress = AdderssText.text.Trim();
        string newAcademicYear = AcademicYearText.text.Trim();

        if (string.IsNullOrEmpty(newName) || string.IsNullOrEmpty(newGroup) || string.IsNullOrEmpty(newSection) ||
            string.IsNullOrEmpty(newDepartment) || string.IsNullOrEmpty(newPhone) || string.IsNullOrEmpty(newAddress) ||
            string.IsNullOrEmpty(newAcademicYear))
        {
            ResultText.text = "Please Fill All Fields.";
            return;
        }

        CheckAndUpdateStudent(newId, newPassword, newName, newGroup, newSection, newDepartment, newPhone, newAddress, newAcademicYear);
    }

    private async void CheckAndUpdateStudent(int newId, string newPassword, string newName, string newGroup, string newSection, string newDepartment, string newPhone, string newAddress, string newAcademicYear)
    {
        bool passwordChanged = !string.IsNullOrEmpty(newPassword);
        bool otherChanged = newId != StudentData.CurrentStudentId || newName != "" || newGroup != "" || newSection != "" ||
            newDepartment != "" || newPhone != "" || newAddress != "" || newAcademicYear != "";

        if (!passwordChanged && !otherChanged)
        {
            ResultText.text = "No Changes Detected.";
            return;
        }
        if (await IsAcademicIDUsedByAnother(newId))
        {
            ResultText.text = "Academic ID is already used.";
            return;
        }

        await UpdateStudentData(newId, newPassword, passwordChanged, newName, newGroup, newSection, newDepartment, newPhone, newAddress, newAcademicYear);
        ResultText.text = "Student Data Updated Successfully.";
    }

    private async Task<bool> IsAcademicIDUsedByAnother(int newId)
    {
        if (newId == StudentData.CurrentStudentId) return false;

        var db = FirebaseFirestore.DefaultInstance;
        var doc = await db.Collection("Student").Document(newId.ToString()).GetSnapshotAsync();
        return doc.Exists;
    }

    private async Task UpdateStudentData(int newId,string newPassword, bool passwordChanged, string newName, string newGroup, string newSection, string newDepartment, string newPhone, string newAddress, string newAcademicYear)
    {
        var db = FirebaseFirestore.DefaultInstance;
        string currentKey = StudentData.CurrentStudentId.ToString();

        var updates = new Dictionary<string, object>
        {
            { "AcademicID", newId },
            { "Name", newName },
            { "Group", int.Parse(newGroup) },
            { "Section", int.Parse(newSection) },
            { "Department", newDepartment },
            { "Phone", newPhone },
            { "Address", newAddress },
            { "AcademicYear", newAcademicYear }
        };

        if (passwordChanged)
        {
            updates["Hash"] = BCrypt.Net.BCrypt.HashPassword(newPassword);
        }

        if (newId != StudentData.CurrentStudentId)
        {
            await db.Collection("Student").Document(newId.ToString()).SetAsync(updates);
            await db.Collection("Student").Document(currentKey).DeleteAsync();
            StudentData.CurrentStudentId = newId;
        }
        else
        {
            await db.Collection("Student").Document(currentKey).UpdateAsync(updates);
        }
    }

    public void OnBackButtonClick()
    {
        StudentDataPanel.SetActive(false);
        SearchStudentPanel.SetActive(false);
        UpdateDataPanel.SetActive(true);
    }
}

