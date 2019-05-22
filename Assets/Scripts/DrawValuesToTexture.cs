// https://github.com/unitycoder/ScrollingTexturePlotter

using UnityEngine;

namespace UnityLibrary
{
    public class DrawValuesToTexture : MonoBehaviour
    {
        [Tooltip("Target material")]
        public Material mat;

        // our plotting texture
        Texture2D tex;
        Color32[] pixels;

        int size = 1024;

        // perlin noise values
        float y;
        float m1 = 0.07123f;
        float m2 = 0.0156f;
        float m3 = 0.0054f;

        void Start()
        {
            // create texture with 1 pixel height
            tex = new Texture2D(size, 1, TextureFormat.ARGB32, false, false);
            tex.filterMode = FilterMode.Point;
            tex.wrapMode = TextureWrapMode.Clamp;

            // init pixel array
            pixels = tex.GetPixels32(0);

            // assign texture to our material
            mat.SetTexture("_MainTex", tex);
        }

        void Update()
        {
            // fill pixels with some perlin noise values
            for (int i = 0; i < size; i++)
            {
                float r = Mathf.PerlinNoise(i * m1, y * m1) - 0.5f;
                float g = Mathf.PerlinNoise(i * m2, y * m2) - 0.5f;
                float b = Mathf.PerlinNoise(i * m3, y * m3) - 0.5f;
                pixels[i] = new Color(r, g, b, 1);
            }
            y += 1f;
            tex.SetPixels32(pixels);
            tex.Apply(false);
        }
    }
}