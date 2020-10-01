using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Dimensions
{
    private static string _startPoint;
    private static string _endPoint;
    private static float _distance = 0;
    private static string _distanceToString;

    public static void SaveDimensions(Vector3 startPoint, Vector3 endPoint)
    {
        _startPoint = startPoint.ToString();
        _endPoint = endPoint.ToString();
        _distance = Vector3.Distance(startPoint, endPoint);
        _distanceToString = _distance.ToString("#.#");
        PlayerPrefs.SetString("StartPoint", _startPoint);
        PlayerPrefs.SetString("EndPoint", _endPoint);
        PlayerPrefs.SetString("Distance", _distanceToString);
        PlayerPrefs.Save();
        Debug.Log("Dimensions Saved");
    }

    public static string LoadDimension(string dimension)
    {
        switch (dimension)
        {
            case "StartPoint":
                return PlayerPrefs.GetString("StartPoint");
            case "EndPoint":
                return PlayerPrefs.GetString("EndPoint");
            case "Distance":
                return PlayerPrefs.GetString("Distance");
            default:
                return "Измерения нет";
        }
    }

    public static void ResetDimensions()
    {
        PlayerPrefs.DeleteAll();
        _startPoint = "";
        _endPoint = "";
        _distance = 0.0f;
    }
    
}
