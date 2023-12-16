using UnityEngine;

public class UrlButton : MonoBehaviour
{
    [SerializeField] private const string instagramUrl = "https://instagram.com/levetskyi._?igshid=OGQ5ZDc2ODk2ZA==";

    public void OpenUrl(string URL)
    {
        Application.OpenURL(URL);
    }

    public void OpenInstagram()
    {
        Application.OpenURL(instagramUrl);
    }
}