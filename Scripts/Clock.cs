using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public TextMeshProUGUI clockText;

    void Update()
    {
        clockText.text = System.DateTime.Now.ToString("HH:mm:ss");
    }
}
