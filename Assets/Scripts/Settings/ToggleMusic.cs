using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Image))]
public class ToggleMusic : MonoBehaviour
{
    [SerializeField] private Sprite on;
    [SerializeField] private Sprite off;

    private Image _audioIcon;

    private bool _isPlaying = false;

    private void Start()
    {
        _audioIcon = GetComponent<Image>();

        int value = PlayerPrefs.GetInt("AudioVolume", 1);

        AudioListener.volume = value;

        _audioIcon.sprite = (value == 1) ? on : off;

        _isPlaying = value == 1;
    }

    public void SwitchAudioState()
    {
        _isPlaying = !_isPlaying;

        AudioListener.volume = _isPlaying ? 1 : 0;

        PlayerPrefs.SetInt("AudioVolume" , _isPlaying ? 1 : 0);

        _audioIcon.sprite = _isPlaying ? on : off;
    }
}