using UnityEngine;
using System;

public static class EventsHolder
{
    public static event Action<Vector3, Vector3, Color> OnCubeSplit;
    public static void CubeSplit(Vector3 position, Vector3 scale, Color color) => OnCubeSplit?.Invoke(position, scale, color);

    public static event Action OnEndLevel;
    public static void EndLevel() => OnEndLevel?.Invoke();

    public static event Action OnTouch;
    public static void Touch() => OnTouch?.Invoke();

    public static event Action OnAddScore;
    public static void AddScore() => OnAddScore?.Invoke();

    public static event Action<Transform> OnPreciseTap;
    public static void PreciseTap(Transform cubePosition) => OnPreciseTap?.Invoke(cubePosition);
}