using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGenerationSettings))]
public class MapGenerationSettingsEditor : Editor {
    static readonly string[] hiddenMapGenerationSettingsProps = new string[] { "m_Script", "BiomeSettings", "ResourceSettings", "HeightmapSettings" };
    static readonly string[] hiddenNoiseSettingsProps = new string[] { "m_Script" };

    MapGenerationSettings mapGenSettings;

    public override void OnInspectorGUI() {
        serializedObject.UpdateIfRequiredOrScript();
        DrawPropertiesExcluding(serializedObject, hiddenMapGenerationSettingsProps);
        DrawNoiseSettingsEditor("Biome Settings", ref mapGenSettings.BiomeSettings);
        DrawNoiseSettingsEditor("Resource Settings", ref mapGenSettings.ResourceSettings);
        DrawNoiseSettingsEditor("Heightmap Settings", ref mapGenSettings.HeightmapSettings);
        serializedObject.ApplyModifiedProperties();

    }

    void DrawNoiseSettingsEditor(string label, ref NoiseSettings noiseSettings) {
        EditorGUILayout.Space();
        noiseSettings = (NoiseSettings)EditorGUILayout.ObjectField(label, noiseSettings, typeof(NoiseSettings), false);
        if (noiseSettings != null) {
            Editor editor = CreateEditor(noiseSettings);
            editor.serializedObject.UpdateIfRequiredOrScript();
            DrawPropertiesExcluding(editor.serializedObject, hiddenNoiseSettingsProps);
            editor.serializedObject.ApplyModifiedProperties();
        }
    }

    void OnEnable() {
        mapGenSettings = (MapGenerationSettings)target;
    }
}
