using UnityEngine;
using Firebase;
using Firebase.Extensions;

public static class DBManager
{
    public static IDataService Service { get; private set; }

    [RuntimeInitializeOnLoadMethod]
    static void Init()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                Service = new FirebaseDataService();
                Debug.Log("Firebase initialized successfully.");
            }
            else
            {
                Debug.LogError("Firebase initialization failed: " + task.Result);
            }
        });
    }
}
