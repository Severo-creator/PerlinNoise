using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerreinGenerator : MonoBehaviour
{

    public int depth = 20;

    public int width = 256;
    public int height = 256;

    public float scale = 20;

    public Terrain terrain;

    void Start()
    {
        terrain = GetComponent<Terrain>();
    }

    void Update()
    {

        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }


    TerrainData GenerateTerrain(TerrainData terrainData){
        terrainData.heightmapResolution = width + 1;

        terrainData.size = new Vector3(width, depth, height);

        terrainData.SetHeights(0, 0, GeneratHeights());
        return terrainData;
    }

    float[,] GeneratHeights(){
        float[,] heights = new float[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                heights[x,y] = CalculateHeight(x,y);
            }
        }

        return heights;
    }

    float CalculateHeight(int x, int y){

        float Xcoord = ((float)x + transform.position.z) / width  * scale;
        float Ycoord = ((float)y + transform.position.x ) / height * scale;

        return Mathf.PerlinNoise(Xcoord, Ycoord);
    }
}