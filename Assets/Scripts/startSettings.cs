using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startSettings : MonoBehaviour
{
    //Main params
    public string imagePath;        //path to image
    public Terrain playground;      //Main playing field
    public GameObject Player;
    public GameObject CameraMain;      

    private Texture2D imageTexture; //Object for Image
    private Vector3 newTerrainSize; //Object for sizes
    private Color pixelColor;
    private Vector3 objectPosition; //position object in playground
    private float newCubeHeight;

    void Start()
    {
        //Загружаем картинку в текстуру
        imageTexture = Resources.Load(imagePath) as Texture2D;

        //Устанавливаем размер игрового поля
        newTerrainSize.x = imageTexture.width;
        newTerrainSize.z = imageTexture.height;
        playground.terrainData.size = newTerrainSize;

        //Устанавливаем позицию игрока и камеры на сцене
        Player.transform.position = new Vector3(imageTexture.width / 2, 3, imageTexture.height / 2);
        //Vector3 offset = new Vector3(0, 2, -2.5f);
        //CameraMain.transform.position = Player.transform.position + offset;

        //Устанавливаем цвет и объекты на сцене
        for (int x = 0; x < imageTexture.width; x++)
        {
            for (int y = 0; y< imageTexture.height; y++)
            {
                pixelColor = imageTexture.GetPixel(x, y);
                //Создаем куб
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //Настраиваем параметры куба
                if (pixelColor.r < 0.1f || pixelColor.g < 0.1f || pixelColor.b < 0.1f)
                {
                    //рассчитываем высоту куба , сумма rgb + сделал поправку для темных цветов иначе значение округляется до нуля
                    // и на поверхности какая то лужа))
                    newCubeHeight = pixelColor.r + pixelColor.g + pixelColor.b + 0.3f;
                } else
                {
                    newCubeHeight = pixelColor.r + pixelColor.g + pixelColor.b; //рассчитываем высоту куба , сумма rgb
                }
                cube.transform.position = new Vector3(x, newCubeHeight / 2, y);// позиция куба
                cube.transform.localScale = new Vector3(1, newCubeHeight, 1); // устанавливаем высоту куба
                Renderer rend = cube.GetComponent<Renderer>();//ссылка на компонент renderer куба
                rend.material = new Material(Shader.Find("Specular"));// выбираем материал для покраски
                rend.material.SetColor("_Color", pixelColor); // красим
                //Instantiate(cube, objectPosition, Quaternion.identity); //создаем куб на сцене
               
            }
        }
    }

    void Update()
    {
        
    }
}