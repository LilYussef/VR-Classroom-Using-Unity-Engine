using UnityEngine;
using Oculus.Avatar2;

public class GetAvatarID : MonoBehaviour
{

    private void Start()
    {
        // Example using Oculus Platform SDK to get logged-in user ID
        Oculus.Platform.Users.GetLoggedInUser().OnComplete(message =>
        {
            if (!message.IsError)
            {
                Debug.Log("User ID: " + message.Data.ID);
            }
            else
            {
                Debug.Log("No user is logged in or unable to retrieve user ID.");
            }
        });
    }
}