using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private TextMeshProUGUI _fps;

    private float _timer = 0f;
    private readonly float _updateInterval = 1f;

    private void Awake()
    {
        _fps = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _updateInterval)
        {
            float frameRate = 1f / Time.deltaTime;
            _fps.text = Mathf.Round(frameRate).ToString();
            _timer -= _updateInterval;
        }
    }
}
