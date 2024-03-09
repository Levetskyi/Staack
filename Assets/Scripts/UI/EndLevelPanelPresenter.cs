using UnityEngine;

public class EndLevelPanelPresenter : MonoBehaviour
{
    [SerializeField] private GameObject losePanel;

    private EventBinding<EndLevelEvent> _endLevelEventBinding;

    private void Awake() => losePanel.SetActive(false);

    private void OnEnable()
    {
        _endLevelEventBinding = new EventBinding<EndLevelEvent>(ShowLosePanel);
        EventBus<EndLevelEvent>.Register(_endLevelEventBinding);
    }

    private void OnDisable()
    {
        EventBus<EndLevelEvent>.Deregister(_endLevelEventBinding);
    }

    private void ShowLosePanel() => losePanel.SetActive(true);
}