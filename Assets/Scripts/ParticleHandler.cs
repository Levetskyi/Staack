using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleHandler : MonoBehaviour
{
    private ParticleSystem _particleSystem;

    private EventBinding<PreciseTapEvent> _preciseTapEventBinding;

    private void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        _preciseTapEventBinding = new EventBinding<PreciseTapEvent>(ShowParticles);
        EventBus<PreciseTapEvent>.Register(_preciseTapEventBinding);
    }

    private void OnDisable()
    {
        EventBus<PreciseTapEvent>.Deregister(_preciseTapEventBinding);
    }

    private void ShowParticles(PreciseTapEvent tapEvent)
    {
        Vector3 newPosition = new(
            tapEvent.CubePosition.position.x,
            tapEvent.CubePosition.position.y - (tapEvent.CubePosition.localScale.y / 2),
            tapEvent.CubePosition.position.z
            );

        _particleSystem.transform.position = newPosition;

        _particleSystem.transform.localScale = tapEvent.CubePosition.localScale;

        _particleSystem.Play();
    }
}