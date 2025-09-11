using UnityEngine;
using System.Collections;
[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    
    Mesh mesh;
    Vector3[] vertices;
    int[] triangle;

    public int xSize = 50;
    public int zSize = 50;
    
    
    
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh; 

        StartCoroutine(CreateShape());
    }

    void Update()
    {
        UpdateMesh();
    }

    IEnumerator CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        
        for(int i = 0,z = 0; z <= zSize; z++)
        {
            for( int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * .3f, z * .3f) *4f;
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        triangle = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;

        for(int z = 0; z < zSize; z++)
        {
            for(int x = 0; x < xSize; x++)
            {
            
                triangle[tris + 0] = vert + 0;
                triangle[tris + 1] = vert + xSize + 1;
                triangle[tris + 2] = vert + 1;
                triangle[tris + 3] = vert + 1;
                triangle[tris + 4] = vert + xSize + 1;
                triangle[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;

                yield return new WaitForSeconds(.01f);
            }
            vert ++;
        }
        
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangle;

    }

}
