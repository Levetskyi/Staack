using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private GameObject newHighScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    private TextMeshProUGUI _scoreText;
    private SaveSystem _saveSystem;

    private int _currentScore = 0;

    private EventBinding<EndLevelEvent> _endLevelEventBinding;
    private EventBinding<AddScoreEvent> _addScoreEventBinding;

    private void Awake()
    {
        _scoreText = GetComponent<TextMeshProUGUI>();

        _scoreText.text = _currentScore.ToString();

        _saveSystem = new SaveSystem();
    }

    private void OnEnable()
    {
        _endLevelEventBinding = new EventBinding<EndLevelEvent>(CheckForNewHighScore);
        EventBus<EndLevelEvent>.Register(_endLevelEventBinding);


        _addScoreEventBinding = new EventBinding<AddScoreEvent>(AddScore);
        EventBus<AddScoreEvent>.Register(_addScoreEventBinding);
    }

    private void OnDisable()
    {
        EventBus<EndLevelEvent>.Deregister(_endLevelEventBinding);

        EventBus<AddScoreEvent>.Deregister(_addScoreEventBinding);
    }

    private void AddScore()
    {
        _currentScore++;

        _scoreText.text = _currentScore.ToString();
    }

    private void CheckForNewHighScore()
    {
        if (_currentScore > _saveSystem.GetPlayerScore())
        {
            _saveSystem.SetPlayerScore(_currentScore);

            newHighScoreText.SetActive(true);
        }

        highScoreText.text = $"HighScore: {_saveSystem.GetPlayerScore()}";

        highScoreText.gameObject.SetActive(true);
    }
}