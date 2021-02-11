using UnityEngine;
using System;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "Scriptable objects/Player settings")]
public class PlayerSettings : ScriptableObject
{
    public float cameraLerpSpeed = 1f;
    public float rotationLerpSpeed = 1f;
    [Range(0f, 1f)]
    public float airControl = 1f;
    public float airLerpSpeed = 1f;
}