using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(MeshFilter))]
public class meshController : MonoBehaviour
{
    Mesh mesh;

    private Vector3[] vertices;
    private int[] triangles;
    private Vector2[] uvs;

    public string pathToImage; // Путь до изображения
    public GameObject Player; // Игрок

    private Texture2D imageTexture; //избражение
    private float yAxisForMesh;

    Color pixelColor;
    
    void Start()
    {
        imageTexture = Resources.Load(pathToImage) as Texture2D;

        mesh = new Mesh();
        

        createMesh();
        updateMesh();
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
        Renderer rend = GetComponent<MeshRenderer>();

        //Создание и установка материала и текстуры поверхности
        rend.material = new Material(Shader.Find("Standard"));
        rend.material.mainTexture = imageTexture;

        //Позиция игрока на сцене
        Player.transform.position = new Vector3(imageTexture.width / 2, 3, imageTexture.height / 2);
    }

    void createMesh()
    {
        vertices = new Vector3[(imageTexture.width + 1) * (imageTexture.height + 1)];

        for (int i = 0, z = 0; z <= imageTexture.height; z++)
        {
            for (int x = 0; x <= imageTexture.width; x++)
            {
                pixelColor = imageTexture.GetPixel(x, z);

                yAxisForMesh = pixelColor.r + pixelColor.g + pixelColor.b;
                vertices[i] = new Vector3(x, yAxisForMesh, z);
                i++;
            }
        }

        triangles = new int[imageTexture.width * imageTexture.height * 6];

        int vert = 0;
        int tris = 0;

        for (int z = 0; z < imageTexture.height; z++)
        {
            for (int x = 0; x < imageTexture.width; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + imageTexture.width + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + imageTexture.width + 1;
                triangles[tris + 5] = vert + imageTexture.width + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }

        uvs = new Vector2[vertices.Length];

        for (int i = 0, z = 0; z <= imageTexture.height; z++)
        {
            for (int x = 0; x <= imageTexture.width; x++)
            {
                uvs[i] = new Vector2((float)x / imageTexture.width, (float)z / imageTexture.height);
                i++;
            }
        }



    }

    void updateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
       
        mesh.RecalculateNormals();
    }
}
