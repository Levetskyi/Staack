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

    private void Awake()
    {
        _scoreText = GetComponent<TextMeshProUGUI>();

        _scoreText.text = _currentScore.ToString();

        _saveSystem = new SaveSystem();
    }

    private void OnEnable()
    {
        EventsHolder.OnAddScore += AddScore;
        EventsHolder.OnEndLevel += CheckForNewHighScore;
    }

    private void OnDisable()
    {
        EventsHolder.OnAddScore -= AddScore;
        EventsHolder.OnEndLevel -= CheckForNewHighScore;
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