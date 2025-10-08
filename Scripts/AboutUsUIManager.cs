using UnityEngine;

public class AboutUsUIManager : MonoBehaviour
{
    public GameObject AboutUsPanel;
    public GameObject ContactUsPanel;
    public GameObject LoginPanel;

    public void OnContactUsButtonClick()
    {
        AboutUsPanel.SetActive(false);
        ContactUsPanel.SetActive(true);
        LoginPanel.SetActive(false);
    }
}