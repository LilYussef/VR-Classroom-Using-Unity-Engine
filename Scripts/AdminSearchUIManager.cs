using UnityEngine;
using TMPro;
using Firebase.Firestore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.Common;

public class AdminSearchUIManager : MonoBehaviour
{
    public TMP_InputField SearchAdminIDField;
    public TMP_InputField IDText;
    public TMP_InputField PasswordText;
    public TMP_Text ResultText;
    public GameObject AdminDataPanel;
    public GameObject SearchAdminPanel;
    public GameObject UpdateDataPanel;

    public void OnSearchButtonClick()
    {
        string idText = SearchAdminIDField.text.Trim();

        if (string.IsNullOrEmpty(idText))
        {
            ResultText.text = "Please Enter Admin ID.";
            return;
        }
        if (!int.TryParse(idText, out int AdminID))
        {
            ResultText.text = "Admin ID Must Be A Number.";
            return;
        }
        SearchAdminById(AdminID);
    }

    private async void SearchAdminById(int adminId)
    {
        var db = FirebaseFirestore.DefaultInstance;
        var doc = await db.Collection("Admin").Document(adminId.ToString()).GetSnapshotAsync();

        if (!doc.Exists)
        {
            ResultText.text = "Admin Not Found.";
            AdminDataPanel.SetActive(false);
            SearchAdminPanel.SetActive(true);
            return;
        }
        IDText.text = doc.GetValue<int>("AdminID").ToString();
        PasswordText.text = "";
        PasswordText.placeholder.GetComponent<TMP_Text>().text = "Leave Blank To Keep Current";
        AdminData.CurrentAdminId = adminId;
        ResultText.text = "Admin Found.";

        SearchAdminPanel.SetActive(false);
        AdminDataPanel.SetActive(true);
    }
    public void OnConfermButtonClick()
    {
        if (!int.TryParse(IDText.text.Trim(), out int newId))
        {
            ResultText.text = "Admin ID Must Be Numeric";
            return;
        }

        string newPassword = PasswordText.text.Trim();

        bool pwChanged = !string.IsNullOrEmpty(newPassword);
        bool idChanged = newId != AdminData.CurrentAdminId;

        if (!pwChanged && !idChanged)
        {
            ResultText.text = "No Data Changed";
            return;
        }
        checkAndUpdateAdmin(newId, newPassword, pwChanged, idChanged);
    }
    private async void checkAndUpdateAdmin(int newId, string newPassword, bool pwChanged, bool idChanged)
    {
        if (idChanged && await IsAdminIDUsed(newId))
        {
            ResultText.text = "This ID Is Already Used.";
            return;
        }

        await UpdateAdminData(newId, newPassword, pwChanged, idChanged);
        ResultText.text = "Admin Data Updated Successfully.";
    }
    private async Task<bool> IsAdminIDUsed(int id)
    {
        var db = FirebaseFirestore.DefaultInstance;
        var doc = await db.Collection("Admin").Document(id.ToString()).GetSnapshotAsync();
        return doc.Exists;
    }
    private async Task UpdateAdminData(int newId, string newPassword, bool pwChanged, bool idChanged)
    {
        var db = FirebaseFirestore.DefaultInstance;
        string currentKey = AdminData.CurrentAdminId.ToString();
        var updates = new Dictionary<string, object> { { "AdminID", newId } };

        if (pwChanged)
        {
            updates["Hash"] = BCrypt.Net.BCrypt.HashPassword(newPassword);
        }
        if (idChanged)
        {
            await db.Collection("Admin").Document(newId.ToString()).SetAsync(updates);
            await db.Collection("Admin").Document(currentKey).DeleteAsync();
            AdminData.CurrentAdminId = newId;
        }
        else
        {
            await db.Collection("Admin").Document(currentKey).UpdateAsync(updates);
        }
    }

    public void OnBackButtonClick()
    {
        AdminDataPanel.SetActive(false);
        SearchAdminPanel.SetActive(false);
        UpdateDataPanel.SetActive(true);
    }
}
