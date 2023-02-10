using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise : MonoBehaviour
{
    public static Texture2D GenerateNoiseMap(int size, int offset, float scale) {
        float[,] noiseMapPixels = new float[size, size];

        for (int y = 0; y < size; y++) {
            for (int x = 0; x < size; x++) {
                float finalScale = Mathf.Max(0.00001f, scale);
                noiseMapPixels[x, y] = Mathf.PerlinNoise(x + offset * finalScale, y + offset * finalScale);
            }
        }

        Texture2D noiseMap = new Texture2D(size, size);
        for (int y = 0; y < size; y++) {
            for (int x = 0; x < size; x++) {
                noiseMap.SetPixel(x, y, new Color(0, 0, 0, noiseMapPixels[x, y]));
            }
        }

        return noiseMap;
    }
}
