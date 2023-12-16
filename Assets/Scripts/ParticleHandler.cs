using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleHandler : MonoBehaviour
{
    private ParticleSystem _particleSystem;

    private void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        EventsHolder.OnPreciseTap += ShowParticles;
    }

    private void OnDisable()
    {
        EventsHolder.OnPreciseTap -= ShowParticles;
    }

    private void ShowParticles(Transform cubeTransform)
    {
        Vector3 newPosition = new(
            cubeTransform.position.x,
            cubeTransform.position.y - (cubeTransform.localScale.y / 2),
            cubeTransform.position.z
            );

        _particleSystem.transform.position = newPosition;

        _particleSystem.transform.localScale = cubeTransform.localScale;

        _particleSystem.Play();
    }
}