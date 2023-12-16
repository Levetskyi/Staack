using UnityEngine;

public class EndLevelPanelPresenter : MonoBehaviour
{
    [SerializeField] private GameObject losePanel;

    private void Awake()
    {
        losePanel.SetActive(false);
    }

    private void OnEnable()
    {
        EventsHolder.OnEndLevel += ShowLosePanel;
    }

    private void OnDisable()
    {
        EventsHolder.OnEndLevel -= ShowLosePanel;
    }

    private void ShowLosePanel()
    {
        losePanel.SetActive(true);
    }
}