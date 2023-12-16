using UnityEngine;

public class TargetFrameRate : MonoBehaviour
{
    [SerializeField] int targetFrameRate = 60;

    private void Awake()
    {
        Application.targetFrameRate = targetFrameRate;
    }
}