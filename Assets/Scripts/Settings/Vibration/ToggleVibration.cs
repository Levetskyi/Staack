using UnityEngine.UI;
using UnityEngine;

public class ToggleVibration : MonoBehaviour
{
    [SerializeField] private Sprite on;
    [SerializeField] private Sprite off;

    private Image _vibrationIcon;

    private bool _isPlaying = false;

    private void Start()
    {
        _vibrationIcon = GetComponent<Image>();
         
        int value = PlayerPrefs.GetInt("Vibration", 1);

        _vibrationIcon.sprite = (value == 1) ? on : off;

        _isPlaying = value == 1;
    }

    public void SwitchVibrationState()
    {
        _isPlaying = !_isPlaying;

        PlayerPrefs.SetInt("Vibration", _isPlaying ? 1 : 0);

        _vibrationIcon.sprite = _isPlaying ? on : off;
    }
}