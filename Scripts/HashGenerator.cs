using UnityEngine;
using BCrypt.Net;

public class HashGenerator : MonoBehaviour
{
    void Start()
    {
        // Example usage
        string password = "1111";
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
        Debug.Log("Hashed Password: " + hashedPassword);
    }
}