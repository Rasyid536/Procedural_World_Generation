using UnityEngine;

public class terrain_generation : MonoBehaviour
{
    public int depth = 20;

    private int width = 256;
    private int height = 256;

    public float scale = 20f;
    public float offSetX = 100f;
    public float offSetY = 100f;
    public int octaves = 4;         
    public float persistence = 0.5f;
    public float lacunarity = 2.0f;

    private void Start()
    {
        offSetX = Random.Range(0f, 9999f);
        offSetY = Random.Range(0f, 9999f);
    }
    
    private void Update()
    {
         Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width +1;
        terrainData.size = new Vector3(width, depth, height);

        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                heights[x, y] = CalculateHeights(x, y);
            }
        }

        return heights;
    }

    float CalculateHeights(int x, int y)
    {
        float noiseValue = 0f;
        float amplitude = 1f;
        float frequency = 1f;
        float maxValue = 0f;

        for (int i = 0; i < octaves; i++)
        {
            float xCoord = (float)x / width * scale * frequency + offSetX;
            float yCoord = (float)y / height * scale * frequency + offSetY;

            float sample = Mathf.PerlinNoise(xCoord, yCoord) * amplitude;
            noiseValue += sample;
            maxValue += amplitude;

            amplitude *= persistence;
            frequency *= lacunarity;
        }

        noiseValue /= maxValue;

        return noiseValue;
    } 
}
