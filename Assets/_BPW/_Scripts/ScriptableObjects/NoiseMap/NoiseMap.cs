using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

[Serializable]
public struct NoiseMapSettings
{
    public int Size;
    public int Offset;
    public float Scale;
}

[CreateAssetMenu(fileName = "NoiseMap", menuName = "Tools/GenerateNoiseMap", order = 1)]
public class NoiseMap : ScriptableObject
{
    [SerializeField] private NoiseMapSettings m_NoiseMapSettings;
    
    private Texture2D m_NoiseMap;

    public Texture2D GetNoiseMap() {
        return m_NoiseMap;
    }

    public void SetNoiseMap(Texture2D noiseMap) {
        m_NoiseMap = noiseMap;
    }

    public NoiseMapSettings GetNoiseMapSettings() {
        return m_NoiseMapSettings;
    }
}
