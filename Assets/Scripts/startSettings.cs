using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startSettings : MonoBehaviour
{
    //Main params
    public string imagePath;        //path to image
    public Terrain playground;      //Main playing field
    public GameObject Player;
    public GameObject CubePrefab;         

    private Texture2D imageTexture; //Object for Image
    private Vector3 newTerrainSize; //Object for sizes
    private Color pixelColor;
    private Vector3 objectPosition; //position object in playground

    void Start()
    {
        //Load image in Texture
        imageTexture = Resources.Load(imagePath) as Texture2D;

        //Set new size Terrain
        newTerrainSize.x = imageTexture.width;
        newTerrainSize.z = imageTexture.height;
        playground.terrainData.size = newTerrainSize;

        //Set player position
        Player.transform.position = new Vector3(newTerrainSize.x / 2, 0, newTerrainSize.z / 2);

        //Check pixel color and set objects on scene
        for (int x = 0; x < imageTexture.width; x++)
        {
            for (int y = 0; y< imageTexture.height; y++)
            {
                pixelColor = imageTexture.GetPixel(x, y);
                if (pixelColor.r < 1f && pixelColor.g < 1f && pixelColor.b < 1f)
                {
                    objectPosition = new Vector3(x, 0.5f, y);
                    Instantiate(CubePrefab, objectPosition, Quaternion.identity);
                }
            }
        }
    }

    void Update()
    {
        
    }
}