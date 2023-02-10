using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NoiseMap))]
public class EditorNoiseMap : Editor
{
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        NoiseMap noiseMap = (NoiseMap)target;

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Generate Noise Map")) {
            NoiseMapSettings settings = noiseMap.GetNoiseMapSettings();
            noiseMap.SetNoiseMap(Noise.GenerateNoiseMap(settings.Size, settings.Offset, settings.Scale));
        }
        GUILayout.EndHorizontal();
    }
}
