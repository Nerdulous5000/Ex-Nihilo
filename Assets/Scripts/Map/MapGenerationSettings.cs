using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapGenerationSettings", menuName = "SettingsObjects/MapGenerationSettings")]
public class MapGenerationSettings : ScriptableObject {
    [Range(-2f, 2f)]
    public float SeaLevel = 0f;
    [Range(-2f, 2f)]
    public float ResourceThreshold = 0f;

    public NoiseSettings BiomeSettings;
    public NoiseSettings ResourceSettings;
    public NoiseSettings HeightmapSettings;
}
