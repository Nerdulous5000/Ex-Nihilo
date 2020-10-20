using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapManager))]
public class MapManagerEditor : Editor {
    static bool foldout = true;
    static readonly string[] hiddenMapGenerationSettingsProps = new string[] { "m_Script", "BiomeSettings", "ResourceSettings", "HeightmapSettings" };
    static readonly string[] hiddenNoiseSettingsProps = new string[] { "m_Script" };

    MapManager manager;

    public override void OnInspectorGUI() {
        using (var check = new EditorGUI.ChangeCheckScope()) {
            base.OnInspectorGUI();
            if (GUILayout.Button("Regenerate")) {
                manager.OnEditorRegeneratePressed();
            }
            if (manager.GenerationSettings != null) {
                DrawSettingsEditor();
            }
            if (check.changed) {
                manager.OnEditorSettingsUpdate();
            }
        }
    }

    void DrawSettingsEditor() {
        foldout = EditorGUILayout.InspectorTitlebar(foldout, manager.GenerationSettings);
        if (foldout) {
            Editor editor = CreateEditor(manager.GenerationSettings);
            editor.OnInspectorGUI();
        }
    }

    void OnEnable() {
        manager = (MapManager)target;
    }
}
