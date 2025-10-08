using UnityEngine;
using TMPro;
using Fusion;

public class JoinLectureForStudent : MonoBehaviour
{
    [Header("Fusion")]
    public NetworkRunner runner;
    public NetworkObject playerPrefab;
    [Header("UI Panels")]
    public GameObject StudentDashboardPanel;
    public GameObject JoinLecturePanel;
    public GameObject StudentToolsForLecture;
    public GameObject PrototypeStart;
    [Header("Input Fields andText")]
    public TMP_InputField LectureCodeInput;
    public TMP_InputField LecturePassInput;
    public TMP_Text DebugText;

    public async void OnJoinButtonClick()
    {
        string CodeText = LectureCodeInput.text.Trim();
        string Password = LecturePassInput.text.Trim();

        if (string.IsNullOrEmpty(CodeText) || string.IsNullOrEmpty(Password))
        {
            DebugText.text = "Please Fill All Fields";
            return;
        }

        FirebaseDataService firebaseDataService = new();
        bool isValid = await firebaseDataService.JoinLectureAuthAsync(CodeText, Password);
        if (isValid)
        {
            DebugText.text = "Join Success";
            JoinLecturePanel.SetActive(false);
            PrototypeStart.SetActive(true);
            StudentToolsForLecture.SetActive(false);
        }
        else
        {
            DebugText.text = "Join Failed, Check Code and Password";
            return;
        }
        if (!runner.IsRunning)
        {
            await runner.StartGame(new StartGameArgs
            {
                GameMode = GameMode.Shared,
                SessionName = CodeText
            });
            Vector3 playerSpawnPosition = new(3, 1, -10);
            runner.Spawn(playerPrefab, playerSpawnPosition, Quaternion.identity, runner.LocalPlayer);
        }
    }
    public void OnBackButtonClick()
    {
        JoinLecturePanel.SetActive(false);
        StudentToolsForLecture.SetActive(false);
        StudentDashboardPanel.SetActive(true);
    }
}
