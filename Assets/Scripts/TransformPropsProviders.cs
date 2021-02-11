using UnityEngine;

public interface IPositionProvider 
{
    Vector3 Position { get; set; }
}

public interface IRotationProvider
{
    Quaternion Rotation { get; set; }
}