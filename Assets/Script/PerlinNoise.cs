using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    public int height = 256;
    public int width = 256;

    public float scale = 20f;
    public float offSetx, offSety;
    public float speed;
    public int octaves = 4;         
    public float persistence = 0.5f;
    public float lacunarity = 2.0f;


    void Start()
    {
        offSetx = Random.Range(0f, 99999f);
        offSety = Random.Range(0f, 99999f);
    }
    void Update()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = GenerateTexture();

        if(Input.GetKey("w"))
        {
            scale += 3f * Time.deltaTime;
        }
        if(Input.GetKey("s"))
        {
            scale -= 3f * Time.deltaTime;
        }

        
        if(Input.GetKey(KeyCode.UpArrow))
        {
            offSety += speed * Time.deltaTime;
            
        }
        if(Input.GetKey(KeyCode.DownArrow))
        {
            offSety -= speed * Time.deltaTime;
            
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            offSetx -= speed * Time.deltaTime;
            
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            offSetx += speed * Time.deltaTime;
            
        }

        //widht and height can't be less than 1
        if(height < 1)
            height =  1;
        if(width < 1)
            width = 1;
    } 

    Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(width, height);

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                Color color = ModifyingColor(x, y);
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();
        return texture;
    }

    Color ModifyingColor(int x,int  y)
    {
        Color color = CalculateColor(x, y);
        return color;
    }

    Color CalculateColor(int x, int y)
    {
        float noiseValue = 0f;
        float amplitude = 1f;
        float frequency = 1f;
        float maxValue = 0f;

        for (int i = 0; i < octaves; i++)
        {
            float xCoord = (float)x / width * scale * frequency + offSetx;
            float yCoord = (float)y / height * scale * frequency + offSety;

            float sample = Mathf.PerlinNoise(xCoord, yCoord) * amplitude;
            noiseValue += sample;
            maxValue += amplitude;

            amplitude *= persistence;
            frequency *= lacunarity;
        }

        noiseValue /= maxValue;

        return new Color(noiseValue, noiseValue, noiseValue);
    }
}
