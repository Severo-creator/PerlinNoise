using UnityEngine;

public class GeradorTexturas : MonoBehaviour
{
    public int width = 256;
    public int height = 256;

    public float scale = 20;

    public float offsetX = 100f;
    public float offsetY = 100f;

    void Start()
    {
        offsetX = Random.Range(0, 99999f);
        offsetY = Random.Range(0, 99999f);
    }
    void Update()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = GenerateTexture();
    }

    Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(width, height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color color = calculateColor(x,y);
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
        return texture;
    }
    Color calculateColor(int x,int y)
    {
        float xCoord = (float)x / width * scale * offsetX;
        float yCoord = (float)y / width * scale * offsetY;
        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        return new Color(sample, sample, sample);
    }   
}
