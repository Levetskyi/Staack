using UnityEngine;

public struct CubeSplitEvent  : IEvent 
{
    public Vector3 Position;
    public Vector3 Scale;
    public Color Color;
}