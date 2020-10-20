using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGenerationSettings))]
public class MapGenerationSettingsEditor : Editor
{
    static readonly string[] hiddenMapGenerationSettingsProps = new string[] { "m_Script", "BiomeSettings", "ResourceSettings", "HeightmapSettings" };
    static readonly string[] hiddenNoiseSettingsProps = new string[] { "m_Script" };

    MapGenerationSettings settings;

    public override void OnInspectorGUI()
    {
        serializedObject.UpdateIfRequiredOrScript();
        DrawPropertiesExcluding(serializedObject, hiddenMapGenerationSettingsProps);
        serializedObject.ApplyModifiedProperties();

        DrawNoiseSettingsEditor("Biome Settings", settings.BiomeSettings);
        DrawNoiseSettingsEditor("Resource Settings", settings.ResourceSettings);
        DrawNoiseSettingsEditor("Heightmap Settings", settings.HeightmapSettings);
    }

    void DrawNoiseSettingsEditor(string label, NoiseSettings settings)
    {
        EditorGUILayout.Space();
        EditorGUILayout.ObjectField(label, settings, typeof(NoiseSettings), false);
        if (settings != null)
        {
            Editor editor = CreateEditor(settings);
            editor.serializedObject.UpdateIfRequiredOrScript();
            DrawPropertiesExcluding(editor.serializedObject, hiddenNoiseSettingsProps);
            editor.serializedObject.ApplyModifiedProperties();
        }
    }

    void OnEnable()
    {
        settings = (MapGenerationSettings)target;
    }
}
