using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NoiseSettings", menuName = "SettingsObjects/NoiseSettings")]
public class NoiseSettings : ScriptableObject {
    public float Scale = 50f;
    [Range(0, 10.0f)]
    public float Amplitude = 1.0f;
    [Range(1f, 32f)]
    public int Octaves = 1;
    [Range(0f, 10f)]
    public float Lacunarity = 2f;
    [Range(0f, 1f)]
    public float Persistence = 0.9f;
}
